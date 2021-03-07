using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionController : MonoBehaviour {

	public static int MissionNo = 1;
//	public int enemiesKilled = 0;
	public static bool currentMissionIsCompleted = false;
	public GameObject completePanel;
	public GameObject failPanel;
	public GameObject pausePanel;
	public static MissionController instance;
	public static int HostagesEnemiesKilled = 0;
//	[SerializeField] private RectTransform popUp;


	// Use this for initialization
	void Awake () {
		if (MissionNo == 1) {
			gameObject.transform.GetChild (0).gameObject.SetActive (true);
		}
		else if (MissionNo == 2) {
			gameObject.transform.GetChild (1).gameObject.SetActive (true);
		}
		else if (MissionNo == 3) {
			gameObject.transform.GetChild (2).gameObject.SetActive (true);
		}
		else if (MissionNo == 4) {
			gameObject.transform.GetChild (3).gameObject.SetActive (true);
		}
		else if (MissionNo == 5) {
			gameObject.transform.GetChild (4).gameObject.SetActive (true);
		}
		else if (MissionNo == 6) {
			gameObject.transform.GetChild (5).gameObject.SetActive (true);
		}
		else if (MissionNo == 7) {
			gameObject.transform.GetChild (6).gameObject.SetActive (true);
		}
		else if (MissionNo == 8) {
			gameObject.transform.GetChild (7).gameObject.SetActive (true);
		}
		else if (MissionNo == 9) {
			gameObject.transform.GetChild (8).gameObject.SetActive (true);
		}
		else if (MissionNo == 10) {
			gameObject.transform.GetChild (9).gameObject.SetActive (true);
		}
	}

	void Start(){

		instance = this;
		HostagesEnemiesKilled = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NextMission(){
		if (!currentMissionIsCompleted) {
			MissionNo++;
			completePanel.SetActive (false);
			Time.timeScale = 1;

			if (MissionNo > 10) {
				MissionNo = 1;
			}

			for (int i = 0; i < gameObject.transform.childCount; i++) {
				gameObject.transform.GetChild (i).gameObject.SetActive (false);
			}

			gameObject.transform.GetChild (MissionNo).gameObject.SetActive (true);
			currentMissionIsCompleted = true;

			if (AdsManager.Instance) {
				AdsManager.Instance.OnNextButton ();
			}
		}
	}

	public void Home(){
		Time.timeScale = 1;
		SceneManager.LoadScene ("MainMenu");
	}

	public void Restart(){
		Time.timeScale = 1;
		SceneManager.LoadScene ("Gameplay");
	}

	public void Resume(){
		Time.timeScale = 1;
		pausePanel.SetActive (false);
	}

	[SerializeField] Galassia.GamePlay.GameCompleted _gameCompleted;

	public void Win(){
//		completePanel.SetActive (true);
//		Time.timeScale = 0;

//		if (AdsManager.Instance) {
//			AdsManager.Instance.OnGameComplete ();
//		}
		Debug.Log("YouWin");
		_gameCompleted.gameObject.SetActive (true);
		StartCoroutine (_gameCompleted.Action ());
		if (AdsManager.Instance) {
			AdsManager.Instance.OnGameComplete ();
		}
	}

	[SerializeField] Galassia.GamePlay.GameFailed _gameFailed;

	public void Lose(){
		Debug.Log("You Lose");
//		failPanel.SetActive (true);
//		StartCoroutine (Action ());
		_gameFailed.gameObject.SetActive (true);
		StartCoroutine (_gameFailed.Action ());
		if (AdsManager.Instance) {
			AdsManager.Instance.OnGameFail ();
		}
	}

	[SerializeField] Galassia.GamePlay.Paused _pause;

	public void OnPauseButtonClick ()
	{
		_pause.gameObject.SetActive (true);
		StartCoroutine (_pause.Action ());
		if (AdsManager.Instance) {
			AdsManager.Instance.OnGamePause ();
		}
	}



}
