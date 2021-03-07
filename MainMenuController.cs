using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour
{
	[SerializeField] List<Screen> screens;
	[SerializeField] private ScreenIndex screenIndex;
	[SerializeField] private Color selectedColor, normalColor;
	public RectTransform topBar;
	public Text totalDollarsText, totalScoreText;
	private int dollars, score;
	public Animator camAnimator;
	public ParticleSystem spendCurrenyParticles;
	public GameObject currencyParent;
	public GameObject welcomeMsgObj;
	public GameObject currencyToAdObj;
	private int menuState = 0;
	public AudioSource btnClick;
	public AudioSource startGameSound;
	public Image soundsImg;
	public Slider sensitivitySlider;
	public bool isClearPlayerPrefs;
	public int unlockLevels = 10;
	public Button removeAds;

	public Image playerIcon;
	public Text playerName;
	public Text MessageText;
	public LevelBtnController[] levelBtns;
	public MainMenuManager mainMenuManager;
	public Galassia.Inventory.InventoryDetail ivDetail;
	public static MainMenuController Instance;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		}


		#if UNITY_EDITOR
		if (isClearPlayerPrefs)
			PlayerPrefs.DeleteAll ();
		#endif

		btnClick = GetComponents<AudioSource> () [0];
		startGameSound = GetComponents<AudioSource> () [1];
		Time.timeScale = 1.0f;

		updateDollarsText ();
		UpdateScore ();
		Invoke ("BlurScreen", 2.0f);
		removeAds.onClick.AddListener (() => RemoveAdsOnClick ());

		PlayerPrefs.SetInt ("weapon_7", 1);
		if (AdsManager.Instance)
			AdsManager.Instance.SetEvent ("MainMenu");
		SetUpGraphicsSettings ();
		SetSound ();
		ShowTopBar ();
		OnMenuButtonClick ();

		Galassia.Controller.RfpsSoundController.Instance.PlayMusic (Galassia.Controller.RfpsSoundController.SoundType.MainMenu);
	}

	int removeadsPopUp = 0;


//	void OnEnable ()
//	{
//		if (AdsManager.Instance) {
//			AdsManager.Instance.SetCanvas (this.gameObject);
//		}
//
//		ShowRemoveAdsPopUp ();
//
//		if (AdsManager.Instance) {
//			AdsManager.Instance.ShowBannerBottom ();
//		}
//		Invoke ("InvokeBanner", 3.0f);
//	}

