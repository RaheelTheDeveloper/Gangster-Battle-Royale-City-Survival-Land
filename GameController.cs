using UnityEngine;
using System.Collections;
public class GameController : MonoBehaviour {
//	public WeaponBehavior[] weapons;
//	public PlayerWeapons playerWeapons;
	private int currentWeaponIndex = 0;
//	public MobleInputController input;
	public LevelController level;
//	public Transform mainPlayer;
	public Transform fpsPlayer;
	public UIController ui;
	public GameObject[] levels;
	private int playerHP = 100;
	private int headShots = 0;
	private bool isGrenadeSelected = false;
	private int mainGunIndex = 0;
	public GameObject falseEffectObj;
	private int grenadeCount = 0;
	private int flashCount = 0;
	public GameObject[] segments;
	public GameObject[] subsegments;
	public AudioSource music;
	public AudioClip[] deathClips;
	public static GameController instance;
	public float playerDistanceFromBomb = 20, bombRadiusForEnemy = 30 ;
	public int bombExplodeTime = 20;
	public bool isUseGrenade ;
	public int grenadeQuantity;
	public bool isLoadThisLevel;
	public int levelNumber = 1;
	GameObject levelObj;
	void OnEnable(){
		if (instance== null) instance = this;
	}
	void Awake() {
		for (int i = 0; i < levels.Length; i++) {
			levels [i].SetActive (false);
		}
		mainGunIndex = PlayerPrefs.GetInt ("SelectedGunIndex", 0);
		if (mainGunIndex != 0) SetHaveWeapon (mainGunIndex);
//		if (IntegrationManager.Instance) IntegrationManager.Instance.gc = this;
		Time.timeScale = 1.0f;

		#if !UNITY_EDITOR

//		levels [Globals.currentLevelNumber-1].SetActive(true);
//		levelObj = levels [Globals.currentLevelNumber-1];

		#endif


		#if UNITY_EDITOR
		if (isLoadThisLevel) {
			levels[levelNumber-1].SetActive(true);
			levelObj = levels [levelNumber-1];
		}
		else {
//			levels [Globals.currentLevelNumber-1].SetActive(true);
//			levelObj = levels [Globals.currentLevelNumber-1];
		}
		#endif

		level = levelObj.GetComponent<LevelController> ();
		level.gc = this;
//		levelMeta = level.GetComponent<LevelMeta> ();
//		mainPlayer.position = level.playerStartingPosition.transform.position;
		Quaternion rot = Quaternion.identity;
		rot.eulerAngles = level.startRotationAngles;
//		mainPlayer.rotation = rot;
		StartCoroutine (ui.showLevelStartDetails (level.missionDetails));

		if (PlayerPrefs.GetInt ("Sound", 1) == 1) {
			AudioListener.volume = 1;
		} else {
			AudioListener.volume = 0;
		}
		StartCoroutine (fadeInMusic ());
//		if (IntegrationManager.Instance) IntegrationManager.Instance.HideBanner ();
		if (LevelController.instance){
			SetInitialGun (GetGunId ());

		}
	}

	void SetHaveWeapon(int index){
	}
	public void SetInitialGun(int idNotIndex){
	}

	IEnumerator fadeInMusic() {
		music.volume = 0;
		music.Play ();
		while (music.volume != 0.4f) {
			music.volume = Mathf.MoveTowards (music.volume, 0.4f, 0.5f * Time.deltaTime);
			yield return null;
		}
	}

	IEnumerator fadeOutMusic() {

		while (music.volume > 0f) {
			music.volume = Mathf.MoveTowards (music.volume, 0f, 0.5f * Time.deltaTime);
			yield return null;
		}
	}

	public void startMissionOnClick() {
		ui.btnClick.Play ();
		StartCoroutine (ui.showHud ());
	}

	public void onFireBtnDown() {
	}
	public AudioClip[] flashSound;

	public void onFireBtnUp() {
		//input.input.fireHold = false;
	}

	public void selectMainGun() {
//		input.deselectGrenade();
//		StartCoroutine (playerWeapons.SelectWeapon (mainGunIndex+1, false, false));
	}

