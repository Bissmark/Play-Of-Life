using UnityEngine;
using System.Collections;

public class ActivateOptions : MonoBehaviour 
{
	public GameObject scrollingOption;
	
	public void ShowOptions() 
	{
		if (scrollingOption.activeSelf == false)
			scrollingOption.SetActive (true);
		else
			scrollingOption.SetActive (false);
	}
}
