using UnityEngine;
using System.Collections;

public class ResetPlayerprefs : MonoBehaviour
{	
	void OnClick()
    {
		PlayerPrefs.DeleteAll();
		Debug.Log("Playerprefs Deleted");
    }
}