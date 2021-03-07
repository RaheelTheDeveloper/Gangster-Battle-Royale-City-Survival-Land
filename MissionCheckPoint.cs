using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionCheckPoint : MonoBehaviour {

	public MissionManager.MisisonType type;
	[SerializeField]private GameObject OpenObject;
	[SerializeField]private string message;
	public string messageAfterTrigger;
	// Use this for initialization

	void OnEnable(){
		GameObject.Find ("GameplayObjectiveText").GetComponent<Text>().text = message;
	}

	public void  Mission(){
		print (message);
		if(OpenObject !=null)
			OpenObject.SetActive (true);
			this.gameObject.SetActive (false);
	
	}

	

}
