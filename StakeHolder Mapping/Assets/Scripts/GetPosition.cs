using UnityEngine;
using System.Collections;

public class GetPosition : MonoBehaviour
{
	private string currentLabel;
	public string position;
	public GameObject user;
	
	void Start()
	{
		user = GameObject.FindGameObjectWithTag("UserControl");
		
		this.GetComponent<UILabel> ().text = position;
		Debug.Log (position);
	}
	
	void Update()
    {
		this.GetComponent<UILabel> ().text = position;

		/*if(PlayerPrefs.HasKey("Position"))
		{
			currentLabel = PlayerPrefs.GetString("Position");
			this.GetComponent<UILabel>().text = currentLabel;
		}*/
    }
}