	public void selectBomb(int index) {
		ui.hideGrenadeBtns ();
//		#if UNITY_EDITOR
//		if (isUseGrenade) {
//			flashCount = 15;
//			grenadeCount = 15;
//		}
//		#endif
;
		if (index == 2 && flashCount == 0)
			return;
		if (index == 1 && grenadeCount == 0)
			return;
		isGrenadeSelected = true;
		//currentWeaponIndex = index;

	}

	public void hitEnemy(int damage, GameObject enemyObj,Vector3 direction) {
		if (!level.allEnemiesActive && !level.isSaperatelyTriggered) {
			level.playerEnterInRange (fpsPlayer.gameObject);
		}
		if (enemyObj.tag == "EnemyHead") {
			Debug.Log ("HeadShot");
			ui.headShotEffect.SetActive (true);
			headShots++;
		}
//		EnemyBase enemy = enemyObj.GetComponentInParent<EnemyBase>();
//		if (enemy != null) {
//			enemy.takeHit (damage,enemyObj,direction);
//		}
	}

	public void AddHeadShot(){
		ui.headShotEffect.SetActive (true);
		headShots++;
	}

	public void setMissionCompleted() {
		//input.input.fireHold = false;
		StartCoroutine (fadeOutMusic ());
		StartCoroutine (ui.showMissionCompleteUI(killedEnemies,killedEnemies*100,headShots,headShots*50,playerHP,(int)(playerHP*0.5f)));
	}

	public void takePlayerDamage(int damageVal) {
		playerHP -= damageVal;
		playerHP = Mathf.Clamp (playerHP, 0, 100);
		ui.updateHealth (playerHP);
		if (playerHP == 0) {
			setGameOver ();
		}
	}
	[HideInInspector]
	public bool isGameover;
	public void setGameOver() {
		isGameover = true;
		StartCoroutine (WaitToShowUI ());
	}
	IEnumerator WaitToShowUI(){
		yield return new WaitForSeconds (1.0f);
		music.volume = 0;
//		if (Globals.currentLevelNumber == 5) {
//			PlayerPrefs.SetInt ("isLevel5FailedOnce", 1);
//		}
		Invoke ("freezeGame", 1.5f);
		ui.showGameOver ();
	}

	void freezeGame() {
		Time.timeScale = 0;
	}
	public void showHintMenu() {
		ui.showHintAndTimer();
	}
	public void takePlayerInput() {
//		input.walkBtnUnPressed ();
//		input.runBtnUnPressed ();
		ui.hudParent.SetActive (false);
	}
	public void showHintOnClick() {
		ui.btnClick.Play ();
		Time.timeScale = 0;
		ui.showHintDetails (level.hintText);
	}
	public void hintGotItOnClick() {
		Time.timeScale = 1.0f;
		ui.hintMenuParent.SetActive (false);
		ui.hudParent.SetActive (true);
	}
	public void hitPipe() {
//		if (level.GetComponent<HostageLevelController> ().molotovFired) {
//			ui.timerObj.SetActive (false);
//			StartCoroutine (level.GetComponent<HostageLevelController> ().breakThePipe ());
//		}
	}
	public void setHostageLevelCompleted() {
		Time.timeScale = 0;
		ui.showHostageLevelComplete ();
//		if (IntegrationManager.Instance) IntegrationManager.Instance.UpdateAnalytics ("LevelStatus_Completed_"+Globals.currentLevelNumber);
	}

	public void hostagesBurnt() {
		setGameOver ();
	}

	public void showFlashEffect() {
		falseEffectObj.SetActive (true);
	}

	public void updateAmmoInfo() {
//		if ((playerWeapons.currentWeapon-1)>=0) 
//		ui.updateAmmoInfo (weapons[playerWeapons.currentWeapon-1].bulletsLeft , weapons[playerWeapons.currentWeapon-1].bulletsToReload);
	}

	public void reloadWeapon() {
		ui.btnClick.Play ();
		//input.input.reloadPress = true;
		Invoke ("stopReload",0.2f);
	}

	public void Zoom(){
		//input.zoomBtnOnClick();
	}

	void stopReload() {
		//input.input.reloadPress = false;
	}
	public void enablePlantBomb() {
		ui.enableBombBtn ();
	}
	public void showBombBtn() {
		ui.showBombBtn ();
	}
	public void HideBombButton(){
		ui.hideBombBtn ();
	}

