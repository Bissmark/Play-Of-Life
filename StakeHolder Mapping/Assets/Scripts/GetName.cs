using UnityEngine;
using System.Collections;

public class GetName : MonoBehaviour
{
	private string currentLabel;
	//public GameObject labelUsed;
	public string name;
	public GameObject user;

	void Start()
	{
		user = GameObject.FindGameObjectWithTag("UserControl");

		this.GetComponent<UILabel> ().text = name;
	}

	void Update()
    {
		this.GetComponent<UILabel> ().text = name;
		/*if(PlayerPrefs.HasKey("Player Name"))
		{
			currentLabel = PlayerPrefs.GetString("Player Name");
			labelUsed.GetComponent<UILabel>().text = currentLabel;
		}*/
    }
}