using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Galassia.Store
{
	using Generic;

	public class ItemGroupHandler : MonoBehaviour
	{
		private int currentGunIndex = 0;
		public Button navigateNextButton;
		public Button navigatePreviousButton;
		public RectTransform buttonsParent;
		public List<StoreItem> weapons;
		public InventoryStore weaponStore;
		public MainMenuController mainMenu;
		public static ItemGroupHandler Instance;
		public InventoryGroup ivGroup;
		public StoreItem weaponPrefab;
		public float itemGap = 700;
		public List< Inventory.Details> allWeapons;
		public bool hasInApp = false;

		void OnEnable ()
		{
			if (dataFetched)
				updateButtonsInfo ();
			if (!PlayerPrefs.HasKey ("UserFirstTime")) {
				PlayerPrefs.SetInt ("UserFirstTime", 0);
			}
		}

		bool dataFetched = false;

		void Start ()
		{
			if (Instance == null)
				Instance = this;

			navigateNextButton.GetComponent<Button> ().onClick.AddListener (() => NavigateNext ());
			navigatePreviousButton.GetComponent<Button> ().onClick.AddListener (() => NavigatePrevious ());

			for (int i = 0; i < weaponStore.allWeaponsList.Count; i++) {
				if (weaponStore.allWeaponsList [i].uiGroup.Equals (ivGroup)) {
					allWeapons.Add (weaponStore.allWeaponsList [i]);
				}
			}

			for (int i = 0; i < allWeapons.Count; i++) {
				StoreItem weapon = Instantiate (weaponPrefab, buttonsParent.transform);
				weapon.SetData (i, allWeapons [i], this);
				weapon.gameObject.name = allWeapons [i].type.ToString ();
				SetModel (weapon);
				weapon.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (weapon.GetComponent<RectTransform> ().anchoredPosition.x, itemGap * (i));
				weapons.Add (weapon);
			}
			dataFetched = true;
			updateButtonsInfo ();

			if (allWeapons.Count == 0) {
				Debug.LogError (ivGroup + " Weapons count is zero in inventory");
			}
		}

		void SetModel (StoreItem weapon)
		{

			foreach (var item in weaponStore.itemModels.groups) {
				if (item.groupType == ivGroup) {
					for (int i = 0; i < item.models.Count; i++) {
						if (item.models [i].type.ToString () == weapon.gameObject.name) {
							weapon.itemModel = item.models [i].model;
							return;
						}
					}
				}
			}
			if (weapon.itemModel == null) {
				Debug.Log ("No Model found in " + WeaponGroup.Pistol.ToString () + " array named " + weapon.gameObject.name);
			}


		}



		public void NavigateNext ()
		{
			mainMenu.btnClick.Play ();
			currentGunIndex++;
			//Debug.Log (currentGunIndex);
			LeanTween.move (buttonsParent, new Vector3 (buttonsParent.anchoredPosition.x, currentGunIndex * -itemGap, 0), 0.4f).setEase (LeanTweenType.easeOutBack);
			updateButtonsInfo ();
		}

		public void NavigatePrevious ()
		{
			mainMenu.btnClick.Play ();
			currentGunIndex--;
			Debug.Log (currentGunIndex);
			LeanTween.move (buttonsParent, new Vector3 (buttonsParent.anchoredPosition.x, currentGunIndex * -itemGap, 0), 0.4f).setEase (LeanTweenType.easeOutBack);
			updateButtonsInfo ();
		}

		void updateButtonsInfo ()
		{
			if (weapons == null || weapons.Count == 0 || weapons.Count == 1) {
				navigateNextButton.interactable = false;
				navigatePreviousButton.interactable = false;
				if (weapons.Count == 1) {
					weapons [0].setUpNewIndex (0);
				}
				return;
			}

			if (currentGunIndex == 0)
				navigatePreviousButton.interactable = false;
			else
				navigatePreviousButton.interactable = true;

			if (currentGunIndex == weapons.Count - 1)
				navigateNextButton.interactable = false;
			else
				navigateNextButton.interactable = true;

			for (int i = 0; i < weapons.Count; i++) {
				weapons [i].setUpNewIndex (currentGunIndex);
			}
		}

		public void doneSelection ()
		{
			mainMenu.btnClick.Play ();
			gameObject.SetActive (false);
			//weaponsMainPopupObj.SetActive(true);
		}



	

	

	
	}
}