using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowLevelTasksInUI : MonoBehaviour {
	public Text plantBombText, totalEnemiesText, headShotText, gunPropertiesText, grenadePropertiesText;
	public GameObject plantbombObj, totalEnemiesObj, headShotObj, gunPropertiesObj, grenadePropertiesObj;

	public static ShowLevelTasksInUI instance;
//	public HudController hudController;
	public Text levelDescription;
	// Use this for initialization
	void OnEnable () {
		if (instance == null) 
			instance = this;
//		if(GameSceneController.instance)
//			levelDescription.text = "" + GameSceneController.instance.currentLevelObj.levelDescription;
//		Time.timeScale = 0;
	}
	void Start(){
		//hudController.EnableJoyStick(false);
	}
	public void StartGame(){
//		Time.timeScale = 1;
//		hudController.EnableJoyStick(true);
		this.gameObject.SetActive (false);
	}
	public void SetTaskUI(bool isBombPlant , int bombCount , bool isTotalEnemies , int enemiesCount , bool isHeadShot , int headShotCount , 
		bool isGunProperties , int gunKillCount , string gunName , bool isGrenade , int grenadeCount){

		plantbombObj.SetActive (isBombPlant);
		totalEnemiesObj.SetActive (isTotalEnemies);
		headShotObj.SetActive (isHeadShot);
		gunPropertiesObj.SetActive (isGunProperties);
		grenadePropertiesObj.SetActive (isGrenade);

		if (isBombPlant)
			plantBombText.text = "plant " + bombCount + " bombs.";
		if (isTotalEnemies)
			totalEnemiesText.text = "kill " + enemiesCount + " enemies.";
		if (isHeadShot)
			headShotText.text = "kill " + headShotCount + " with headShot.";
		if (isGunProperties)
			gunPropertiesText.text = "shoot " + gunKillCount + " with " + gunName +" gun.";
		if (isGrenade)
			grenadePropertiesText.text = "kill " + grenadeCount + " with grenade." ;
	}
}
