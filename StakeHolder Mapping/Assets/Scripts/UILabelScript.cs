using UnityEngine;
using System.Collections.Generic;

public class UILabelScript : MonoBehaviour
{
	//private CreateNameBox testScript;
	//private GameObject targetRoot;
	//private GameObject targetName;
	
	GameObject[] inputBox = new GameObject[8];

	void Start()
	{
		//testScript = GetComponent<CreateNameBox>();
		//targetRoot = GameObject.FindGameObjectWithTag("AdminTest");
		//testScript.rootParent = targetRoot;
		
		inputBox[0] = GameObject.Find("UILabelActionPlan");
		inputBox[1] = GameObject.Find("UILabelName");
		inputBox[2] = GameObject.Find("UILabelNameAlign");
		inputBox[3] = GameObject.Find("UILabelOther");
		inputBox[4] = GameObject.Find("UILabelPosition");
		inputBox[5] = GameObject.Find("UILabelReportAlign");
		inputBox[6] = GameObject.Find("UILabelReportPosition");
		inputBox[7] = GameObject.Find("UILabelReportToName");
	}
	
	void OnClick()
	{
		//Debug.Log(inputBox[6]);
	
		byte[] saveData = LevelSerializer.SaveObjectTree(inputBox[0]);
		string saveString = System.Convert.ToBase64String(saveData);
		PlayerPrefs.SetString("Name", saveString);
		
		
		
		Debug.Log(saveString);
	
		//foreach(GameObject testBox in inputBox)
		//{
		//	byte[] saveData = LevelSerializer.SaveObjectTree(testBox);
		//	Debug.Log(testBox);
		//}		
		//JSONLevelSerializer.SaveDataToPlayerPrefs();
	}
}