using UnityEngine;
using System.Collections;

public class SetName : MonoBehaviour 
{
	public GameObject inputField;
	public GameObject outputField;
    
    //void OnEnable()
    //{
     //   UIEventListener.Get(InputField).onSubmit += OnSubmit;	
    //}
    
    void Start()
    {
        if(PlayerPrefs.HasKey("Player Name"))
        {
			//outputField.GetComponent<UILabel>().text = PlayerPrefs.GetString("Player Name");
            //inputField.GetComponent<UIInput>().label.text = PlayerPrefs.GetString("Player Name");
        }
    }
    
    void OnSubmit(GameObject go)
    {
        if(PlayerPrefs.HasKey("Player Name"))
        {
            //PlayerPrefs.SetString("Player Name", outputField.GetComponent<UILabel>().text);
			Debug.Log("Herro");
			go.GetComponent<UILabel>().text = PlayerPrefs.GetString("Player Name");
            //PlayerControl.Name = PlayerPrefs.GetString("Player Name");
        }
        else
        {
            Debug.Log("No key");	
        }
        
        Debug.Log("Name: " + PlayerPrefs.GetString("Player Name"));
    }
}


	//public GameObject nameInputField;
	//public GameObject nameBox;
	
	// Use this for initialization
	//void Start () 
	//{
	//	if(PlayerPrefs.HasKey("Player Name"))
    //    {
    //        //nameInputField.GetComponent<UIInput>().label.text = PlayerPrefs.GetString("Player Name");
	//		PlayerPrefs.SetString("Player Name", nameInputField.GetComponent<UIInput>().text);
	//		label.text = PlayerPrefs.GetString("Player Name");
			//Debug.Log("Player Name");
    //    }
	//}
//}

//PlayerPrefs.SetString("Player Name", position2InputField.GetComponent<UIInput>().text);