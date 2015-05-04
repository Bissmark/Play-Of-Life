using UnityEngine;
using System.Collections;
using System.IO;


public class ScreenshotLoader : MonoBehaviour {

	public Transform _screenshot_parent;
	public GameObject _preview_prefab;

	// Use this for initialization
	void Start () 
	{
		LoadScreens();
	}

	string[] GetScreenShotNames()
	{
		return Directory.GetFiles(Application.persistentDataPath, "SavedScreen*"); ;
	}

	public void LoadScreens()
	{
		string[] filenames = GetScreenShotNames();

		if (filenames.Length == 0)
			return;

		int i = 0;
		foreach (string s in filenames)
		{
			FileStream fs = new FileStream(s, FileMode.Open, FileAccess.Read);
			byte[] imageData = new byte[fs.Length];
			fs.Read(imageData, 0, (int)fs.Length);

			Texture2D texture = new Texture2D(4, 4);
			texture.LoadImage(imageData);

			GameObject go = (GameObject)GameObject.Instantiate(_preview_prefab);
			go.transform.parent = _screenshot_parent;
			go.transform.localScale = Vector3.one;
			//go.transform.localPosition = new Vector3(_screenshot_parent.GetComponent<UIWrapContent>().itemSize * i, 0, 0);

			go.transform.GetChild(0).GetComponent<UITexture>().mainTexture = texture;
			fs.Close();
			++i;
		}
	}
}
