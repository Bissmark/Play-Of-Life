using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class UserStorage : MonoBehaviour
{
	public User mainUser = new User();
	public Stakeholder mainStakeholder = new Stakeholder();

	public string fileName;

	public string tempName;
	public string tempPass;

	public bool isLoaded = false;

	GameObject outputPanel1;
	public GameObject outputMale;
	public GameObject outputFemale;
	public GameObject UIRoot;

	// Use this for initialization
	void Start () 
	{
		mainUser = new User();
		mainUser.SetUser ();
		mainStakeholder.Refresh ();
		Debug.Log ("setup complete");
		//LoadUser ();
	}

	
	// Update is called once per frame
	void Update ()
	{
		/*if (isLoaded) 
		{
			debugX = mainUser.personalList [0].transform.x;
			debugY = mainUser.personalList [0].transform.y;
		}*/
		//debugName = mainUser.userName;
		//debugNameHolder = mainStakeholder.name;
		//debugListSize = mainUser.personalList.Count;
	}

	public void CreateMaleStakeholder()
	{
		Stakeholder temp = new Stakeholder ();
		temp.Refresh ();
		temp.CopyStakeholder (mainStakeholder);
		temp.isMale = true;
		temp.isFemale = false;
		mainUser.AddStakeholder (temp);
	}

	public void CreateFemaleStakeholder()
	{
		Stakeholder temp = new Stakeholder ();
		temp.Refresh ();
		temp.CopyStakeholder (mainStakeholder);
		temp.isMale = false;
		temp.isFemale = true;
		mainUser.AddStakeholder (temp);
	}

	public void PopulateGrid()
	{
		if (mainUser != null) 
		{
			for (int i = 0; i < mainUser.personalList.Count; ++i) 
			{
					mainUser.personalList [i].isUsed = false;
			}
			for (int i = 0; i < mainUser.personalList.Count; ++i) 
			{
				if(mainUser.personalList[i].isMale)
				{
					outputPanel1 = NGUITools.AddChild (GameObject.Find ("UIBackgroundTableArea"), outputMale);
					outputPanel1.transform.localScale = new Vector3 (1f, 1.5f, 1f);
				}
				if(mainUser.personalList[i].isFemale)
				{
					outputPanel1 = NGUITools.AddChild (GameObject.Find ("UIBackgroundTableArea"), outputFemale);
					outputPanel1.transform.localScale = new Vector3 (1f, 1.5f, 1f);
				}
			}
		}
	}

	public void SetUser()
	{
		mainUser.SetUser ();
		mainStakeholder.Refresh ();
		mainUser.personalList = new List<Stakeholder>();
	}

	public void CheckLogin()
	{
		//bool debugBool = LoadUser ();
		//Debug.Log (debugBool);
		if (LoadUser ()) 
		{
			UIRoot.GetComponent<MenuLogicStakeHolder>().MenuPanel();
		}
	}

	public void SaveUser()
	{
		Serializer.Save<User>(fileName, mainUser);
	}
	public bool LoadUser()
	{
		isLoaded = true;
		if (File.Exists (tempName+".bin")) 
		{
			User tempUser = Serializer.Load<User> (tempName+".bin");
			if(tempPass == tempUser.password)
			{
				mainUser = Serializer.Load<User> (tempName+".bin");
				fileName = tempName+".bin";
				return true;
			}
			return false;
		} 
		{
			mainUser = new User(tempName,tempPass);
			fileName = tempName+".bin";
			return true;
		}
	}
}

[System.Serializable]
public class Stakeholder
{
	public Stakeholder()
	{
		transformX = 0f;
		transformY = 0f;
		name = "";
		position = "";
		goals = "";
		reportName = "";
		reportPosition = "";
		reportAlign = "";
		knowledge = "";
		other = "";
		actionPlan = "";
		isMale = false;
		isFemale = false;
	}
	public float transformX = 0f;
	public float transformY = 0f;

	public string name = "";
	public string position = "";
	public string goals = "";

	public bool isMale = false;
	public bool isFemale = false;

	public string reportName = "";
	public string reportPosition = "";
	public string reportAlign = "";

	public string knowledge = "";
	public string other = "";
	public string actionPlan = "";

	public bool isUsed;

	public void Refresh()
	{
		transformX = 0f;
		transformY = 0f;
		name = "";
		position = "";
		goals = "";
		reportName = "";
		reportPosition = "";
		reportAlign = "";
		knowledge = "";
		other = "";
		actionPlan = "";
		isMale = false;
		isFemale = false;
	}

	public void CopyStakeholder(Stakeholder copy)
	{
		transformX = copy.transformX;
		transformY = copy.transformY;
		name = copy.name;
		position = copy.position;
		goals = copy.goals;
		reportName = copy.reportName;
		reportPosition = copy.reportPosition;
		reportAlign = copy.reportAlign;
		knowledge = copy.knowledge;
		other = copy.other;
		actionPlan = copy.actionPlan;
	}
}

[System.Serializable]
public class User
{
	public User()
	{
		userName = "";
		password = "";
		personalList = new List<Stakeholder>();
	}
	public User(string name, string pass)
	{
		userName = name;
		password = pass;
		personalList = new List<Stakeholder>();
	}

	public string userName = "";
	public string password = "";
	public List<Stakeholder> personalList = new List<Stakeholder>();

	public void SetUser()
	{
		userName = "";
		password = "";
		//personalList = new List<Stakeholder>();
	}
	public void AddStakeholder(Stakeholder holder)
	{
		personalList.Add(holder);
	}

	public Stakeholder GetMaleStakeholder()
	{
		for(int i = 0; i < personalList.Count; ++i)
		{
			if(!personalList[i].isUsed&& !personalList[i].isFemale)
			{
				personalList[i].isUsed = true;
				return personalList[i];
			}
		}
		Stakeholder temp = new Stakeholder();
		temp.Refresh();
		return temp;//this is terrible, needs to be changed
	}
	public Stakeholder GetFemaleStakeholder()
	{
		for(int i = 0; i < personalList.Count; ++i)
		{
			if(!personalList[i].isUsed && !personalList[i].isMale)
			{
				personalList[i].isUsed = true;
				return personalList[i];
			}
		}
		Stakeholder temp = new Stakeholder();
		temp.Refresh();
		return temp;//this is terrible, needs to be changed
	}
}


public class Serializer
{
	public static T Load<T>(string filename) where T: class
	{
		if (File.Exists(filename))
		{
			try
			{
				using (Stream stream = File.OpenRead(filename))
				{
					BinaryFormatter formatter = new BinaryFormatter();
					return formatter.Deserialize(stream) as T;
				}
			}
			catch (Exception e)
			{
				Debug.Log(e.Message);
			}
		}
		return default(T);
	}
	
	public static void Save<T>(string filename, T data) where T: class
	{
		using (Stream stream = File.OpenWrite(filename))
		{   
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, data);
		}
	}
}
