using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PartComponent))]
public class PartFlagsEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        PartComponent my_target = target as PartComponent;
        my_target.OnInspector();
    }

}
