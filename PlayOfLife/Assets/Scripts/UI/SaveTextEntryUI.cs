using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveTextEntryUI : MonoBehaviour 
{
    public void OnButtonClick()
    {
        DebugUI.Instance.OnSaveTextEntryClick( GetComponentInChildren<Text>().text );
    }
}
