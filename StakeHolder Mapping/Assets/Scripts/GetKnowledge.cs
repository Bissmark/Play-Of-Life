using UnityEngine;
using System.Collections;

public class GetKnowledge : MonoBehaviour
{
	private string currentLabel;
	public string knowledge;
	public GameObject user;

	void Start()
	{
		user = GameObject.FindGameObjectWithTag("UserControl");
		
		this.GetComponent<UILabel> ().text = knowledge;
	}
	void Update()
	{
		this.GetComponent<UILabel> ().text = knowledge;
		/*if(PlayerPrefs.HasKey("Knowledge"))
		{
			currentLabel = PlayerPrefs.GetString("Knowledge");
			this.GetComponent<UILabel>().text = currentLabel;
		}*/
    }
}