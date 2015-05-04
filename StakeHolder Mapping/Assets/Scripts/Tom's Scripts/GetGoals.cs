using UnityEngine;
using System.Collections;

public class GetGoals : MonoBehaviour
{
	private string currentLabel;
	//public GameObject labelUsed;
	public string goals;
	public GameObject user;
	
	void Start()
	{
		user = GameObject.FindGameObjectWithTag("UserControl");
		
		this.GetComponent<UILabel> ().text = goals;
		Debug.Log (goals);
	}
	
	void Update()
	{
		this.GetComponent<UILabel> ().text = goals;
		/*if(PlayerPrefs.HasKey("Player Name"))
		{
			currentLabel = PlayerPrefs.GetString("Player Name");
			labelUsed.GetComponent<UILabel>().text = currentLabel;
		}*/
	}
}
