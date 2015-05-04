using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine.UI;

public class PartData
{
    public string hair_name  = "";
    public string head_name  = "";
    public string torso_name = "";
    public string legs_name  = "";
    public string shoes_name = "";

    public Vector3 leg_up = Vector3.up;
    public Vector3 pos = Vector3.zero;
    public Quaternion main_rotation = Quaternion.LookRotation(Vector3.back);
    

    [XmlIgnore]
    public Transform   character_parent = null;
}

public class CustomiserController : MonoBehaviour 
{
    public MovementController movement_controller;
    public Transform customiser_camera;
    public Transform customiser_ui;
    public Transform placement_camera;
    public Transform placement_ui;
	public Transform stage;
    
    public Transform part_parent; /*parenet transform that the different parts of the currently edited character are parented under*/
    public Transform part_list; /*prefab that has all of the parts as its children*/

    //lists for each body part. Could find a less cumbersome way to store this
    List<PartComponent> hair  = new List<PartComponent>();
    List<PartComponent> heads  = new List<PartComponent>();
    List<PartComponent> torsos = new List<PartComponent>();
    List<PartComponent> legs = new List<PartComponent>();
    List<PartComponent> shoes = new List<PartComponent>();

    PartComponent current_hair;
    PartComponent current_head;
    PartComponent current_torso;
    PartComponent current_legs;
    PartComponent current_shoes;

    Transform hair_parent;
    Transform head_parent;
    Transform torso_parent;
    Transform shoes_parent;
    Transform legs_parent;

    PartUseage gender = PartUseage.MALE;
    PartUseage skin = PartUseage.MEDIUM_SKIN;
    PartUseage happiness = PartUseage.HAPPY;

    List<PartButton> buttons;
	// Use this for initialization

