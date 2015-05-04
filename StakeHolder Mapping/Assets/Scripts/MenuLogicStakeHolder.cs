using UnityEngine;
using System.Collections;

public class MenuLogicStakeHolder : MonoBehaviour
{
	public GameObject inputPanel;
	public GameObject adminTablePanel;
	public GameObject viewerTablePanel;
	public GameObject menuPanel;
	public GameObject loginPanel;
	public GameObject graph;
	//public GameObject playoflifeLevel;
	
	private GameObject activeMenu;
	private GameObject prevMenu;
	
	void Awake() 
	{
		activeMenu = loginPanel;
	}
	
	void ChangeMenu(GameObject newMenu) 
	{
		prevMenu = activeMenu;
		NGUITools.SetActive(activeMenu, false);
		activeMenu = newMenu;
		NGUITools.SetActive(activeMenu, true);
	}
	
	public void InputPanel() 
	{
		ChangeMenu(inputPanel);
	}
	
	public void AdminTablePanel() 
	{
		ChangeMenu(adminTablePanel);
	}
	
	public void ViewerTablePanel() 
	{
		ChangeMenu(viewerTablePanel);
	}
	
	public void MenuPanel() 
	{
		ChangeMenu(menuPanel);
	}
	
	public void LoginPanel() 
	{
		ChangeMenu(loginPanel);
	}
	
	public void Back() 
	{
		ChangeMenu(prevMenu);
	}

	public void PlayofLife()
	{
		Application.LoadLevel ("PlayofLife");
	}
	
	public void Exit()
	{
		Application.Quit();
	}
}