using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {

	public enum MisisonType{
		Pistol,
		Rifle,
		PointToGo,
		GetACar,
		GoToFriend,
		KillEnemies,
		Shotgun,
		BombPlanting,
		Sniper,
		Grenade,
		Ak47,
		BombDiffuse,
		HostageEnemies,
		RPG
	
	}

	public MisisonType currentType;


	[SerializeField] List<MissionCheckPoint> checkPoints;
	// Use this for initialization
	void Start () {

		MissionController.HostagesEnemiesKilled = 0;
		Galassia.GameController2.Instance.missionManager = this;
		MissionController.currentMissionIsCompleted = false;
		OpenMission ();
	}
	


	int currentPoint=0;
	public void OpenMission(){
	
		if (currentPoint >= checkPoints.Count) {

			print ("Raheel Game Complete Completed, You Win");
//			Debug.Log (currentPoint);
//			if (!MissionController.currentMissionIsCompleted) {
//				MissionController.MissionNo++;
//				MissionController.currentMissionIsCompleted = true;
//			}
			Invoke("YouWin", 2.5f);

			return;
		}
		checkPoints [currentPoint].gameObject.SetActive (true);
		currentType = checkPoints [currentPoint].type;
		currentPoint++;




//		if (Galassia.GameController2.Instance.missionManager.currentType == MissionManager.MisisonType.Pistol) {
//			Galassia.GameController2.Instance.missionManager.OpenMission ();
//		}
	}

	void YouWin(){

		MissionController.instance.Win();

	}



}
