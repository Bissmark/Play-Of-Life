using UnityEngine;
using System.Collections;

public class PositionOutput : MonoBehaviour 
{
	public GameObject outputPanel;
	
	// Use this for initialization
	void OnEnable()
	{
		outputPanel.transform.position = new Vector3(0,0,0);
	}
}
