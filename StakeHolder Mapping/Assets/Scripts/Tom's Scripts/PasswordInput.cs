using UnityEngine;
using System.Collections;

public class PasswordInput : MonoBehaviour 
{

	public GameObject user;
	/*public string password;

	void Start()
	{

	}
	void OnInput() 
	{
		user.GetComponent<UserStorage> ().tempPass = password;
	}*/

	void Update()
	{
		user.GetComponent<UserStorage> ().tempPass = GetComponent<UIInput> ().value;
	}
}
