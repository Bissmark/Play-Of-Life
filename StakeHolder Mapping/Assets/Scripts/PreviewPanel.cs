using UnityEngine;
using System.Collections;

public class PreviewPanel : MonoBehaviour 
{
	public GameObject previewPanel;

	void Start()
	{
		NGUITools.SetActive(previewPanel, false);
	}

	void OnPress(bool isPressed)
	{
		if(isPressed)
		{
			NGUITools.SetActive(previewPanel, true);
		}
		else
		{
			NGUITools.SetActive(previewPanel, false);
		}
	}
}
