using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum PartUseage
{ 
    NONE = 0,

    MALE = 1,
    FEMALE = 2,
    DARK_SKIN = 4,
    MEDIUM_SKIN = 8,
    LIGHT_SKIN = 16,
    HAPPY = 32,
    SAD = 64,
};

public enum PartType
{ 
    NONE,
    HAIR,
    HEAD,
    TORSO,
    LEGS,
    SHOES
}

public class PartComponent : MonoBehaviour 
{
    public int part_data;
    PartType type;

#if UNITY_EDITOR
    delegate void SetFlag(string s, PartUseage d);

    public void OnInspector()
    {
        //create a little lambda that makes it easy to set each flag
        SetFlag SetBool = ((string message, PartUseage flag) =>
        {
            bool value = GUILayout.Toggle((part_data & (int)flag) != 0, message);
            if (value)
                part_data |= (int)flag;
            else
                part_data &= ~(int)flag;
        });

        //let the user set each flag
        SetBool("Male Can Use", PartUseage.MALE);
        SetBool("Female Can Use", PartUseage.FEMALE);
        SetBool("Dark Skin Can Use", PartUseage.DARK_SKIN);
        SetBool("Medium Skin Can Use", PartUseage.MEDIUM_SKIN);
        SetBool("Light Skin Can Use", PartUseage.LIGHT_SKIN);
        SetBool("Happy Can Use", PartUseage.HAPPY);
        SetBool("Sad Can Use", PartUseage.SAD);
    }
#endif
}