	public void plantBombOnClick() {
		ui.btnClick.Play ();
		ui.hpParent.SetActive (false);
		ui.hideBombBtn ();
		level.plantedBomb.SetActive (true);
		ui.showBombTimer ();
	}
	public void ShowBombPlantedUi(){
		ui.showBombTimer ();
		ui.hpParent.SetActive (false);
	}

	public void detotanteBomb() {
		ui.timerObj.SetActive (false);
		float dist = Vector3.Distance (fpsPlayer.position, level.plantedBomb.transform.position);
		Debug.Log(dist);
		if (dist < 7) { // die with bomb
			level.bombParticles.SetActive(true);
			level.GetComponent<AudioSource> ().Play ();
//			fpsPlayer.GetComponent<FPSPlayer>().ApplyDamage(200,null,false);
		} else { // clearLevel
			level.bombParticles.SetActive(true);
			level.GetComponent<AudioSource> ().Play ();
//			TerroristController[] enemies = GameObject.FindObjectsOfType<TerroristController> ();
//			if (enemies.Length > 0) {
//				for (int i = 0; i < enemies.Length; i++) {
//					enemies [i].damageHit (100, fpsPlayer.position, fpsPlayer.position, fpsPlayer.transform, true, true, false);
//				}
//			}
			Invoke("clearBombLevel",2.0f);
		}
	}
	void clearBombLevel() {
		Time.timeScale = 0;
//		if(PlayerPrefs.GetInt("LevelsCleared")<Globals.currentLevelNumber)
//			PlayerPrefs.SetInt ("LevelsCleared", Globals.currentLevelNumber);
		ui.plantBombCompletedParent.SetActive (true);
	}
	public void pauseGameOnClick() {
//		if (IntegrationManager.Instance) IntegrationManager.Instance.GamePausePanel ();
		ui.btnClick.Play ();
		Time.timeScale = 0;
		ui.showPauseMenu ();
	}

	public void resumeGameOnclick() {
		ui.btnClick.Play ();
		Time.timeScale = 1;
		ui.hidePauseMenu ();
	}
	public void doubleRewardOnClick() {
//		if (IntegrationManager.Instance.checkHeyzappAvailable()) {
//			IntegrationManager.Instance.ShowHeyzApp_RewardsVideo ("DoubleReward" , 100);
//		} else {
//			IntegrationManager.Instance.ShowUnityAds_RewardedVideo ("DoubleReward" , 100);
//		}
	}
	public void doubltTheReward() {
		ui.doubleReward ();
	}

	void Update() {
//		if (input.input.fireHold) {
//			updateAmmoInfo ();
//		}
//		if (Input.GetKeyDown (KeyCode.F)) onFireBtnDown();
//		if (Input.GetKeyUp(KeyCode.F)) onFireBtnUp();
	}

	public PlantBomb plantBomb;
	public PlantedBombBehaviour plantedBombBehaviour;
	public void ShowNewBombButton(bool isShow , GameObject triggerObj){
		ui.ShowNewBombButton (isShow , triggerObj);
	}
	public void EnableBombSituation(bool isEnable ){
		plantBomb.EnableBombScenerio(isEnable );
	}

	public void TimeUpBalstTheBomb(){
		plantedBombBehaviour.BlastBomb();
	}
	public void PlayFastBeep(){
		plantedBombBehaviour.PlayBeepSound (true);
	}

	public void CheckForPlayer(Transform bombPosition){
		float distance = Vector3.Distance (fpsPlayer.position, bombPosition.transform.position);
		if (distance <= playerDistanceFromBomb) Invoke ("DiePlayer", 0.25f);
	}
	void DiePlayer(){
//		fpsPlayer.GetComponent<FPSPlayer>().ApplyDamage(200,null,false);
	}
	void CheckForEnemy(Transform bombPosition){
		print ("Called ");
		Collider[] hitColliders = Physics.OverlapSphere(bombPosition.transform.position , bombRadiusForEnemy);
//		for (int i = 0; i < hitColliders.Length; i++) {
//			if (hitColliders[i].tag.Equals("Head") || hitColliders[i].tag.Equals("Body") || hitColliders[i].tag.Equals("Legs")) {
//				if (hitColliders[i].GetComponent<HitPoint>()) {
//					hitColliders [i].GetComponent<HitPoint> ().ApplyDamage (5000, false);
//					hitColliders [i].GetComponent<HitPoint> ().isBombDie = true;
//
//				}
//			}
//		}
	}

