using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//using Holoville.HOTween;

public class MovementController : MonoBehaviour 
{

    public Transform spawn_pos;

    public Transform stage;

    public Transform part_parent_prefab;

    public Transform selection_ring;

    public Transform stage_camera_pos;

	public Button testButton;

    /*prefab that has all of the parts as its children*/
    public Transform part_list;

    public enum MoveState
    {
        UNSELECTED,
        SELECTED,
        ROTATING,
        POSING,
        MOVING
    }

    MoveState curr_state = MoveState.UNSELECTED;

    Transform curr_selected = null;


    /* parenet transform that the different parts of the currently edited character are parented under */
    List<PartData> part_parents = new List<PartData>(); 

    delegate void TransformStringDelegate(Transform t, string s);

    bool start_called = false;

    public void Start()
    {
        start_called = true;
        Load();
    }

    // Use this for initialization
	public void AddNewCharacter (PartData data)
    {
        Transform new_part_parent = ((GameObject)Instantiate(part_parent_prefab.gameObject)).transform;
        new_part_parent.name = "character";

        Transform hair_parent = new_part_parent.FindChild("hair_anchor");
        Transform head_parent = new_part_parent.FindChild("head_anchor");
        Transform torso_parent = new_part_parent.FindChild("body_anchor");
        Transform legs_parent = new_part_parent.FindChild("legs_rotation");

        Transform shoes_parent = legs_parent.FindChild("shoes_anchor");

        legs_parent = legs_parent.FindChild("legs_anchor");

        TransformStringDelegate setup = (Transform parent, string child_name) =>
        {
            GameObject go = (GameObject)Instantiate(part_list.FindChild(child_name).gameObject);
            go.transform.parent = parent;
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
        };

        setup(hair_parent, data.hair_name);
        setup(head_parent, data.head_name);
        setup(torso_parent, data.torso_name);
        setup(legs_parent, data.legs_name);
        setup(shoes_parent, data.shoes_name);

        Transform leg_rotator = legs_parent.parent;
        leg_rotator.rotation = Quaternion.LookRotation(Vector3.Cross(data.leg_up, leg_rotator.right), data.leg_up);
        new_part_parent.position = data.pos == Vector3.zero ? spawn_pos.position : data.pos;
        new_part_parent.rotation = data.main_rotation;

        data.character_parent = new_part_parent;
		new_part_parent.parent = stage;

        part_parents.Add(data);

		Debug.Log (new_part_parent);
	}

    public void SaveAndQuit()
    {
        Save();
        Application.LoadLevel("SHMAndroid");
    }

    public void Save()
    {
        foreach (var d in part_parents)
        {
            Transform leg_rotator = d.character_parent.FindChild("legs_rotation");
            d.leg_up = leg_rotator.up;
            d.main_rotation = d.character_parent.rotation;
            d.pos = d.character_parent.position;
        }

        XmlSerializer ser = new XmlSerializer(typeof(List<PartData>));
        ser.Serialize(new FileStream(Application.persistentDataPath + "/save_game.xml", FileMode.Create), part_parents);
    }

    public void Load()
    {
        if (!File.Exists(Application.persistentDataPath + "/save_game.xml"))
        {
            Debug.Log("No File Found");
            return;
        }

        Debug.Log("Found File");

        XmlSerializer ser = new XmlSerializer(typeof(List<PartData>));
        List<PartData> data = (List<PartData>)ser.Deserialize(new FileStream(Application.persistentDataPath + "/save_game.xml", FileMode.Open));

        Debug.Log("data count = " + data.Count.ToString());

        foreach (var d in data)
        {
            Debug.Log("adding char");
            AddNewCharacter(d);
        }
    }

    float touch_timer = 0;
	
