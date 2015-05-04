using UnityEngine;
using System.Collections;

public class GetOther : MonoBehaviour
{
	private string currentLabel;
	public string other;
	public GameObject user;
	
	void Start()
	{
		user = GameObject.FindGameObjectWithTag("UserControl");
		
		this.GetComponent<UILabel> ().text = other;
		//Debug.Log (reportName);
	}

	void Update()
	{
		this.GetComponent<UILabel> ().text = other;
		/*if(PlayerPrefs.HasKey("Other"))
		{
			currentLabel = PlayerPrefs.GetString("Other");
			this.GetComponent<UILabel>().text = currentLabel;
		}*/
    }
}