	public void SwitchWeapon(bool isSwitch){
		//input.input.SwitchWeapon (isSwitch);
		//input.input.grenadeHold = false;
	}




	#region TaskCompletedWork


	public LevelMeta levelMeta;
//	public LeaderBoardHandler leaderBoardHandler;

	[Header("Level Data")]
	[Space(10)]
	[Header("Initial Gun Data")]
	public bool isGiveInitialGun;
	public string initialGunName = "";
	public int initialtGunId = 1;

	[Space(10)]
	[Header("Bomb Data")]
	public bool isBombPlant;
	public int totalLevelBombs = 0;
	public int plantedBombCount = 0;

	[Space(10)]
	[Header("Enemies Data")]
	public bool isShootEnemies;
	public int totalEnemiesCount;
	public int killedEnemies = 0;

	[Space(10)]
	[Header("HeadShot Data")]
	public bool isHeadShotKill;
	public int headShotCount = 0;
	public int KilledByHeadShots = 0;
	[Space(10)]
	[Header("Special Gun Data")]
	public bool isSpecialGunKill;
	public string specialGunName = ""; 
	public int specialGunCount = 0 , specialGunKilled = 0;
	public int SpecialGunId = 0;

	[Space(10)]
	[Header("Grenade Data")]
	public int grenadeKillCount = 0;
	public bool  isKillWithGrenade;
	public int killedWithGrenade = 0;

	public int taskCompleted = 0, totaltask = 0 , totalScenerio = 0 , currentScenerioCount = 0;

	public void SetLevelData(LevelMeta levelMetaObj){
		levelMeta = levelMetaObj;
		if (levelMeta && levelMeta.levelType == TypeOfLevel.levelWithConditions) {
			// specific gun in start Work
			if (levelMeta.levelConditions.isGiveInitialSpecialGun) {
				isGiveInitialGun = true;
				initialGunName = levelMeta.initialGun.ToString ();
				SetInitialGun ();
			}
			// Till here

			if (levelMeta.levelConditions.isplantBomb){
				isBombPlant = true;
				totalLevelBombs = levelMeta.levelConditions.totalBombCount;
				AddATask ();
				totalScenerio++;
			}
			if (levelMeta.levelConditions.isKillEnemies) {
				isShootEnemies = true;
				totalScenerio++;
				AddATask ();
				totalEnemiesCount = levelMeta.levelConditions.totalEnemies;
				if (levelMeta.levelConditions.isKillWithHeadShot) {
					AddATask ();
					isHeadShotKill = true;
					headShotCount = levelMeta.levelConditions.headShotCount;
				}
				if (levelMeta.levelConditions.isKillWithSpecificGun) {
					AddATask ();
					isSpecialGunKill = true;
					specialGunName = levelMeta.levelConditions.specificGunName;
					specialGunCount = levelMeta.levelConditions.specificGunKillCount;
					SetSpecialGunIndex ();
				}
				if (levelMeta.levelConditions.isKillWithGrenade) {
					AddATask ();
					isKillWithGrenade = true;
					grenadeKillCount = levelMeta.levelConditions.grenadeKillEnemyCount;
				}
			}
		}
		else if(levelMeta && levelMeta.levelType == TypeOfLevel.TaskOneByOne){
			UpdateTask ();
		}
	}
	public void SetLeveltaskUI(){
		if (LevelTaskUIHandler.instance)
			LevelTaskUIHandler.instance.SetLevelTaskUI (isBombPlant, isShootEnemies, isHeadShotKill, isSpecialGunKill, isKillWithGrenade);
		Invoke ("SetTaskPopUpUI", 0.5f);
	}

	public void SetPopUi(){
		SetTaskPopUpUI ();
	}

	void SetTaskPopUpUI(){
		if (ShowLevelTasksInUI.instance) {
			ShowLevelTasksInUI.instance.SetTaskUI (isBombPlant, totalLevelBombs, isShootEnemies, totalEnemiesCount, isHeadShotKill, headShotCount, 
				isSpecialGunKill, specialGunCount, specialGunName, isKillWithGrenade, grenadeKillCount);
		}
	}

