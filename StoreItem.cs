using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Galassia.Store
{
	using Inventory;

	public class StoreItem : MonoBehaviour
	{

		[SerializeField]private Inventory.Details itemDetail;
		[Header ("Weapon Details")]
		[Space (10)]
		public int gunIndex;
		public Text weaponNameText;
		[Space (10)]
		public Text shortDetailsText;
	
		[Space (10)]
		public Image damageBar;
		[Space (10)]
		public Image ammoPerClipBar;

		[Space (10)]
		[Header ("Detail")]
		[SerializeField] Text amountTitleText;
		[SerializeField] Text amounText;
		[SerializeField] Image itemIconImage;
		[SerializeField] Button addItemButton;
		[SerializeField] Text addItemPriceText;
		[SerializeField] Text addAmountText;



		[Header (" Price")]
		[Space (10)]
		public Text cashPrice;
		public Text localizedPrice;
		[Space (10)]
		[Header ("Required Buttons")]
		[Space (10)]
		public GameObject selectedText;
		public GameObject buyViaCash;
		public GameObject buyViaInApp;
		[Space (10)]
		[Header ("Weapon Model")]
		[Space (10)]

		public GameObject itemModel;

		private	ItemGroupHandler groupHandler;

		void Awake ()
		{
			groupHandler = GetComponentInParent<ItemGroupHandler> ();
			buyViaCash.GetComponent<Button> ().onClick.AddListener (() => PurchaseViaCash ());
			buyViaInApp.GetComponent<Button> ().onClick.AddListener (() => PurchaseViaInApp ());
			if (addItemButton) {
				addItemButton.onClick.AddListener (() => OnAddItemClick ());

			}
		}


		public void SetData (int _gunIndex, Details currentDetail, ItemGroupHandler IGHandler)
		{
			gunIndex = _gunIndex;
			itemDetail = currentDetail;
			SetWeaponData ();
			groupHandler = IGHandler;
		}





		void SetWeaponData ()
		{
			weaponNameText.text = itemDetail.type.ToString ();
			shortDetailsText.text = itemDetail.weaponShortDetails;
			if (addItemPriceText) {
				addItemPriceText.text = itemDetail.priceInCash.ToString ("000");
				amounText.text = itemDetail.amount.ToString ("00");
				amountTitleText.text = itemDetail.amountTitle;
				addAmountText.text = itemDetail.pickAmount.ToString ("00");
			}

			itemIconImage.sprite = itemDetail.ivSprite;
			cashPrice.text = itemDetail.priceInCash.ToString ();

			//	print (gameObject.name + " purchsaed " + PlayerPrefs.GetInt (itemDetail.inappSKU, 0));
			if (itemDetail.purchased) {
				buyViaCash.SetActive (false);
				buyViaInApp.SetActive (false);
				selectedText.SetActive (true);
			} 


			SetSpecification ();
			if (groupHandler && groupHandler.hasInApp) {
				SetLocalizePrice ();
			}
		}

		void OnEnable ()
		{
			//print (gameObject.name + " purchsaed " + PlayerPrefs.GetInt (itemDetail.inappSKU, 0));

			if (itemDetail.purchased) {
				buyViaCash.SetActive (false);
				buyViaInApp.SetActive (false);
				selectedText.SetActive (true);
			}
//			if (groupHandler != null && groupHandler.hasInApp && !string.IsNullOrEmpty (itemDetail.inappSKU)) {
//				SetWeaponData ();
//
//			}

		}

		void OnDisable ()
		{
			if (itemModel)
				itemModel.SetActive (false);
		}

		void SetSpecification ()
		{
			damageBar.fillAmount = itemDetail.damageValue / 100;
			ammoPerClipBar.fillAmount = (float)itemDetail.ammoPerClip / 60f;
		}

		void SetLocalizePrice ()
		{
			
			if (AdsManager.Instance) {
				print ("Sku :" + itemDetail.inappSKU);

				localizedPrice.text = AdsManager.Instance.GetLocalizedPrice (itemDetail.inappSKU);
				if (localizedPrice.text == "-- --") {
					localizedPrice.text = "1.99" + "  $";
				}

//				if (AdsManager.Instance.GetLocalizedPrice (itemDetail.inappSKU) != "-- --") {
//
//
//					localizedPrice.text = AdsManager.Instance.GetLocalizedPrice (itemDetail.inappSKU);
//
//				} 
			}

		}

		public void setUpNewIndex (int index)
		{
//			print ("Index :" + index + " gunIndex " + gunIndex);
			if (index == gunIndex) {
				//print("name " + gameObject.name);
				if (itemModel)
					itemModel.SetActive (true);

				if (InventoryStore.Instance)
					InventoryStore.Instance.currentStoreItem = this;
			} else if (itemModel)
				itemModel.SetActive (false);
			
				

		}

		IEnumerator fadeOutButton ()
		{
			float targetAlpha = 0.9f;

			while (targetAlpha != 0.5f) {
				targetAlpha = Mathf.MoveTowards (targetAlpha, 0.5f, 1.0f * Time.deltaTime);
				Color col = GetComponent<Image> ().color;
				col.a = targetAlpha;
				GetComponent<Image> ().color = col;
				yield return null;
			}

		}

		IEnumerator fadeInButton ()
		{
			float targetAlpha = 0.5f;

			while (targetAlpha != 0.9f) {
				targetAlpha = Mathf.MoveTowards (targetAlpha, 0.9f, 1.0f * Time.deltaTime);
				Color col = GetComponent<Image> ().color;
				col.a = targetAlpha;
				GetComponent<Image> ().color = col;
				yield return null;
			}
		}



		#region Purchase

		public void selectWeapon ()
		{
			print ("selectWeapon");
			OnPurchaseAction ();
		}


		void OnAddItemClick ()
		{
			int totalDollars = MainMenuController.Instance.mainMenuManager.GetTotalCashCount ();
			if (totalDollars < itemDetail.priceInCash) {
				// Dont have enough money
				InventoryStore.Instance.noCashPanel.GetComponent<NoCashPanel> ().currentScreen = groupHandler;
				InventoryStore.Instance.noCashPanel.SetActive (true);
				InventoryStore.Instance.itemModels.gameObject.SetActive (false);
			} else {
				// purchase Gun

				print ("Add Item");
				itemDetail.picked = true;
				itemDetail.amount += itemDetail.pickAmount;
				amounText.text = itemDetail.amount.ToString ("00");
				MainMenuController.Instance.mainMenuManager.consumeCash (itemDetail.priceInCash);
				MainMenuController.Instance.updateDollarsText ();
				MainMenuController.Instance.showSpendCurrencyParticles ();

			}

		}

		public void PurchaseViaCash ()
		{
			int totalDollars = MainMenuController.Instance.mainMenuManager.GetTotalCashCount ();
			if (totalDollars < itemDetail.priceInCash) {
				// Dont have enough money
				InventoryStore.Instance.noCashPanel.GetComponent<NoCashPanel> ().currentScreen = groupHandler;
				InventoryStore.Instance.noCashPanel.SetActive (true);
				InventoryStore.Instance.itemModels.gameObject.SetActive (false);
			} else {
				// purchase Gun
		
				print ("purchase Gun");
				OnPurchaseAction ();
				MainMenuController.Instance.mainMenuManager.consumeCash (itemDetail.priceInCash);
				MainMenuController.Instance.updateDollarsText ();
				MainMenuController.Instance.showSpendCurrencyParticles ();

			}
		}

		public void PurchaseViaInApp ()
		{
			if (AdsManager.Instance) {
				AdsManager.Instance.OnItemPurchased += Instance_OnItemPurchased;
				AdsManager.Instance.PurchaseProduct (itemDetail.inappSKU);
			}
		
			print ("weapon : " + itemDetail.inappSKU);
		}

		private void Instance_OnItemPurchased (bool _result, string _productID)
		{
			if (_result) {
				OnPurchaseAction ();
				//AdsManager.Instance.ShowPopUp ("Purchase Successful", "Your Gun is ready to Use", "OK");
				//WeaponStore.Instance.itemModels.gameObject.SetActive(false);
			}
			AdsManager.Instance.OnItemPurchased -= Instance_OnItemPurchased;
		}

		void OnPurchaseAction ()
		{
			MainMenuController.Instance.btnClick.Play ();
			PlayerPrefs.SetInt (itemDetail.inappSKU, 1);
			itemDetail.purchased = true;
			itemDetail.amount = itemDetail.pickAmount;
			selectedText.SetActive (true);
			buyViaCash.SetActive (false);
			buyViaInApp.SetActive (false);
			selectedText.gameObject.SetActive (true);

		}

		#endregion
	}
}
