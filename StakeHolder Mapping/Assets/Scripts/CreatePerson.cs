using UnityEngine;
using System.Collections;

public class CreatePerson : MonoBehaviour 
{
	public GameObject rootParent1;
	public GameObject outputMale;
	public GameObject outputFemale;

	public GameObject user;// = GameObject.FindGameObjectWithTag ("UserControl");
	
	//public UIToggle maleToggle;
	//public UIToggle femaleToggle;
	
	GameObject outputPanel1;

	void Start()
	{
		user = GameObject.FindGameObjectWithTag ("UserControl");
	}

	void OnClick() 
    {
		//if(maleToggle.isChecked)
		//{
			//outputPanel1 = NGUITools.AddChild(GameObject.Find("UIBackgroundTableArea"), outputMale);
			//outputPanel1.transform.localScale = new Vector3(1f, 1.5f, 1f);
			//user.GetComponent<UserStorage> ().CreateMaleStakeholder ();
		//}
		//else if (femaleToggle.isChecked)
		//{
			//outputPanel1 = NGUITools.AddChild(GameObject.Find("UIBackgroundTableArea"), outputFemale);
			//outputPanel1.transform.localScale = new Vector3(1f, 1.5f, 1f);
			//user.GetComponent<UserStorage> ().CreateFemaleStakeholder ();
		//}


	}
}