using UnityEngine;
using System.Collections;
using System.IO;

public class Screenshot : MonoBehaviour 
{
	//public GameObject _UI_root;

	public IEnumerator TakeScreen(string filePath)
	{
		//_UI_root.SetActive(false);
		// We should only read the screen buffer after rendering is complete
		yield return new WaitForEndOfFrame();
		// Create a texture the size of the screen, RGB24 format
		var width = Screen.width;
		var height = Screen.height;
		Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
		// Read screen contents into the texture
		screenshot.ReadPixels (new Rect(0, 0, width, height), 0, 0);
		
		yield return 0;
		
		screenshot.Apply ();
		
		// Encode texture into PNG
		byte[] bytes = screenshot.EncodeToPNG();
		//Destroy (screenshot);
		// For testing purposes, also write to a file in the project folder
		//File.WriteAllBytes(Application.persistentDataPath + filePath, bytes);
		//_UI_root.SetActive(true);
	}
	
	void OnClick()
	{
		//StartCoroutine(TakeScreen("/SavedScreen" + System.DateTime.Now.ToFileTime() + ".png"));
	}
}