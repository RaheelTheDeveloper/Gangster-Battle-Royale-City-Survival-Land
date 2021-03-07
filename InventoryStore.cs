using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Galassia.Store
{
	using Inventory;
	using Generic;

	public class InventoryStore : MonoBehaviour
	{


		private int selectedGunIndex;

		public GameObject noCashPanel;
		public GameObject noVideoEffect;
		public ItemGroupHandler[] itemGroupHandlers;
		public StoreItem currentStoreItem;
		public ItemModels itemModels;
		public static InventoryStore Instance;
		[HideInInspector]
		public InventoryDetail inventory;

		public List<Details> allWeaponsList;

		void Awake ()
		{

			if (Instance == null)
				Instance = this;
			if (inventory == null) {
				if (MainMenuController.Instance)
					inventory = MainMenuController.Instance.ivDetail;
				
			}
			for (int i = 0; i < inventory.ivList.Count; i++) {
				for (int weaponIndex = 0; weaponIndex < itemGroupHandlers.Length; weaponIndex++) {
					if (inventory.ivList [i].uiGroup == itemGroupHandlers [weaponIndex].ivGroup) {
						allWeaponsList.Add (inventory.ivList [i]);
					}
				}

			}
			if (allWeaponsList.Count == 0) {
				Debug.LogError ("Weapon count is zero");
			}
      
		}

		// Use this for initialization
		void OnEnable ()
		{
      
			noCashPanel.SetActive (false);

			if (!PlayerPrefs.HasKey ("FirstTime")) {
				PlayerPrefs.SetInt ("FirstTime", 0);
			}

  
			itemModels.gameObject.SetActive (true);
			selectedWeaponPanel = -1;
			OpenWeaponPanel (0);
			checkGunStats ();
		}

		void OnDisable ()
		{
			for (int i = 0; i < itemGroupHandlers.Length; i++) {
				itemGroupHandlers [i].gameObject.SetActive (false);
			}
		}

		void checkGunStats ()
		{
			//selectedGunIndex = PlayerPrefs.GetInt ("SelectedGunIndex", -1);

			//if (PlayerPrefs.GetInt("LevelsCleared", 0) >= 0)
			//{
			//    grandesLockedBlock.SetActive(false);
			//    grenadesUnlockBlock.SetActive(true);
			//    grenadeQuanityText.text = PlayerPrefs.GetInt("Grenades", 0).ToString();
			//    flashbangQuantityText.text = PlayerPrefs.GetInt("Flashbang", 0).ToString();
			//}
			//else
			//{
			//    grandesLockedBlock.SetActive(true);
			//    grenadesUnlockBlock.SetActive(false);
			//}
		}

		public void buyFlashbangOnClick ()
		{
			//int totalDollars = PlayerPrefs.GetInt("Dollars", 0);
			//if (totalDollars >= 600)
			//{
			//    PlayerPrefs.SetInt("Dollars", totalDollars - 600);
			//    //  mainMenu.updateDollarsText();
			//    //  mainMenu.showSpendCurrencyParticles();
			//    int flashBangs = PlayerPrefs.GetInt("Flashbang", 0);
			//    flashBangs++;
			//    PlayerPrefs.SetInt("Flashbang", flashBangs);
			//    flashbangQuantityText.text = flashBangs.ToString();
			//}
			//else
			//{
			//    noCashPanel.SetActive(true);
			//}
		}

		public void buyGrenadeOnClick ()
		{
			//  mainMenu.btnClick.Play();
			//int totalDollars = PlayerPrefs.GetInt("Dollars", 0);
			//if (totalDollars >= 600)
			//{
			//    PlayerPrefs.SetInt("Dollars", totalDollars - 600);
			//    //    mainMenu.updateDollarsText();
			//    //    mainMenu.showSpendCurrencyParticles();
			//    int grenades = PlayerPrefs.GetInt("Grenades", 0);
			//    grenades++;
			//    PlayerPrefs.SetInt("Grenades", grenades);
			//    grenadeQuanityText.text = grenades.ToString();
			//}
			//else
			//{
			//    noCashPanel.SetActive(true);
			//}
		}

		public void AddGrenade (bool isFlash, int amount)
		{
			Debug.Log ("bool is : " + isFlash + "amount is : " + amount);
			//if (isFlash)
			//{
			//    //			mainMenu.updateDollarsText ();
			//    // mainMenu.showSpendCurrencyParticles();
			//    int flashBangs = PlayerPrefs.GetInt("Flashbang", 0);
			//    //			flashBangs++;
			//    flashBangs += amount;
			//    PlayerPrefs.SetInt("Flashbang", flashBangs);
			//    flashbangQuantityText.text = flashBangs.ToString();
			//}
			//else
			//{
			//    //			mainMenu.updateDollarsText ();
			//    //   mainMenu.showSpendCurrencyParticles();
			//    int grenades = PlayerPrefs.GetInt("Grenades", 0);
			//    //			grenades++;
			//    grenades += amount;
			//    PlayerPrefs.SetInt("Grenades", grenades);
			//    grenadeQuanityText.text = grenades.ToString();
			//}
		}

		public void WatchVideo (bool isFlashBang)
		{
			//			if (AdsManager.Instance) {
			////				AdsManager.Instance.ShowUnityAds_RewardedVideo ();
			//				if (Advertisement.IsReady(AdsManager.Instance.UnityAds_zoneID)) {
			//				if (isFlashBang) 
			//					AdsManager.Instance.ShowUnityAds_RewardedVideo ("FlashBang", 3);
			//				else 
			//					AdsManager.Instance.ShowUnityAds_RewardedVideo ("DamageGrenade", 3);
			//				}
			//			else if (HZIncentivizedAd.IsAvailable ()) {
			//				if(isFlashBang)	
			//					AdsManager.Instance.ShowHeyzApp_RewardsVideo ("FlashBang", 3);
			//				else 
			//					AdsManager.Instance.ShowHeyzApp_RewardsVideo ("DamageGrenade", 3);
			//			}
			//			else if (Chartboost.hasRewardedVideo(CBLocation.Default)) {
			//				if(isFlashBang)	
			//					AdsManager.Instance.ShowChartBoost_RewardedVideo ("FlashBang", 3);
			//				else 
			//					AdsManager.Instance.ShowChartBoost_RewardedVideo ("DamageGrenade", 3);
			//			} else {
			//				noVideoEffect.SetActive (true);
			//				Invoke ("OffEffect", 2f);
			//		}
			//		}
		}

		void OffEffect ()
		{
			noVideoEffect.SetActive (false);
		}

		int selectedWeaponPanel = -1;

		[SerializeField] List<RectTransform> listOfGroupButtons;

		public void OpenWeaponPanel (int weaponsPanelIndex)
		{
			if (weaponsPanelIndex == selectedWeaponPanel) {
				return;
			}
			if (selectedWeaponPanel >= 0) {
				itemGroupHandlers [selectedWeaponPanel].gameObject.SetActive (false);
			}
			itemGroupHandlers [weaponsPanelIndex].gameObject.SetActive (true);
			selectedWeaponPanel = weaponsPanelIndex;
			if (listOfGroupButtons.Count > selectedWeaponPanel) {
				foreach (var item in listOfGroupButtons) {
					if (item.gameObject) {
						item.gameObject.SetActive (false);
					}
				}
				listOfGroupButtons [selectedWeaponPanel].gameObject.SetActive (true);
		
			}
		}


	}
}