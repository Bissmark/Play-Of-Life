using UnityEngine;
using System.Collections;
using System.IO;

public class readScreenshot : MonoBehaviour 
{	
	//public UISprite test;
	//UITexture selfScreenshot;
	
	void Start()
	{
		//selfScreenshot = GetComponent<UITexture>();
		
		string filePath = Application.persistentDataPath + "/../SavedScreen.png";
	
		if(File.Exists(filePath))
		{
			//var bytes = File.ReadAllBytes(filePath);
			//Texture2D screenshot = new Texture2D(0, 0, TextureFormat.ARGB32, false);
			//screenshot.LoadImage(bytes);
			//selfScreenshot.mainTexture = screenshot;
		}		
	}
}
