using UnityEngine;
using System.Collections;

public class GetActionPlan : MonoBehaviour
{
	private string currentLabel;
	public string actionPlan;
	public GameObject user;
	
	void Start()
	{
		user = GameObject.FindGameObjectWithTag("UserControl");
		
		this.GetComponent<UILabel> ().text = actionPlan;
		//Debug.Log (reportName);
	}
	void Update()
	{
		this.GetComponent<UILabel> ().text = actionPlan;
		/*if(PlayerPrefs.HasKey("Action Plan"))
		{
			currentLabel = PlayerPrefs.GetString("Action Plan");
			this.GetComponent<UILabel>().text = currentLabel;
		}*/
    }
}