	void SetInitialGun(){
		if (levelMeta.initialGun == InitialGun.M1911 || levelMeta.initialGun == InitialGun.none)
			initialtGunId = 1;
		else if (levelMeta.initialGun == InitialGun.MP5)
			initialtGunId = 2;
		else if (levelMeta.initialGun == InitialGun.AK47)
			initialtGunId = 3;
		else if (levelMeta.initialGun == InitialGun.M4)
			initialtGunId = 4;
		else if (levelMeta.initialGun == InitialGun.Sniper)
			initialtGunId = 5;
		else if (levelMeta.initialGun == InitialGun.knife)
			initialtGunId = 7;
		else if (levelMeta.initialGun == InitialGun.ShotGun)
			initialtGunId = 8;
	}

	void SetSpecialGunIndex(){
		if (levelMeta.gunType == GunType.M1911 || levelMeta.gunType == GunType.none)
			SpecialGunId = 1;
		else if (levelMeta.gunType == GunType.MP5)
			SpecialGunId = 2;
		else if (levelMeta.gunType == GunType.AK47)
			SpecialGunId = 3;
		else if (levelMeta.gunType == GunType.M4)
			SpecialGunId = 4;
		else if (levelMeta.gunType == GunType.Sniper)
			SpecialGunId = 5;
		else if (levelMeta.gunType == GunType.knife)
			SpecialGunId = 7;
		else if (levelMeta.gunType == GunType.ShotGun)
			SpecialGunId = 8;
	}

	public int GetGunId(){
		return initialtGunId;
	}
	public void UpdateEnemyKillCount(string bodyPartName , int dieWithPlayerGun , bool isGrenadeDie){

		killedEnemies++;
		if(levelMeta.levelType == TypeOfLevel.levelWithConditions){
			if (isShootEnemies) {
				if ((killedEnemies >= totalEnemiesCount) && (!isEnemyTaskComplete)) {
					isEnemyTaskComplete = true;
					currentScenerioCount++;
					TaskCompleted ("TotalEnemies");
				}
				if (isHeadShotKill) UpdateHeadShot (bodyPartName);
				if (isSpecialGunKill) UpdateSpecialGunKills (dieWithPlayerGun);
				if (isKillWithGrenade && isGrenadeDie) UpdateGreandeKills ();
			}

			CheckGameStatus ();
			if (LevelTaskUIHandler.instance) LevelTaskUIHandler.instance.UpdateTaskUI ();

//			if (leaderBoardHandler) {
//				leaderBoardHandler.EnemyKilled ();
//				leaderBoardHandler.SpecialGunKilled (dieWithPlayerGun);
//				if (bodyPartName.Equals("Head")) 
//					leaderBoardHandler.HeadShotKilled ();
//			}
		}
		else if(levelMeta.levelType == TypeOfLevel.TaskOneByOne){
			
		}

	}
	bool isBombTaskCompleted , isEnemyTaskComplete , isHeadShotTaskCompleted , isSpeciaGunTaskCompleted , isGrenadeTaskCompleted ;
	void UpdateHeadShot(string bodyPartName){
		if (bodyPartName.Equals("Head")) 
			KilledByHeadShots++;
		if (KilledByHeadShots >= headShotCount && (!isHeadShotTaskCompleted)) {
			isHeadShotTaskCompleted = true;
			TaskCompleted ("Head");
		}
		if (LevelTaskUIHandler.instance)
			LevelTaskUIHandler.instance.UpdateTaskUI ();


	}
	void UpdateSpecialGunKills(int id){
		if (id == SpecialGunId)
			specialGunKilled++;
		if (specialGunKilled >= specialGunCount && (!isSpeciaGunTaskCompleted)) {
			isSpeciaGunTaskCompleted = true;
			TaskCompleted ("SpecialGun");
		}
		if (LevelTaskUIHandler.instance)
			LevelTaskUIHandler.instance.UpdateTaskUI ();


	}
	void UpdateGreandeKills(){
		killedWithGrenade++;
		if (killedWithGrenade >= grenadeKillCount && (!isGrenadeTaskCompleted)) {
			isGrenadeTaskCompleted = true;
			TaskCompleted ("Grenade");
		}
		if (LevelTaskUIHandler.instance)
			LevelTaskUIHandler.instance.UpdateTaskUI ();
//		if (leaderBoardHandler) 
//			leaderBoardHandler.GrenadeKill ();

	}
	public void UpdateBombPlantedStatus(){
		plantedBombCount++;
//		if (leaderBoardHandler) 
//			leaderBoardHandler.BombPlanted ();
		if (isBombPlant) {
			if ((plantedBombCount >= totalLevelBombs) && (!isBombTaskCompleted)) {
				isBombTaskCompleted = true;
				currentScenerioCount++;
				TaskCompleted ("Bomb");
			}
		}
		CheckGameStatus ();
		if (LevelTaskUIHandler.instance)
			LevelTaskUIHandler.instance.UpdateTaskUI ();
	}

