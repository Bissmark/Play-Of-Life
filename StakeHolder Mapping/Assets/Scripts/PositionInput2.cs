using UnityEngine;
using System.Collections;

public class PositionInput2 : MonoBehaviour 
{
	public GameObject user;// = GameObject.FindGameObjectWithTag("UserControl");
	
	void Start () 
	{
		user = GameObject.FindGameObjectWithTag("UserControl");
	}
	
	/*void OnInput() 
	{
		PlayerPrefs.SetString("Report To", GetComponent<UIInput>().value);
		user.GetComponent<UserStorage> ().mainStakeholder.reportPosition = GetComponent<UIInput> ().value;
	}*/

	void Update()
	{
		user.GetComponent<UserStorage> ().mainStakeholder.reportPosition = GetComponent<UIInput> ().value;
	}

}