    bool start_called = false;
	void Start () 
    {
        start_called = true;

        foreach (Transform t in part_list)
        {
            if (t.name.Contains("hair"))
                hair.Add(t.GetComponent<PartComponent>());
            else if (t.name.Contains("head"))
                heads.Add(t.GetComponent<PartComponent>());
            else if (t.name.Contains("shirt"))
                torsos.Add(t.GetComponent<PartComponent>());
            else if (t.name.Contains("legs"))
                legs.Add(t.GetComponent<PartComponent>());
            else if (t.name.Contains("shoes"))
                shoes.Add(t.GetComponent<PartComponent>());
        }

        hair_parent = part_parent.FindChild("hair_anchor");
        head_parent = part_parent.FindChild("head_anchor");
        torso_parent = part_parent.FindChild("body_anchor");
        legs_parent = part_parent.FindChild("legs_anchor");
        shoes_parent = part_parent.FindChild("shoes_anchor");

        SetPartsThatFitConstrait(ref current_hair, hair_parent, (int)gender | (int)skin, 0, hair);
        SetPartsThatFitConstrait(ref current_head, head_parent, (int)gender | ((int)skin | (int)happiness), 0, heads);
        SetPartsThatFitConstrait(ref current_torso, torso_parent, (int)gender | (int)skin, 0, torsos);
        SetPartsThatFitConstrait(ref current_legs, legs_parent, (int)gender | (int)skin, 0, legs);
        SetPartsThatFitConstrait(ref current_shoes, shoes_parent, (int)gender | (int)skin, 0, shoes);

        buttons = new List<PartButton>(GameObject.FindObjectsOfType<PartButton>());
        buttons.Sort((a, b) => a.name.CompareTo(b.name));

        GameObject[] scrollings = GameObject.FindGameObjectsWithTag("Scrolling");

        foreach (var g in scrollings)
        {
            g.SetActive(false);
        }

        ResetGUIObjects();

        customiser_ui.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!start_called)
            Start();
    }
	
	// Update is called once per frame
    PartData SetupSaveParts() 
    {
        PartData chosen_parts = new PartData();

        chosen_parts.hair_name = current_hair.name;
        chosen_parts.head_name = current_head.name;
        chosen_parts.torso_name = current_torso.name;
        chosen_parts.legs_name = current_legs.name;
        chosen_parts.shoes_name = current_shoes.name;

        return chosen_parts;
	}

    public void SetAdult()
    {
        part_parent.localScale = new Vector3(1f, 1f, 1f);
        part_parent.position = new Vector3(0, 1, 0);
    }

    public void SetChild()
    {
        part_parent.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        part_parent.position = new Vector3(0, 0.6f, 0);
    }

    public void SetParts()
    {
        if (UIButton.current == null)
            throw new InvalidOperationException("Set part called by function other than UIButton");

        PartButton button = UIButton.current.GetComponent<PartButton>();
        
        switch (button.type)
        {
            case PartType.NONE:
                if (button.constraint == PartUseage.NONE)
                    return;

                if (button.constraint == PartUseage.MALE || button.constraint == PartUseage.FEMALE)
                    gender = button.constraint;
                else
                    skin = button.constraint;

                SetPartsThatFitConstrait(ref current_hair, hair_parent,   (int)gender | (int)skin, 0, hair);
                SetPartsThatFitConstrait(ref current_head, head_parent, (int)gender | (int)skin | (int)happiness, 0, heads);
                SetPartsThatFitConstrait(ref current_torso, torso_parent, (int)gender | (int)skin, 0, torsos);
                SetPartsThatFitConstrait(ref current_legs, legs_parent,   (int)gender | (int)skin, 0, legs);
                SetPartsThatFitConstrait(ref current_shoes, shoes_parent,   (int)gender | (int)skin, 0, shoes);

                ResetGUIObjects();
                break;

            case PartType.HAIR:
                ReplacePart(ref current_hair, hair_parent, button.attached_part);
                break;
            case PartType.HEAD:
                ReplacePart(ref current_head, head_parent, button.attached_part);
                break;
            case PartType.LEGS:
                ReplacePart(ref current_legs, legs_parent, button.attached_part);
                break;
            case PartType.TORSO:
                ReplacePart(ref current_torso, torso_parent, button.attached_part);
                break;
            case PartType.SHOES:
                ReplacePart(ref current_shoes, shoes_parent, button.attached_part);
                break;
        }
    }

    public void Confirm()
    {
        GoToStage();
        PartData new_data = SetupSaveParts();
        movement_controller.AddNewCharacter(new_data);
    }

    public void GoToStage()
    {
        customiser_camera.gameObject.SetActive(false);
        customiser_ui.gameObject.SetActive(false);

        placement_camera.gameObject.SetActive(true);
        placement_ui.gameObject.SetActive(true);
    }

    public void NewCharacter()
    {
        customiser_camera.gameObject.SetActive(true);
        customiser_ui.gameObject.SetActive(true);

        placement_camera.gameObject.SetActive(false);
        placement_ui.gameObject.SetActive(false);
    }

    public void HandleSlider()
    {
        string slider_name = UISlider.current.name;

        if (slider_name == "happiness_slider")
        {
            happiness = UISlider.current.value > 0.5 ? PartUseage.HAPPY : PartUseage.SAD;
            SetPartsThatFitConstrait(ref current_head, head_parent, (int)gender | (int)skin | (int)happiness, 0, heads);

        }
        else if (slider_name == "height_slider")
        {

        }
        else if (slider_name == "size_slider")
        {

        }

    }

    void ReplacePart( ref PartComponent current, Transform current_parent, PartComponent new_part )
    {
        if (new_part == null)
            return;

        if (current != null )
            Destroy(current.gameObject);

        GameObject go = (GameObject)Instantiate(new_part.gameObject);
        go.name = new_part.name;
        go.transform.parent = current_parent;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        current = go.GetComponent<PartComponent>();
    }

    void SetPartsThatFitConstrait(ref PartComponent current, Transform current_parent, int positive_constaints, int negative_constaints, List<PartComponent> available_parts)
    {
        if (current == null || ((current.part_data & positive_constaints) != positive_constaints) || (current.part_data & negative_constaints) != 0)
        {
            foreach (var part in available_parts)
            {
                if ((part.part_data & positive_constaints) == positive_constaints && (part.part_data & negative_constaints) == 0)
                {
                    ReplacePart(ref current, current_parent, part);
                    return;
                }
            }
        }
    }

    void SetAttachedPart(PartButton button, ref int pos, List<PartComponent> parts)
    {
        while (pos < parts.Count && (parts[pos].part_data & ((int)gender | (int)skin)) != ((int)gender | (int)skin)) 
        {
            ++pos;
        }

        if (pos >= parts.Count)
        {
            button.gameObject.SetActive(false);
            return;
        }

        button.attached_part = parts[pos++];
    }

    void ResetGUIObjects()
    {
        int hair_pos = 0;
        int head_pos = 0;
        int torso_pos = 0;
        int legs_pos = 0;
        int shoes_pos = 0;
        
        foreach (PartButton b in buttons)
        {
            b.gameObject.SetActive(true);
            switch (b.type)
            { 
                case PartType.NONE:
                    break;
                case PartType.HAIR:
                    SetAttachedPart(b, ref hair_pos, hair);
                    break;
                case PartType.HEAD:
                    SetAttachedPart(b, ref head_pos, heads);
                    break;
                case PartType.LEGS:
                    SetAttachedPart(b, ref legs_pos, legs);
                    break;
                case PartType.SHOES:
                    SetAttachedPart(b, ref shoes_pos, shoes);
                    break;
                case PartType.TORSO:
                    SetAttachedPart(b, ref torso_pos, torsos);
                    break;
            }
        }
    }
}