	// Update is called once per frame
	void Update ()
    {
        if (!start_called)
            Start();

        if (TouchHandler.touchCount == 0)
        {
            touch_timer = 0;
            return;
        }

        touch_timer += Time.deltaTime;

        Ray r = Camera.main.ScreenPointToRay(TouchHandler.GetTouch(0).position);
        SimpleTouch t = TouchHandler.GetTouch(0);
        RaycastHit info;

        switch (curr_state)
        {
            case MoveState.UNSELECTED:
            {
                if (t.phase == TouchPhase.Began)
                {
                    if (Physics.Raycast(r, out info) && info.collider.tag == "Part")
                    {
                        curr_selected = info.collider.transform;
                        while (curr_selected != null && curr_selected.name != "character")
                            curr_selected = curr_selected.parent;

                        if (curr_selected == null)
                            return;

                        curr_state = MoveState.SELECTED;
                        return;
                    }
                }
                break;
            }
            case MoveState.SELECTED:
            {
                if (t.phase == TouchPhase.Began)
                {
                    RaycastHit info2;
                    if (!Physics.Raycast(r, out info2))
                    {
                        curr_selected = null;
                        curr_state = MoveState.UNSELECTED;
                    }
                    else if (selection_ring.GetComponent<Collider>().Raycast(r, out info, 100))
                    {
                        curr_state = MoveState.ROTATING;
                    }
                    else if (info2.collider.tag == "Part")
                    {
                        Transform character = info2.collider.transform;
                        while (character != null && character.name != "character")
                            character = character.parent;

                        curr_selected = character;
                    }
                    else if (stage.GetComponent<Collider>().Raycast(r, out info, 100))
                    {
                        curr_state = MoveState.MOVING;
                    }
                }
                break;
            }
            case MoveState.MOVING:
            {
                if (touch_timer < 0.1)
                    return;

                if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary) 
                {
                    if (stage.GetComponent<Collider>().Raycast(r, out info, 100))
                    {
                        Vector3 target = info.point;
                        target.y = curr_selected.transform.position.y;
                        curr_selected.transform.position = Vector3.Lerp(curr_selected.transform.position, target, Time.deltaTime * 5);
                        curr_selected.transform.forward = (target - curr_selected.transform.position).normalized;
                    }
                }
                else
                {
                    curr_state = MoveState.SELECTED;
                }
                break;
            }
            case MoveState.ROTATING:
            {
                if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)
                {
                    if (stage.GetComponent<Collider>().Raycast(r, out info, 100))
                    {
                        Vector3 target = info.point;
                        target.y = curr_selected.transform.position.y;
                        curr_selected.transform.forward = (target - curr_selected.transform.position).normalized;
                    }
                }
                else
                {
                    curr_state = MoveState.SELECTED;
                }

                break;
            }
            case MoveState.POSING:
            {
                if (touch_timer < 0.1)
                    return;

                Transform leg_rotator = curr_selected.FindChild("legs_rotation");

                Plane p = new Plane(curr_selected.right, leg_rotator.position);
                float dist;
                p.Raycast(r, out dist);
                
                Vector3 new_up = (leg_rotator.position - r.GetPoint(dist)).normalized;

                leg_rotator.rotation = Quaternion.LookRotation(Vector3.Cross(new_up, leg_rotator.right), new_up);
                
                break;
            }
        }

        if (curr_selected != null)
        {
            selection_ring.gameObject.SetActive(true);
            Ray r_at_stage = new Ray(curr_selected.position, Vector3.down);

            stage.GetComponent<Collider>().Raycast(r_at_stage, out info, 100);

            selection_ring.transform.position = info.point + Vector3.up * 0.01f;
        }
        else
        {
            selection_ring.gameObject.SetActive(false);
        }
	}


    public void TogglePosing()
    {
        if (curr_selected == null)
            return;

		Text label = testButton.transform.GetChild(0).GetComponent<Text>();
        if ( curr_state == MoveState.POSING )
        {
            label.text = "Change Pose";

            //HOTween.To(Camera.main.transform, 0.5f, new TweenParms().Prop("position", stage_camera_pos.position).Ease(EaseType.EaseOutQuad));
            //HOTween.To(Camera.main.transform, 0.5f, new TweenParms().Prop("rotation", stage_camera_pos.rotation).Ease(EaseType.EaseOutQuad));

            foreach (var d in part_parents)
            {
                d.character_parent.gameObject.SetActive(true);
            }

            curr_state = MoveState.SELECTED;
        }
        else
        {
            label.text = "Return to Stage";
            Transform cam_child = curr_selected.FindChild("pose_camera");

            foreach (var d in part_parents)
            {
                if (d.character_parent == curr_selected)
                    continue;

                d.character_parent.gameObject.SetActive(false);
            }

            //HOTween.To(Camera.main.transform, 0.5f, new TweenParms().Prop("position", cam_child.position).Ease(EaseType.EaseOutQuad).OnComplete(()=>curr_state = MoveState.POSING));
           // HOTween.To(Camera.main.transform, 0.5f, new TweenParms().Prop("rotation", cam_child.rotation).Ease(EaseType.EaseOutQuad).OnComplete(()=>curr_state = MoveState.POSING));

        }
    }

}
