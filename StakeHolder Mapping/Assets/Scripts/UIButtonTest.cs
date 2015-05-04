using UnityEngine;

public class UIButtonTest : MonoBehaviour
{
	private UIButtonMessage1 testScript;
	private GameObject targetRoot;
	
	public GameObject inputPanelSetActive;

	void Start()
	{
		testScript = GetComponent<UIButtonMessage1>();
		targetRoot = GameObject.FindGameObjectWithTag("Test");
		testScript.target = targetRoot;
	}
	
	void OnClick()
	{
		NGUITools.Destroy(inputPanelSetActive);
	}
}