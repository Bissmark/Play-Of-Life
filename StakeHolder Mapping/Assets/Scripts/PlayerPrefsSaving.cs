using UnityEngine;
using System.Collections;

public class PlayerPrefsSaving : MonoBehaviour 
{
	public GameObject nameInputField;
	public GameObject positionInputField;
	public GameObject position2InputField;
	public GameObject goalsWithInputField;
	public GameObject reportInputField;
	public GameObject actionPlanInputField;
	public GameObject knowledgeInputField;
	public GameObject otherInputField;

	void OnClick()
	{
        if(PlayerPrefs.HasKey("Player Name"))
        {
            PlayerPrefs.SetString("Player Name", nameInputField.GetComponent<UIInput>().value);
        }
        else if(PlayerPrefs.HasKey("Position"))
        {
            PlayerPrefs.SetString("Position", positionInputField.GetComponent<UIInput>().value);
        }
		else if(PlayerPrefs.HasKey("Position2"))
        {
            PlayerPrefs.SetString("Position2", position2InputField.GetComponent<UIInput>().value);
        }
		else if(PlayerPrefs.HasKey("Goals With"))
        {
            PlayerPrefs.SetString("Goals With", goalsWithInputField.GetComponent<UIInput>().value);
        }
		else if(PlayerPrefs.HasKey("Report To"))
        {
            PlayerPrefs.SetString("Report To", reportInputField.GetComponent<UIInput>().value);
        }
		else if(PlayerPrefs.HasKey("Action Plan"))
        {
            PlayerPrefs.SetString("Action Plan", actionPlanInputField.GetComponent<UIInput>().value);
        }
		else if(PlayerPrefs.HasKey("Knowledge"))
        {
            PlayerPrefs.SetString("Knowledge", knowledgeInputField.GetComponent<UIInput>().value);
        }
		else if(PlayerPrefs.HasKey("Other"))
        {
            PlayerPrefs.SetString("Other", otherInputField.GetComponent<UIInput>().value);
        }
		else
        {
            Debug.Log("No key");
        }
	}
}