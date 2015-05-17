using UnityEngine;
using System.Collections;

public class MainCanvas : MonoBehaviour
{
    public void OnSaveButtonClick()
    {
        SaveLoadManager.Instance.SaveScene(string.Empty);
    }
    public void OnScreenshotButtonClick()
    {
        SaveLoadManager.Instance.SaveScreenshot();
    }

}
