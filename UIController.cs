using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour {
	public GameObject hudParent;
	public GameObject levelStartParent;
	public GameObject startMissionBtn;
	public GameObject missionCompleteParent;
	public GameObject hpParent;
	public GameObject gameOverParent;
	public GameObject hintMenuParent;
	public GameObject hintBtnObj;
	public GameObject hintDeatilObj;
	public Text hintDetailText;
	public Text missionDetailText;
	// Level Clear Variables
	public GameObject levelClearHeading;
	public GameObject enemiesKilledRow;
	public GameObject headShotsRow;
	public GameObject healtBonusRow;
	public GameObject buttonParent;
	public GameObject totalRewardParent;
	public Text totalRewardText;
	public Text enemiesKilledVal;
	public Text headShotsVal;
	public Text healthRemainingVal;
	public Text enemiesKilledBonus;
	public Text headShotsBonus;
	public Text healthRemainingBonus;
	public GameObject centerLine;
	public GameObject hostageLevelCompleteParent;
	public GameObject loadingScreen;
	public GameObject timerObj;
	public GameObject flashbangBtn;
	public GameObject grenadeBtn;
	public GameObject flashBangInstructionPopup;
	public Text ammoText;
	public Image ammoFillImage;
	public Text HPText;
	public Text grenadeCountText;
	public Text flashbangCountText;
	public GameObject headShotEffect;
	public GameObject bombBtn;
	public GameObject plantBombCompletedParent;
	public GameObject pauseMenuParent;
	public AudioSource btnClick;
	public AudioSource counterSound;
	private int totalRewardEarned;
	public Button doubleRewardBtn;
	public Slider sensitivityslider;
	public GameController gc;
	public Image soundsImg;
	public Sprite soundsOn;
	public Sprite soundsOff;
	public GameObject joystickParent;
//	public SmoothMouseLook smoothMouseLook;
//	public InputControl inputControl;
	public Button autoAimBtn;
	bool isAutoAim ;
//	public LeaderBoardHandler leaderBoardHandler;
	public 
	void OnEnable() {
		btnClick = GetComponents<AudioSource> () [0];
		counterSound = GetComponents<AudioSource> () [1];
		SetSliderValue ();
		autoAimBtn.onClick.AddListener (() => AutoAimPress ());
	}
	void AutoAimPress(){
		isAutoAim = !isAutoAim;
//		inputControl.WeaponOperateState (isAutoAim);
	}
	public IEnumerator showLevelStartDetails(string message) {
		missionDetailText.text = message;
		startMissionBtn.transform.localScale = Vector3.zero;
		yield return new WaitForSeconds (1.5f);
		levelStartParent.SetActive (true);
		yield return new WaitForSeconds (2.5f);
		startMissionBtn.SetActive (true);
		LeanTween.scale (startMissionBtn, Vector3.one, 0.3f).setEase (LeanTweenType.easeOutBack);
	}

	public IEnumerator showHud() {
		levelStartParent.SetActive (false);
		hudParent.SetActive (true);
//		joystickParent.SetActive (true);
//		setUpHUdButton ();
		SetTaskUiPanel();
		//if(GameController.instance && GameController.instance.levelMeta.levelType == TypeOfLevel.levelWithConditions)
	//		if (LevelTaskUIHandler.instance)
	//				LevelTaskUIHandler.instance.ShowTaskPanelAnim ();
		
		yield return null;
	}

	public void showGrenadeBtns() {
		grenadeBtn.SetActive (true);
		flashbangBtn.SetActive (true);
	}
	public void hideGrenadeBtns() {
		grenadeBtn.SetActive (false);
		flashbangBtn.SetActive (false);
	}
	public IEnumerator showMissionCompleteUI(int enemiesKilled,int enemiesReward, int headShots, int headshotsReward,int healthRemaining, int healthBonusReward) {
		int totalRewardVal = 0;
		float tempVal = 0f;
		hudParent.SetActive (false);
		joystickParent.SetActive (false);
		yield return new WaitForSeconds (2.5f);
		missionCompleteParent.SetActive (true);
		enemiesKilledVal.text = enemiesKilled.ToString ();
		enemiesKilledBonus.text = enemiesReward.ToString ();
		headShotsVal.text = headShots.ToString ();
		headShotsBonus.text = headshotsReward.ToString ();
		healthRemainingVal.text = healthRemaining.ToString ()+ "%";
		healthRemainingBonus.text = healthBonusReward.ToString ();
		yield return new WaitForSeconds (1.0f);

		levelClearHeading.transform.localScale = Vector3.one * 2.5f;
		levelClearHeading.SetActive (true);
		LeanTween.scale (levelClearHeading, Vector3.one, 0.05f).setEase (LeanTweenType.linear);
		yield return new WaitForSeconds (0.5f);

		totalRewardParent.transform.localScale = Vector3.zero;
		totalRewardParent.SetActive (true);
		LeanTween.scale (totalRewardParent, Vector3.one, 0.2f).setEase (LeanTweenType.easeOutBack);
		yield return new WaitForSeconds (0.5f);

		centerLine.transform.localScale = Vector3.zero;
		centerLine.SetActive (true);
		LeanTween.scale (centerLine, Vector3.one, 0.2f).setEase (LeanTweenType.easeOutBack);
		yield return new WaitForSeconds (0.4f);

		enemiesKilledRow.transform.localScale = Vector3.zero;
		enemiesKilledRow.SetActive (true);
		LeanTween.scale (enemiesKilledRow, Vector3.one, 0.2f).setEase (LeanTweenType.easeOutBack);
		yield return new WaitForSeconds (0.2f);
		counterSound.Play ();
		while (totalRewardVal != enemiesReward) {
			tempVal = Mathf.MoveTowards (tempVal, enemiesReward, 500.0f * Time.deltaTime);
			totalRewardVal = (int)tempVal;
			totalRewardText.text = totalRewardVal.ToString ();

			yield return null;
		}

		counterSound.Stop ();
		yield return new WaitForSeconds (0.3f);

		headShotsRow.transform.localScale = Vector3.zero;
		headShotsRow.SetActive (true);
		LeanTween.scale (headShotsRow, Vector3.one, 0.2f).setEase (LeanTweenType.easeOutBack);
		yield return new WaitForSeconds (0.2f);
		int targetVal = totalRewardVal + headshotsReward;
		totalRewardVal = targetVal;
		totalRewardText.text = totalRewardVal.ToString ();
//		counterSound.Play ();
//		while (totalRewardVal != targetVal) {
//			tempVal = Mathf.MoveTowards (totalRewardVal, targetVal, 50.0f * Time.deltaTime);
//			totalRewardVal = (int)tempVal;
//			totalRewardText.text = totalRewardVal.ToString ();
//			yield return null;
//		}
//		counterSound.Stop ();
		yield return new WaitForSeconds (0.3f);

		healtBonusRow.transform.localScale = Vector3.zero;
		healtBonusRow.SetActive (true);
		LeanTween.scale (healtBonusRow, Vector3.one, 0.2f).setEase (LeanTweenType.easeOutBack);
		yield return new WaitForSeconds (0.2f);
		targetVal = totalRewardVal + healthBonusReward;
		counterSound.Play ();
		while (totalRewardVal != targetVal) {
			tempVal = Mathf.MoveTowards (totalRewardVal, targetVal, 100.0f * Time.deltaTime);
			totalRewardVal = (int)tempVal;
			totalRewardText.text = totalRewardVal.ToString ();
			yield return null;
		}
		counterSound.Stop ();
		yield return new WaitForSeconds (0.3f);
		buttonParent.transform.localScale = Vector3.zero;
		buttonParent.SetActive (true);
		LeanTween.scale (buttonParent, Vector3.one, 0.2f).setEase (LeanTweenType.easeOutBack);
//		if (IntegrationManager.Instance) IntegrationManager.Instance.ShowUnity_SkipableVideo ();

//		if (IntegrationManager.Instance && (IntegrationManager.Instance.checkHeyzappAvailable () || IntegrationManager.Instance.hasUnityAds_RewardedVideo ())) {
//			doubleRewardBtn.interactable = true;
//		} else {
//			doubleRewardBtn.interactable = false;
//		}
		int totalTemp = PlayerPrefs.GetInt ("Dollars",0);
		totalTemp += totalRewardVal;
		totalRewardEarned = totalRewardVal;
		PlayerPrefs.SetInt ("Dollars",totalTemp);
		PushScore (totalTemp);
//		if(PlayerPrefs.GetInt("LevelsCleared",0)<Globals.currentLevelNumber)
//			PlayerPrefs.SetInt ("LevelsCleared", Globals.currentLevelNumber);
//		if (IntegrationManager.Instance) {
//			IntegrationManager.Instance.UpdateAnalytics ("Level Status_Completed_Levels Cleared_"+PlayerPrefs.GetInt ("LevelsCleared", 0).ToString ());
//			IntegrationManager.Instance.GameCompletePanel ();
//		}
		yield return null;
	}
	void PushScore(int score){
//		currentScore = PlayerPrefs.GetInt ("CurrentScore", 0);
//		currentScore += score;
//		PlayerPrefs.SetInt ("CurrentScore", currentScore);
//		if (LeaderBoardManager.instance)
//			LeaderBoardManager.instance.SubmitScore ();
//		if (leaderBoardHandler) leaderBoardHandler.AddScore (score);

	}

	public void showGameOver() {
		hudParent.SetActive (false);
		joystickParent.SetActive (false);
		gameOverParent.SetActive (true);
//		Globals.levelsFailedOnTrot++;
//		if (Globals.levelsFailedOnTrot == 4)
//			Globals.levelsFailedOnTrot = 0;
//		Invoke ("playVideoAd", 1.1f);
//		if (IntegrationManager.Instance) {
//			IntegrationManager.Instance.UpdateAnalytics ("Level Status_Failed_Levels_Cleared_"+Globals.currentLevelNumber.ToString ());
//			IntegrationManager.Instance.GameFailPanel ();
//		}
	}

	void playVideoAd() {
//		if (Globals.levelsFailedOnTrot == 3){
//			if (IntegrationManager.Instance) IntegrationManager.Instance.ShowChartBoost_Interstitial ();
//
//		} else {
//			if (IntegrationManager.Instance) IntegrationManager.Instance.ShowUnity_SkipableVideo ();
//
//		}
	}
	public void showHintAndTimer() {
		hintMenuParent.SetActive (true);
		hudParent.SetActive (false);
		joystickParent.SetActive (false);
		timerObj.SetActive (true);
	}

	public void showBombTimer() {
		timerObj.GetComponent<TimerController> ().detonateBomb = true;
//		timerObj.GetComponent<TimerController> ().totalTime = gc.bombExplodeTime;
		timerObj.GetComponent<TimerController>().addtionalText = " to move away from the dead circle";
		timerObj.SetActive (true);
	}
	public void showHintDetails(string hintText) {
		hintDetailText.text = hintText;
		hintBtnObj.SetActive (false);
		hintDeatilObj.SetActive (true);
	}

	public void showHostageLevelComplete() {
		hudParent.SetActive (false);
		joystickParent.SetActive (false);
//		if(PlayerPrefs.GetInt("LevelsCleared")<Globals.currentLevelNumber)
//			PlayerPrefs.SetInt ("LevelsCleared", Globals.currentLevelNumber);
		hostageLevelCompleteParent.SetActive (true);
//		if (leaderBoardHandler) leaderBoardHandler.HostagesSaved ();
	}

	public void loadMainMenu() {
		btnClick.Play ();
		loadingScreen.SetActive (true);
//		if (IntegrationManager.Instance) IntegrationManager.Instance.ShowAdmobInterstetials ();
		SceneManager.LoadScene ("MainMenu");
	}
	public void restartLevel() {
		btnClick.Play ();
		loadingScreen.SetActive (true);
//		if (IntegrationManager.Instance) IntegrationManager.Instance.ShowAdmobInterstetials ();
		SceneManager.LoadScene ("Gameplay");
	}

	void showFlashBangInstructions() {
		hudParent.SetActive (false);
		joystickParent.SetActive (false);
		flashBangInstructionPopup.SetActive (true);
	}

	public void gotFlashBangInstructions() {
		hudParent.SetActive (true);
		joystickParent.SetActive (true);
		flashBangInstructionPopup.SetActive (false);
		flashbangBtn.SetActive (true);
	}
	public void updateAmmoInfo(int bulletsLeft, int totalBullets) {
//		print ("Called func : " + bulletsLeft + " total : " + totalBullets);
		ammoText.text = bulletsLeft + "/" + totalBullets;
		ammoFillImage.fillAmount = (float)bulletsLeft / (float)totalBullets;
	}
	public void updateHealth(int health) {
		HPText.text = health.ToString ();
	}
	public void updateFlashCounter(int val) {
		flashbangCountText.text = val.ToString ();
	}
	public void updateGrenadeCounter(int val) {
		grenadeCountText.text = val.ToString ();
	}
	public void showBombBtn() {
		bombBtn.SetActive (true);
		bombBtn.GetComponent<Button> ().interactable = false;
	}
	public void enableBombBtn() {
		bombBtn.GetComponent<Button> ().interactable = true;
	}
	public void hideBombBtn() {
		bombBtn.SetActive (false);
	}

	public void showPauseMenu() {
		hudParent.SetActive (false);
		joystickParent.SetActive (false);
		sensitivityslider.value = PlayerPrefs.GetFloat ("Sensitivity", 0.5f);
		setUpSound ();
		pauseMenuParent.SetActive (true);
	}

	public void hidePauseMenu() {
		hudParent.SetActive (true);
		joystickParent.SetActive (true);
		pauseMenuParent.SetActive (false);
	}

	public void doubleReward() {
		totalRewardEarned *= 2;
		totalRewardText.text = totalRewardEarned.ToString ();
		doubleRewardBtn.interactable = false;
	}
	void SetSliderValue(){
		sensitivityslider.value = PlayerPrefs.GetFloat ("MySliderValue", 0.5f);

	}
	public void sensitivitySliderOnClick() {
//		sensitivityslider.value = sliderVal.value;
//		sensitivityslider.value = PlayerPrefs.GetFloat ("MySliderValue", 0.5f);
		PlayerPrefs.SetFloat ("Sensitivity", sensitivityslider.value);
		PlayerPrefs.SetFloat ("MySliderValue", sensitivityslider.value);
//		gc.input.updateSensitivity ();
//		smoothMouseLook.updateSensitivity();
	}

	public void soundsBtnOnClick() {
		if (PlayerPrefs.GetInt ("Sound", 1) == 1) {
			PlayerPrefs.SetInt ("Sound", 0);
		} else {
			PlayerPrefs.SetInt ("Sound", 1);
		}
//		Debug.Log ("Called");
		setUpSound ();
	}
	void setUpSound() {
		if (PlayerPrefs.GetInt ("Sound", 1) == 1) {
			soundsImg.sprite = soundsOn;
			soundsImg.SetNativeSize ();
			AudioListener.volume = 1;
		} else {
			soundsImg.sprite = soundsOff;
			soundsImg.SetNativeSize ();
			AudioListener.volume = 0;
		}
	}

	public GameObject newBombButton , bombHUD;
	public Image bombTimer;
	public Text bombTimeText;
	public PlantBomb plantBomb;
	public GameObject fadeIn , fadeOut;
	GameObject bombTrigger;
	public void ShowNewBombButton(bool isShow , GameObject bombTriggerObject){
		newBombButton.SetActive (isShow);
		bombTrigger = bombTriggerObject;
	}
	public void EnableBombHands(bool isEnable){
//		print ("Calling");
		StartCoroutine (WaitForPress (isEnable));
		if (isEnable) joystickParent.SetActive (!isEnable);
		newBombButton.gameObject.SetActive (false);
	}
	IEnumerator WaitForPress(bool isEnable){
		yield return new WaitForSeconds (0.1f);
		fadeIn.transform.parent.gameObject.SetActive (true);
		fadeIn.SetActive (true);
		yield return new WaitForSeconds (1.25f);
//		gc.EnableBombSituation (isEnable );
		hudParent.SetActive (!isEnable);
		bombHUD.SetActive (isEnable);
		joystickParent.SetActive (!isEnable);
		yield return new WaitForSeconds (1.0f);
		fadeOut.SetActive (true);
		fadeIn.SetActive (false);
		yield return new WaitForSeconds (1.5f);
		fadeOut.SetActive (false);
		fadeIn.SetActive (false);
		fadeIn.transform.parent.gameObject.SetActive (false);

	}
	public void FillBombTimer(float totalTime , float currentTime,  float timeToShow){
		bombTimer.fillAmount = currentTime / totalTime;
		bombTimeText.text = "00:0" + (int)timeToShow;
	}
	public void StartBombPlant(bool isPress){
		plantBomb.StartBombPlanting (isPress , bombTrigger);
		EnableBombFiller (isPress);
	}
	public void EnableBombFiller(bool isEnable){
		bombTimer.transform.gameObject.SetActive (isEnable);
	}
	public GameObject pickUpBtn;

	public GameObject levelWithConditionsUi , levelWithTaskOneByOneUi;
	public void SetTaskUiPanel(){
		if(GameController.instance && GameController.instance.levelMeta.levelType == TypeOfLevel.levelWithConditions){
			levelWithTaskOneByOneUi.SetActive (false);
			levelWithConditionsUi.SetActive (true);
			if(levelWithConditionsUi.GetComponent<UIElementAnimationController>())
			levelWithConditionsUi.GetComponent<UIElementAnimationController> ().enabled = true;
		}
		if(GameController.instance && GameController.instance.levelMeta.levelType == TypeOfLevel.TaskOneByOne){
			levelWithTaskOneByOneUi.SetActive (true);
			levelWithConditionsUi.SetActive (false);
			if(levelWithTaskOneByOneUi.GetComponent<UIElementAnimationController>())
				levelWithTaskOneByOneUi.GetComponent<UIElementAnimationController> ().enabled = true;
		}
	}
	public Text taskOneByOnePanelText;
	public void SetTaskOneByOnePanelText(string text){
		taskOneByOnePanelText.text = "" + text;
	}
}
