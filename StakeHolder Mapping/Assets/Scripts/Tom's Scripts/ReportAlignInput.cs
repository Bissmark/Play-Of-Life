﻿using UnityEngine;
using System.Collections;

public class ReportAlignInput : MonoBehaviour
{

	public GameObject user;// = GameObject.FindGameObjectWithTag("UserControl");
	
	void Start () 
	{
		user = GameObject.FindGameObjectWithTag("UserControl");
	}
	
	/*void OnInput() 
	{
		user.GetComponent<UserStorage> ().mainStakeholder.reportAlign = GetComponent<UIInput> ().value;
	}*/
	void Update()
	{
		user.GetComponent<UserStorage> ().mainStakeholder.reportAlign = GetComponent<UIInput> ().value;
	}
}
