using UnityEngine;
using System.Collections;

public class ReportToInput : MonoBehaviour 
{
	public GameObject user;// = GameObject.FindGameObjectWithTag("UserControl");
	
	void Start () 
	{
		user = GameObject.FindGameObjectWithTag("UserControl");
	}

	/*void OnInput() 
	{
		user.GetComponent<UserStorage> ().mainStakeholder.reportName = GetComponent<UIInput> ().value;
	}*/

	void Update()
	{
		user.GetComponent<UserStorage> ().mainStakeholder.reportName = GetComponent<UIInput> ().value;
	}
}
