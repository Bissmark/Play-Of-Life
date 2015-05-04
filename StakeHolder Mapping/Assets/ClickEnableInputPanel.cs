using UnityEngine;
using System.Collections;

public class ClickEnableInputPanel : MonoBehaviour 
{
	public GameObject inputPanelRequired;
	public GameObject removePreviousPanel;
	public GameObject rootParent;

	private GameObject activeMenu;
	private GameObject prevMenu;
	
	void ChangeMenu(GameObject newMenu) 
	{
		//NGUITools.SetActive(removePreviousPanel, false);
		prevMenu = activeMenu;
		//NGUITools.SetActive(activeMenu, false);
		activeMenu = newMenu;
		//NGUITools.SetActive(activeMenu, true);
	}

	public void CreateInput()
	{
		//GameObject TestPanel = NGUITools.AddChild(rootParent, inputPanelRequired);
	}

	public void InputPanel() 
	{
		ChangeMenu(inputPanelRequired);
	}
}