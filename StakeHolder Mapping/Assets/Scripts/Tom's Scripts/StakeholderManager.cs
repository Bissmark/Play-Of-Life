using UnityEngine;
using System.Collections;

public class StakeholderManager : MonoBehaviour 
{
	public GameObject user;
	public Stakeholder currentHolder;
	public GameObject parent;

	public GameObject nameLabel;
	public GameObject positionLabel;
	public GameObject goalsLabel;

	public GameObject reportNameLabel;
	public GameObject reportPositionLabel;
	public GameObject reportAlignLabel;

	public GameObject knowledgeLabel;
	public GameObject otherLabel;
	public GameObject actionPlanLabel;
	public  bool isMale;
	public  bool isFemale;

	// Use this for initialization
	void Start ()
	{
		user = GameObject.FindGameObjectWithTag("UserControl");

		if(isMale)
			currentHolder = user.GetComponent<UserStorage> ().mainUser.GetMaleStakeholder ();
		if(isFemale)
			currentHolder = user.GetComponent<UserStorage> ().mainUser.GetFemaleStakeholder ();

		Vector3 temp = parent.transform.position;
		temp.x = currentHolder.transformX;
		temp.y = currentHolder.transformY;
		parent.transform.position = temp;

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (currentHolder != null) 
		{
			nameLabel.GetComponent<GetName> ().name = currentHolder.name;
			positionLabel.GetComponent<GetPosition> ().position = currentHolder.position;
			goalsLabel.GetComponent<GetGoals> ().goals = currentHolder.goals;
			reportNameLabel.GetComponent<GetReportName> ().reportName = currentHolder.reportName;
			reportPositionLabel.GetComponent<GetReportPosition> ().reportPosition = currentHolder.reportPosition;
			reportAlignLabel.GetComponent<GetReportAlign>().reportAlign = currentHolder.reportAlign;
			knowledgeLabel.GetComponent<GetKnowledge>().knowledge = currentHolder.knowledge;
			otherLabel.GetComponent<GetOther>().other = currentHolder.other;
			actionPlanLabel.GetComponent<GetActionPlan>().actionPlan = currentHolder.actionPlan;

			currentHolder.transformX = parent.transform.position.x;
			currentHolder.transformY = parent.transform.position.y;
		}
	}
}

