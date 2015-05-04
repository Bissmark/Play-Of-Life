using UnityEngine;
using System.Collections;

public class GetReportPosition : MonoBehaviour
{
	private string currentLabel;
	public string reportPosition;
	public GameObject user;
	
	void Start()
	{
		user = GameObject.FindGameObjectWithTag("UserControl");
		
		this.GetComponent<UILabel> ().text = reportPosition;
		//Debug.Log (reportName);
	}

	void Update()
    {
		this.GetComponent<UILabel> ().text = reportPosition;
		/*if(PlayerPrefs.HasKey("Position2"))
		{
			currentLabel = PlayerPrefs.GetString("Position2");
			this.GetComponent<UILabel>().text = currentLabel;
		}*/
    }
}