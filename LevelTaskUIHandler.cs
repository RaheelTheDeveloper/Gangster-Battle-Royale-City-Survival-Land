using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelTaskUIHandler : MonoBehaviour {

	public static LevelTaskUIHandler instance;
	public GameObject bombPlantUiObj, totalEnemiesUiObj, headShotsUiObj, specialGunObj, grenadeUiOBj;
	public Text bombPlantText, totalEnemiesText, headShotText, specialGunNameText, specialGunKillCountText, grenadeText;
	LevelController levelController;
	public UIElementAnimationController animController;
	public UIElementAnimationController bombTaskAnim , totalEnemiesTaskAnim , headShotTaskAnim , specialGunTaskAnim , grenadeTaskAnim;
	public Image gunImage;
	public Sprite knifeSprite, m1911Sprite, ak47Sprite, mp4Sprite, mp5Sprite, sniperSprite, shotGunSprite;

	void OnEnable(){
		if (instance == null)
			instance = this;
	}
	void Start(){
		levelController = LevelController.instance;
	}
	public void ShowTaskPanelAnim(){
		animController.enabled = true;
	}

	public void SetLevelTaskUI(bool isBombPlant , bool isTotalEnemies , bool isHeadShot , bool isSpecialGun , bool isGrenade){
		bombPlantUiObj.SetActive (isBombPlant);
		totalEnemiesUiObj.SetActive (isTotalEnemies);
		headShotsUiObj.SetActive (isHeadShot);
		specialGunObj.SetActive (isSpecialGun);
		grenadeUiOBj.SetActive (isGrenade);
		UpdateTaskUI ();
	}
	public void UpdateTaskUI(){
		UpdatePlantBombUI();
		UpdateTotalEnemiesUI ();
		UpdateHeadShotUI ();
		UpdateSpecialGunUI ();
		UpdateGrenadeUI ();
	}

	void UpdatePlantBombUI(){
		if (levelController && GameController.instance.isBombPlant) 
			bombPlantText.text = GameController.instance.plantedBombCount + "/" + GameController.instance.totalLevelBombs;
		else 
			levelController = LevelController.instance;
	}
	void UpdateTotalEnemiesUI(){
		if (levelController && GameController.instance.isShootEnemies) 
			totalEnemiesText.text = GameController.instance.killedEnemies + "/" + GameController.instance.totalEnemiesCount;
		else if(levelController == null)
			levelController = LevelController.instance;
	}
	void UpdateHeadShotUI(){
		if (levelController && GameController.instance.isHeadShotKill) 
			headShotText.text = GameController.instance.KilledByHeadShots + "/" + GameController.instance.headShotCount;
		else if(levelController == null)
			levelController = LevelController.instance;
	}
	void UpdateSpecialGunUI(){
		if (levelController && GameController.instance.isSpecialGunKill) {
			specialGunNameText.text = GameController.instance.specialGunName;
			specialGunKillCountText.text = GameController.instance.specialGunKilled + "/" + GameController.instance.specialGunCount;
			SetGunImage ();
		}
		else if(levelController == null)
			levelController = LevelController.instance;
	}
	void UpdateGrenadeUI(){
		if (levelController && GameController.instance.isKillWithGrenade) 
			grenadeText.text = GameController.instance.killedWithGrenade + "/" + GameController.instance.grenadeKillCount;
		else if(levelController == null)
			levelController = LevelController.instance;
	}
	void SetGunImage(){
		if (GameController.instance.SpecialGunId == 1)
			gunImage.sprite = m1911Sprite;
		else if (GameController.instance.SpecialGunId == 2)
			gunImage.sprite = mp5Sprite;
		else if (GameController.instance.SpecialGunId == 3)
			gunImage.sprite = ak47Sprite;
		else if (GameController.instance.SpecialGunId == 4)
			gunImage.sprite = mp4Sprite;
		else if (GameController.instance.SpecialGunId == 5)
			gunImage.sprite = sniperSprite;
		else if (GameController.instance.SpecialGunId == 7)
			gunImage.sprite = knifeSprite;
		else if (GameController.instance.SpecialGunId == 8)
			gunImage.sprite = shotGunSprite;
	}
	public void ShowTaskCompletedAnim(string taskName){
		StartCoroutine (TaskCompleted (taskName));
		PlayAchievementSound ();
	}

	IEnumerator TaskCompleted (string name){
		yield return new WaitForSeconds (0.5f);
		if (name.Equals ("Bomb")){
			bombTaskAnim.enabled = true;
			PlayAchievementSound ();

			yield return new WaitForSeconds (1.5f);
			bombPlantUiObj.SetActive (false);
		}
		else if (name.Equals("TotalEnemies")) {
			totalEnemiesTaskAnim.enabled = true;
			PlayAchievementSound ();

			yield return new WaitForSeconds (1.5f);
			totalEnemiesUiObj.SetActive (false);
		}
		else if (name.Equals("Head")) {
			headShotTaskAnim.enabled = true;
			PlayAchievementSound ();

			yield return new WaitForSeconds (1.5f);
			headShotsUiObj.SetActive (false);
		}
		else if (name.Equals("SpecialGun")) {
			specialGunTaskAnim.enabled = true;
			PlayAchievementSound ();

			yield return new WaitForSeconds (1.5f);
			specialGunObj.SetActive (false);
		}
		else if (name.Equals("Grenade")) {
			grenadeTaskAnim.enabled = true;
			PlayAchievementSound ();

			yield return new WaitForSeconds (1.5f);
			grenadeUiOBj.SetActive (false);
		}
	}

	public AudioClip[] achievementSound;
	public AudioSource audioSource;
	int val = 0;
	void PlayAchievementSound(){
		val = Random.Range (0, achievementSound.Length);
		audioSource.PlayOneShot (achievementSound [val]);
	}
//	public void Update
}
