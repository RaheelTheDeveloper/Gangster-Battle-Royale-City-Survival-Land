using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskHandler : MonoBehaviour {

	public Task task;
	bool isTaskCompleted = false;
	public bool isUseThisTask = true;

	public bool isCompleteThis = false;
	void Update(){
		if(isCompleteThis)
			Done();
	}
	public void TaskCompleted(){
		if (!isUseThisTask)
			return;
		if (isTaskCompleted)
			return;
		if (GameController.instance){
			GameController.instance.TaskAccomplished (true, task);
			isTaskCompleted = true;
//			Debug.LogError("Done");
		}
	}


	void Done(){
		TaskCompleted ();
	}
}
