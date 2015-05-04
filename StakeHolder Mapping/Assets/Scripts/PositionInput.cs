using UnityEngine;
using System.Collections;

public class PositionInput : MonoBehaviour 
{

	public GameObject user;// = GameObject.FindGameObjectWithTag("UserControl");
	
	void Start () 
	{
		user = GameObject.FindGameObjectWithTag("UserControl");
	}

/*	void OnInput() 
	{
		PlayerPrefs.SetString("Position", GetComponent<UIInput>().value);
		user.GetComponent<UserStorage> ().mainStakeholder.position = GetComponent<UIInput> ().value;
	}*/

	void Update()
	{
		user.GetComponent<UserStorage> ().mainStakeholder.position = GetComponent<UIInput> ().value;
	}
}
