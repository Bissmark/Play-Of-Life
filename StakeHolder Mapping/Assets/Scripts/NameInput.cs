using UnityEngine;
using System.Collections;

public class NameInput : MonoBehaviour 
{
	public GameObject user;// = GameObject.FindGameObjectWithTag("UserControl");

	void Start () 
	{
		user = GameObject.FindGameObjectWithTag("UserControl");
	}

	/*void OnInput() 
	{
		PlayerPrefs.SetString("Player Name", GetComponent<UIInput> ().value);
		user.GetComponent<UserStorage> ().mainStakeholder.name = GetComponent<UIInput> ().value;

	}*/
	void Update()
	{
		user.GetComponent<UserStorage> ().mainStakeholder.name = GetComponent<UIInput> ().value;
	}
}
