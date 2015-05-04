using UnityEngine;
using System.Collections;

public class BringBackAdmin : MonoBehaviour 
{
	private CreatePerson adminPanelDisable;
	private GameObject targetRoot;

	void Start()
	{		
		adminPanelDisable = GetComponent<CreatePerson>();
		targetRoot = GameObject.FindGameObjectWithTag("Test");
		adminPanelDisable.rootParent1 = targetRoot;
	}
}
