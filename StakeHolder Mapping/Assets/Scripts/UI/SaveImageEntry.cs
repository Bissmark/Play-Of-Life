using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveImageEntry : MonoBehaviour 
{
    public void OnButtonClick()
    {
        DebugUI.Instance.OnSaveImageEntryClick( GetComponentInChildren<Text>().text );
    }
}
