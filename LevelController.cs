using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {
//	public Vector3 startPosition;
	public Vector3 startRotationAngles;
//	public EnemyBase[] enemies;
	public bool allEnemiesActive;
	public string missionDetails;
//	public int enemiesKilled = 0;
	public GameController gc;
	public GameObject activationTrigger;
	public string hintText;
	public GameObject plantedBomb;
	public GameObject bombParticles;
	public bool isSaperatelyTriggered;
//	public List<TerroristController> levelEnemies ;
	public static LevelController instance;
	public Transform[] playerPoints;
	public int[] playerIndexWise;
	int currentPointIndex = -1 , currentPointPlayers = 0 ;
//	public InputControl inputController;
	public Transform playerStartingPosition;
	LevelMeta levelMeta;
//	public LeaderBoardHandler leaderBoardHandler;

	void OnEnable(){
		if (instance == null) instance = this;
		levelMeta = GetComponent<LevelMeta> ();
		Invoke ("SetLeveltaskUI" , 1.0f);
		gc.SetLevelData (levelMeta);
//		GameController.taskInfo += 
	}

	public void playerEnterInRange(GameObject player) {
		allEnemiesActive = true;
		activationTrigger.SetActive (false);
	}

	public void EnemyKilled(Transform enemy , string hitAtBodyPartName , int playerGunName , bool isDieWithGrenade) {
		gc.UpdateEnemyKillCount (hitAtBodyPartName , playerGunName , isDieWithGrenade);
		Vector3 soundPoint = gc.fpsPlayer.position;
		soundPoint.z += Vector3.Distance(gc.fpsPlayer.position,enemy.position)/10; 
		AudioSource.PlayClipAtPoint(gc.deathClips[Random.Range(0,gc.deathClips.Length)],soundPoint);
	}

	public void enablePlantBomb() {
		gc.enablePlantBomb ();
	}

	public void pickupBomb() {
		gc.showBombBtn ();
	}

	void SetLeveltaskUI(){
		gc.SetLeveltaskUI ( );
	}

}