//	void InvokeBanner ()
//	{
//		if (AdsManager.Instance) {
//			AdsManager.Instance.ShowBannerBottom ();
//		}
//	}

	#region GameScreen

	[System.Serializable]
	public class Screen
	{
		[SerializeField] private string name;
		public GameObject _Object;
		public Image buttonImage;
		public Sprite normal, selected;
		public bool hasTopBar = true;

	}


	[SerializeField]private int currentIndex = 0;
	[SerializeField]private int previousIndex = 0;


	public void OpenScreen (int index)
	{
		if (currentIndex.Equals (index)) {
			return;
		}
		previousIndex = currentIndex;
		if (screens [previousIndex]._Object) {
			screens [previousIndex]._Object.SetActive (false);

			if (screens [previousIndex].normal) {
				screens [previousIndex].buttonImage.sprite = screens [previousIndex].normal;
			} else {
				screens [previousIndex].buttonImage.color = normalColor;
			}

		}


		currentIndex = index;
	
		if (screens [currentIndex]._Object) {
			screens [currentIndex]._Object.SetActive (true);

			if (screens [currentIndex].selected) {
				screens [currentIndex].buttonImage.sprite = screens [currentIndex].selected;
			} else {
				screens [currentIndex].buttonImage.color = selectedColor;
			}
			if (screens [currentIndex].hasTopBar && !isTopBar) {
				ShowTopBar ();
			} else if (!screens [currentIndex].hasTopBar && isTopBar) {
				HideTopBar ();
			} 
		}
	}

	private bool isTopBar = false;

	public void ShowTopBar ()
	{
		isTopBar = true;
		LeanTween.move (topBar, new Vector3 (0, 0, 0), 0.15f).setEase (LeanTweenType.linear);
	}

	public void HideTopBar ()
	{
		isTopBar = false;
		LeanTween.move (topBar, new Vector3 (0, 130, 0), 0.15f).setEase (LeanTweenType.linear);
	}


	#endregion


	#region Button Click


	[System.Serializable]
	[SerializeField] private class ScreenIndex
	{
		public int store = 4;
		public int moregame = 8;
		public int settings = 3;
		public int menu = 1;
		public int inventory = 2;
		public int loading = 7;
		public int freeSpin = 5;
		public int mission = 6;
		public int exit = 0;
		public int stats = 9;
		public int achievements = 10;

	}




	public void OnExitButtonClicked ()
	{


		switch (currentIndex) {

		case 1:
			ExitGame ();
			break;
		default:
			OnMenuButtonClick ();
			break;
		}

	

	}

	void ExitGame ()
	{
		btnClick.Play ();
		OpenScreen (screenIndex.exit);
		if (AdsManager.Instance) {
			AdsManager.Instance.OnExitScreen ();
			AdsManager.Instance.SetEvent ("ExitScreen");

		}
	}

	public void OnPlayButtonClick ()
	{
		if (currentIndex == screenIndex.menu) {
			OnInventoryButtonClick ();
		} else if (currentIndex == screenIndex.inventory) {
			OnMissionButtonClick ();
		} else if (currentIndex == screenIndex.mission) {
			OnLoadingButtonClick ();
		} else {
			OnInventoryButtonClick ();
		}
	}

	public void OnMissionButtonClick ()
	{
		btnClick.Play ();
		OpenScreen (screenIndex.mission);
	}

	public void OnFreeSpinButtonClick ()
	{
		btnClick.Play ();
		OpenScreen (screenIndex.freeSpin);
	}

	public void OnInventoryButtonClick ()
	{
		btnClick.Play ();
		OpenScreen (screenIndex.inventory);

	}



	private Loading _loading;

	public void OnLoadingButtonClick ()
	{

		//StartCoroutine (fadeOutMusic ());

		btnClick.Play ();
		OpenScreen (screenIndex.loading);

//		if (AdsManager.Instance) {
//			AdsManager.Instance.ShowBannerTop ();
//		}

		if (_loading == null) {
			_loading = screens [screenIndex.loading]._Object.GetComponent <Loading> ();
		}
		_loading.LoadScene ("Gameplay");

	}

	public void OnMenuButtonClick ()
	{
		btnClick.Play ();
		OpenScreen (screenIndex.menu);
	}


	public void OnStoreButtonClick ()
	{
		btnClick.Play ();
		OpenScreen (screenIndex.store);
	}


	public void OnRateUsButtonClick ()
	{
		Application.OpenURL ("https://play.google.com/store/apps/details?id=" + Application.identifier);
	}


	public void OnMoreGameButtonClick ()
	{
		btnClick.Play ();
		OpenScreen (screenIndex.moregame);
		if (AdsManager.Instance) {
			AdsManager.Instance.OnMoreGames ();
		}
	}

	public void OnSettingsButtonClick ()
	{
		btnClick.Play ();
		sensitivitySlider.value = mainMenuManager.SensitivityValue;
		OpenScreen (screenIndex.settings);
		if (AdsManager.Instance) {
			AdsManager.Instance.OnSettingsScreen ();
		}
	}

	public void OnStatsButtonClick ()
	{
		btnClick.Play ();
		OpenScreen (screenIndex.stats);
	}

	public void OnAchievementsButtonClick ()
	{
		btnClick.Play ();
		OpenScreen (screenIndex.achievements);
	}

	#endregion



	#region MainMenu

	void ShowRemoveAdsPopUp ()
	{
		removeadsPopUp = PlayerPrefs.GetInt ("removeadsPopUp", 1);
		if (removeadsPopUp % 2 == 0) {
			if (AdsManager.Instance) {
				AdsManager.Instance.ShowRemoveAdsPopUp ();
			}
		}
		PlayerPrefs.SetInt ("removeadsPopUp", PlayerPrefs.GetInt ("removeadsPopUp", 1) + 1);
	}




	public void ShowStartReward ()
	{
		if ((bool)mainMenuManager && !mainMenuManager.detail.isCollectedStartCash) {
			mainMenuManager.detail.isCollectedStartCash = true;
			welcomeMsgObj.SetActive (true);
		}
	}

	public void takeStartCashOnClick ()
	{
		btnClick.Play ();
		welcomeMsgObj.SetActive (false);
		StartCoroutine (showAddCurrencyParticles (Vector3.zero, 250, 0));
	}



	#endregion

	#region ExitScreen

	public void OnGameQuit ()
	{
		Application.Quit ();
	}

	public void OnBackToMainMenu ()
	{
		OnMenuButtonClick ();
	}

	#endregion

	#region Currency

	public void updateDollarsText ()
	{
		dollars = mainMenuManager.GetTotalCashCount ();
		totalDollarsText.text = dollars.ToString ();
	}

	public void UpdateScore ()
	{
		score = mainMenuManager.GetTotalXPCount ();
		totalScoreText.text = score.ToString ();
	}

	public void showSpendCurrencyParticles ()
	{
		spendCurrenyParticles.Emit (15);
		spendCurrenyParticles.GetComponent<AudioSource> ().Play ();
		currencyParent.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		LeanTween.scale (currencyParent, Vector3.one, 0.2f).setEase (LeanTweenType.easeOutBack);
		updateDollarsText ();
	}

	public IEnumerator showAddCurrencyParticles (Vector3 startPos, int amountToAd, float startWait)
	{
		yield return new WaitForSeconds (startWait);
		currencyToAdObj.GetComponentInChildren<Text> ().text = amountToAd.ToString ();
		currencyToAdObj.transform.localScale = Vector3.one * 1.5f;

		currencyToAdObj.SetActive (true);
		LeanTween.scale (currencyToAdObj, Vector3.one, 0.5f).setEase (LeanTweenType.linear);

		LeanTween.moveLocal (currencyToAdObj, new Vector3 (500, 323, 0), 0.5f).setEase (LeanTweenType.easeInBack);
		yield return new WaitForSeconds (0.5f);
		spendCurrenyParticles.Emit (15);
		spendCurrenyParticles.GetComponent<AudioSource> ().Play ();
		currencyParent.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		LeanTween.scale (currencyParent, Vector3.one, 0.2f).setEase (LeanTweenType.easeOutBack);
		currencyToAdObj.SetActive (false);

		print ("showAddCurrencyParticles");
		mainMenuManager.addCash (amountToAd);

		totalDollarsText.text = mainMenuManager.GetTotalCashCount ().ToString ();
	}

	public void addCurrency (int val)
	{
		StartCoroutine (showAddCurrencyParticles (Vector3.zero, val, 1.5f));
	}

	#endregion

	#region Settings Screen


	public void soundsBtnOnClick ()
	{
		mainMenuManager.IsSoundOn = !mainMenuManager.IsSoundOn;
		SetSound ();
	}

	void SetSound ()
	{
		soundsImg.sprite = mainMenuManager.getSoundIcon (mainMenuManager.IsSoundOn);
		soundsImg.SetNativeSize ();
		AudioListener.pause = mainMenuManager.IsSoundOn;
		AudioListener.volume = mainMenuManager.SoundVolume;
	}

	public Dropdown graphicsSettingsDropDown;

	public void SetUpGraphicsSettings ()
	{
		if (graphicsSettingsDropDown == null) {
			Debug.LogError ("graphicsSettingsDropDown is null, assign it");
			return;
		}

		switch (graphicsSettingsDropDown.value) {
		case 0:
			{
				mainMenuManager.SetGraphicsSettingsLevel (GraphicsSettings.Low);
				QualitySettings.SetQualityLevel (0, true);
				break;
			}
		case 1:
			{
				mainMenuManager.SetGraphicsSettingsLevel (GraphicsSettings.Medium);
				QualitySettings.SetQualityLevel (1, true);
				break;
			}
		case 2:
			{
				mainMenuManager.SetGraphicsSettingsLevel (GraphicsSettings.High);
				QualitySettings.SetQualityLevel (2, true);
				break;
			}
		}
	}

	#endregion

	#region InApps

	void RemoveAdsOnClick ()
	{
		if (AdsManager.Instance) {
			AdsManager.Instance.OnItemPurchased += Instance_OnPurchaseSucceedCallBack;
			AdsManager.Instance.PurchaseProduct ("remove_ads");
		}
		if (AdsManager.Instance)
			AdsManager.Instance.SetEvent ("RemoveAdsOnClick");
	}

	private void Instance_OnPurchaseSucceedCallBack (bool _result, string _productID)
	{
		if (_productID == "remove_ads") {
			PlayerPrefs.SetString ("RemoveAds", "true");
			MessageText.gameObject.SetActive (true);
			MessageText.text = "Ads are successfully removed";
			Invoke ("HideMessgae", 4);
		}
		AdsManager.Instance.OnItemPurchased -= Instance_OnPurchaseSucceedCallBack;
	}

	public void PurchaseUnlockAll ()
	{
		if (AdsManager.Instance) {
			AdsManager.Instance.OnItemPurchased += Instance_OnItemPurchased;
			AdsManager.Instance.PurchaseProduct ("unlock_all");
		}
	}

	private void Instance_OnItemPurchased (bool _result, string _productID)
	{
		if (_result && _productID == "unlock_all") {
			mainMenuManager.addCash (10000);
			mainMenuManager.addCoins (1000);

			foreach (var item in  ivDetail.ivList) {
				if (item.uiGroup == Galassia.Generic.InventoryGroup.Weapon) {
					item.purchased = true;
					item.amount = item.pickAmount;
				}
			}
			AdsManager.Instance.ShowPopUp ("UnlockAll", "Everything is unlocked", "Done", "", "Enjoy good shooting with 15 different weapons in all intense action packed missions");

		}
		AdsManager.Instance.OnItemPurchased -= Instance_OnItemPurchased;
	}

	#endregion



	#region RewardedVideo

	public void watchVideoOnClick ()
	{
		btnClick.Play ();

		if (AdsManager.Instance.hasUnityAds_RewardedVideo ()) {
			AdsManager.Instance.OnRewardedVideoCallBack += Instance_OnRewardedVideoCallBack1;
			AdsManager.Instance.ShowUnityAds_RewardedVideo (RewardType.Cash, 100);
		} else {
			ShowMessageText ("Check Your Internet Connection...");
		}


	}

	private void Instance_OnRewardedVideoCallBack1 (RewardVideoResults _result, RewardType _rewardType, int _valueOfReward)
	{
		print ("Instance_OnRewardedVideoCallBack1");
		if (_result == RewardVideoResults.Completed)
			StartCoroutine (showAddCurrencyParticles (Vector3.zero, _valueOfReward, 0));
	}

	void HideMessgae ()
	{
		MessageText.gameObject.SetActive (false);
		CancelInvoke ("HideMessgae");
	}

	public void WatchVideoForGrenades ()
	{
		if (AdsManager.Instance) {
			AdsManager.Instance.OnRewardedVideoCallBack += Instance_OnRewardedVideoCallBack;
			AdsManager.Instance.ShowUnityAds_RewardedVideo (RewardType.Grenades, 2);
		}
	}

	private void Instance_OnRewardedVideoCallBack (RewardVideoResults _result, RewardType _rewardType, int _valueOfReward)
	{
		print ("Instance_OnRewardedVideoCallBack");
		if (_result != RewardVideoResults.Completed) {
			return;
		}
		if (_rewardType == RewardType.Grenades) {
			int currentGrenadesCount = PlayerPrefs.GetInt ("Grenades", 0);
			currentGrenadesCount += _valueOfReward;
			PlayerPrefs.SetInt ("Grenades", currentGrenadesCount);
			// UIController.Instance.updateGrenadeCounter(currentGrenadesCount);
			MessageText.gameObject.SetActive (true);
			MessageText.text = _valueOfReward + " Grenades are added to your inventory";
			Invoke ("HideMessgae", 4);
			print ("1 grenade added");
		}
		AdsManager.Instance.OnRewardedVideoCallBack -= Instance_OnRewardedVideoCallBack;
	}

	void ShowMessageText (string _mgs)
	{
		MessageText.gameObject.SetActive (true);
		MessageText.text = _mgs;
		Invoke ("HideMessgae", 4);
		
	}


	public void addRewardAfterWatchingAd ()
	{
		StartCoroutine (showAddCurrencyParticles (Vector3.zero, 100, 0));
	}

	#endregion


	public void levelSelected (int num)
	{
		btnClick.Play ();
		if (PlayerPrefs.GetInt ("LevelsCleared", 0) + 1 >= num) {
			for (int i = 0; i < levelBtns.Length; i++) {
				if (i != num - 1)
					levelBtns [i].selectedCircle.SetActive (false);
				else
					levelBtns [i].selectedCircle.SetActive (true);
			}
			Globals.currentLevelNumber = num;
		}


	}


	public void OnSensitivityValueChanged ()
	{
		mainMenuManager.SensitivityValue = sensitivitySlider.value;
		//Debug.Log (sensitivitySlider.value);
	}

	public void tetsPuchaseOnClick ()
	{
		//uncomment after adding AdsManager
		//AdsManager.Instance.Purchase_Consumable("android.test.purchased");
	}

	public void ShowLeaderBoard ()
	{
		//uncomment after adding AdsManager
		//if (LeaderBoardManager.instance)
		//    LeaderBoardManager.instance.ShowLeaderBoardUI();
	}

	public void ShowAchievementUi ()
	{
		//uncomment after adding AdsManager
		//if (LeaderBoardManager.instance)
		//    LeaderBoardManager.instance.ShowAchievementUI();
	}

	public void ResetAchievements ()
	{
		//uncomment after adding AdsManager
		//if (LeaderBoardManager.instance)
		//    LeaderBoardManager.instance.ResetAchievements();
	}


	public void OnConsentButtonClick ()
	{
		if (AdsManager.Instance) {
			AdsManager.Instance.ShowConsentPopup ();
		}
	}
}
