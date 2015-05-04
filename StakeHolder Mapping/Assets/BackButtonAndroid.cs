using UnityEngine;
using System.Collections;

public class BackButtonAndroid : MonoBehaviour 
{
	public MenuLogicStakeHolder previousMenu;
	
	// Update is called once per frame
	void Update () 
	{
		if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                // Insert Code Here (I.E. Load Scene, Etc)
				previousMenu.MenuPanel();
                //Application.Quit();
            }
		}
	}
}
