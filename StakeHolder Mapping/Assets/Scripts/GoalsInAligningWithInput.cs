using UnityEngine;
using System.Collections;

public class GoalsInAligningWithInput : MonoBehaviour 
{
	public GameObject user;// = GameObject.FindGameObjectWithTag("UserControl");
	
	void Start () 
	{
		user = GameObject.FindGameObjectWithTag("UserControl");
	}

	/*void OnInput() 
	{
		PlayerPrefs.SetString("Goals With", GetComponent<UIInput>().value);
		user.GetComponent<UserStorage> ().mainStakeholder.goals = GetComponent<UIInput> ().value;
	}*/

	void Update()
	{
		user.GetComponent<UserStorage> ().mainStakeholder.goals = GetComponent<UIInput> ().value;
	}
}
