using UnityEngine;
using System.Collections;

public class GetReportAlign : MonoBehaviour 
{
	private string currentLabel;
	public string reportAlign;
	public GameObject user;
	
	void Start()
	{
		user = GameObject.FindGameObjectWithTag("UserControl");
		
		this.GetComponent<UILabel> ().text = reportAlign;
	}
	void Update()
	{
		this.GetComponent<UILabel> ().text = reportAlign;

	}
}
