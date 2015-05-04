using UnityEngine;
using System.Collections;

public class UsernameInput : MonoBehaviour 
{
	public GameObject user;
	/*public string userName;
	private UILabel temp;

	void Start()
	{
		temp = GetComponent<UIInput> ().label;
	}
	void OnInput() 
	{
		user.GetComponent<UserStorage> ().tempName = userName;
	}*/
	void Update()
	{
		user.GetComponent<UserStorage> ().tempName = GetComponent<UIInput> ().value;
	}
}