	void CheckGameStatus(){
		if (currentScenerioCount >= totalScenerio) {
			if (taskCompleted >= totaltask) {
				if (!(GameController.instance.isGameover)) {
					setMissionCompleted ();
				}
			} else {
				setGameOver ();
			}
		}
	}

	bool HeadShotsTaskComplete(){
		if (KilledByHeadShots >= headShotCount)
			return true;
		else
			return false;
	}
	bool SpecialGunTaskComplete(){
		if (specialGunKilled >= specialGunCount) 
			return true;
		else
			return false;
	}
	bool GrenadeTaskComplete(){
		if (killedWithGrenade >= grenadeKillCount)
			return true;
		else
			return false;
	}

	void AddATask(){
		totaltask++;
	}
	void TaskCompleted(string taskName){
		taskCompleted++;
		if (LevelTaskUIHandler.instance) {
			LevelTaskUIHandler.instance.UpdateTaskUI ();
			LevelTaskUIHandler.instance.ShowTaskCompletedAnim(taskName);
		}
	}

	[Header("Level Task By Task Data")]
	[Space(10)]
	[Header("Task By Task Data")]

	public bool isIncremental= false;
	public delegate void TaskInfo(bool isTaskCompleted , Task completedTask , bool isIncremental , int amountToAdd);
	public static event TaskInfo taskInfo;

	public void TaskAccomplished(bool taskStatus , Task taskType , int amountToAdd = 1){
//			print ("Got Event");
//		if (taskInfo != null){
			if(levelMeta.levelType == TypeOfLevel.TaskOneByOne){
				isIncremental = levelMeta.taskOneByOne [currentTaskId].isIncremental;
				taskInfo += TaskCompleted;
				taskInfo (taskStatus, taskType , isIncremental , amountToAdd);
//				print ("Got Event");
//				TaskCompleted (taskStatus , taskType , isIncremental , amountToAdd);
			}
//		}
	}

	public int currentTaskId = 0 , incrementalTaskCount = 0;
	//bool isIncrementalTask = false;
	public Task currentTask = Task.none;
	public void TaskCompleted(bool isCompleted, Task completedtask , bool isIncrementalTask , int amountToAdd){
		taskInfo -= TaskCompleted;
		if (!isCompleted)
			return;
		if(CheckForTask(completedtask , isIncrementalTask , amountToAdd)){
			if (levelMeta.taskOneByOne [currentTaskId].objectsToDeActivate)
				levelMeta.taskOneByOne [currentTaskId].objectsToDeActivate.SetActive (false);
			if (levelMeta.taskOneByOne [currentTaskId].objectToActive)
				levelMeta.taskOneByOne [currentTaskId].objectToActive.SetActive (false);
			currentTaskId++;
			incrementalTaskCount = 0;
			isIncrementalTask = false;
			PlayAchievementSound ();
			if(currentTaskId >= levelMeta.taskOneByOne.Count){
				Debug.LogError ("LevelCompleted");
//				if (GameSceneController.instance)
//					GameSceneController.instance.GameCompleted ();
			}
			else{
				UpdateTask ();
			}
		}
		else{
			if (GameController.instance){
				Debug.LogError ("NOT COMPLETE");
//				GameSceneController.instance.hudController.GameFail ("You haven't completed the task with given condition. Try again... ");
//				GameSceneController.instance.StopGameIsEnded();
			}
		}

	}


