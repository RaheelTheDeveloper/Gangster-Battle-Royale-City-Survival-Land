using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Galassia.Store;

public class NoCashPanel : MonoBehaviour
{

	public Button backBtn, watchVideoBtn;
	public GameObject noCashEffect;
	public Text details;
	public Galassia.Store.ItemGroupHandler currentScreen;
	// Use this for initialization

	void Awake ()
	{
		backBtn.onClick.AddListener (() => BackButtonPress ());
		watchVideoBtn.onClick.AddListener (() => WatchVideoPress ());
	}

	void OnEnable ()
	{
		MainMenuController.Instance.HideTopBar ();
		currentScreen.gameObject.SetActive (false);
		details.text = "Watch video to get 100 cash";
			
	
	}

	void BackButtonPress ()
	{
		MainMenuController.Instance.ShowTopBar ();
		InventoryStore.Instance.itemModels.gameObject.SetActive (true);
		currentScreen.gameObject.SetActive (true);
		this.gameObject.SetActive (false);
	}

	void WatchVideoPress ()
	{
		if (AdsManager.Instance.hasUnityAds_RewardedVideo ()) {
			AdsManager.Instance.OnRewardedVideoCallBack += Instance_OnRewardedVideoCallBack;
			AdsManager.Instance.ShowUnityAds_RewardedVideo (RewardType.Cash, 100);
		} else {
			details.text = "Rewarded Video is not available for the moment, check your internet connection";
		}
	}


	private void Instance_OnRewardedVideoCallBack (RewardVideoResults _result, RewardType _rewardType, int _valueOfReward)
	{
		print ("Instance_OnRewardedVideoCallBack");
		if (_result == RewardVideoResults.Completed && _rewardType == RewardType.Cash) {
			StartCoroutine (MainMenuController.Instance.showAddCurrencyParticles (Vector3.zero, _valueOfReward, 0));
			Invoke ("DisablePanel", 1);
		}

		AdsManager.Instance.OnRewardedVideoCallBack -= Instance_OnRewardedVideoCallBack;
	}

	void DisablePanel ()
	{
		MainMenuController.Instance.ShowTopBar ();
		InventoryStore.Instance.itemModels.gameObject.SetActive (true);
		currentScreen.gameObject.SetActive (true);
		gameObject.SetActive (false);
	}
}
