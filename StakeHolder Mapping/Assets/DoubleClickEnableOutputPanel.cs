using UnityEngine;
using System.Collections;

public class DoubleClickEnableOutputPanel : MonoBehaviour 
{
	public GameObject outputPanelRequired;
	public GameObject ViewerTablePanel;

	private GameObject activeMenu;
	private GameObject prevMenu;
	
	void ChangeMenu(GameObject newMenu) 
	{
		//NGUITools.SetActive(ViewerTablePanel, false);
		prevMenu = activeMenu;
		//NGUITools.SetActive(activeMenu, false);
		activeMenu = newMenu;
		//NGUITools.SetActive(activeMenu, true);
	}

	public void OutputPanel() 
	{
		ChangeMenu(outputPanelRequired);
	}
}