	bool CheckForTask(Task completedTask , bool isIncrementalTask , int amountToAdd){
		if (currentTask == completedTask){
			if(isIncrementalTask){
				incrementalTaskCount+=amountToAdd;
				UpdateTask ();
				if (incrementalTaskCount >= levelMeta.taskOneByOne [currentTaskId].amount)
					return true;
				else
					return false;
			}
			else{
				return true;
			}
		}
		else
			return false;
	}

	void UpdateTask(){
		currentTask = levelMeta.taskOneByOne [currentTaskId].task;
		if (levelMeta.taskOneByOne [currentTaskId].objectToActive)
			levelMeta.taskOneByOne [currentTaskId].objectToActive.SetActive (true);
		isIncremental = levelMeta.taskOneByOne [currentTaskId].isIncremental;
		ui.SetTaskOneByOnePanelText (GetTaskString (isIncremental));
	}
	public void SetTask(){
		UpdateTask ();
	}
	public AudioSource audioSource;
	public AudioClip[] achievementsSounds;
	public void PlayAchievementSound(){
		int val = Random.Range (0, achievementsSounds.Length);
		audioSource.PlayOneShot (achievementsSounds[val]);
	}

	public string currentTaskString;
	string GetTaskString(bool isIncrementalTask){
		if (currentTask == Task.GetACar)
			if(isIncrementalTask)
				currentTaskString = "Get "+ incrementalTaskCount +" / "+ levelMeta.taskOneByOne [currentTaskId].amount + "Cars.";
			else
				currentTaskString = "Get A Car.";
		else if (currentTask == Task.PickAk47)
			if(isIncrementalTask)
				currentTaskString = "Find and pick " +incrementalTaskCount+" / "+levelMeta.taskOneByOne [currentTaskId].amount+ "AK47.";
			else
				currentTaskString = "Find and pick AK47.";
		else if (currentTask == Task.PickAPistol)
			if(isIncrementalTask)
				currentTaskString = "Find and pick " +incrementalTaskCount+" / "+levelMeta.taskOneByOne [currentTaskId].amount+ "Pistols";
			else
				currentTaskString = "Pick a Pistol";
		else if (currentTask == Task.PickGrenade)
			if(isIncrementalTask)
				currentTaskString = "Find and pick " +incrementalTaskCount+" / "+levelMeta.taskOneByOne [currentTaskId].amount+ "grenades.";
			else
				currentTaskString = "Pick a grenade";
		else if (currentTask == Task.PickMp5)
			if(isIncrementalTask)
				currentTaskString = "Find and pick " +incrementalTaskCount+" / "+levelMeta.taskOneByOne [currentTaskId].amount+ "Mp5 gun.";
			else
				currentTaskString = "Pick Mp5 gun.";
		else if (currentTask == Task.PickRocketLauncher)
			if(isIncrementalTask)
			currentTaskString = "Find and pick " +incrementalTaskCount+" / "+levelMeta.taskOneByOne [currentTaskId].amount+ "Rcket Launchers.";
			else
				currentTaskString = "Pick rocket launcher.";
		else if (currentTask == Task.PickShotGun)
			if(isIncrementalTask)
				currentTaskString = "Find and pick " +incrementalTaskCount+" / "+levelMeta.taskOneByOne [currentTaskId].amount+ "ShotGuns.";
			else
				currentTaskString = "Get a ShotGun";
		else if (currentTask == Task.PickSniper)
			if(isIncrementalTask)
				currentTaskString = "Find and pick " +incrementalTaskCount+" / "+levelMeta.taskOneByOne [currentTaskId].amount+ "Swords.";
			else
				currentTaskString = "Get A Sword";
		else if (currentTask == Task.PickSniper)
			if(isIncrementalTask)
				currentTaskString = "Find and pick " +incrementalTaskCount+" / "+levelMeta.taskOneByOne [currentTaskId].amount+ "Snipers.";
			else
				currentTaskString = "Pick a Sniper";
		else if (currentTask == Task.GiveChalanToWronglyParkedCars)
		if(isIncrementalTask)
			currentTaskString = "Find " +incrementalTaskCount+" / "+levelMeta.taskOneByOne [currentTaskId].amount+ "Wrongly parked cars and chalan it.";
		else
			currentTaskString = "Find a wrongly parked car and chalan it.";
		return currentTaskString;
	}
	#endregion
}
