using UnityEngine;
using System.Collections;

public class GetReportName : MonoBehaviour
{
	private string currentLabel;
	public string reportName;
	public GameObject user;
	
	void Start()
	{
		user = GameObject.FindGameObjectWithTag("UserControl");
		
		this.GetComponent<UILabel> ().text = reportName;
		//Debug.Log (reportName);
	}
	void Update()
    {
		this.GetComponent<UILabel> ().text = reportName;
		/*if(PlayerPrefs.HasKey("Report To"))
		{
			currentLabel = PlayerPrefs.GetString("Report To");
			this.GetComponent<UILabel>().text = currentLabel;
		}*/
    }
}