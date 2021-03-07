using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlantBomb : MonoBehaviour {

	public float bombPlantTime = 5;
	float currentTime , requiredTime = 0;
	bool isPlant;
	public GameObject animatedBombWitghHands , BombwithInAnimatedHands , magicBomb , fpsPlayer , justBomb;
	public Text justBombText;
	public PlantedBombBehaviour plantedBomb;
	public GameObject[] fpsStuffToBeDisable;
	public UIController ui;
	public GameController gc;
	public AnimationEventsReceiver eventsReceiver;
	public string plantedBombDigits = "";
	GameObject bombTriggerObj;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isPlant) {
			if (currentTime > requiredTime) {
				isPlant = false;
				StartCoroutine(TimeOverNowPlantBomb ());
			} else {
				currentTime += Time.deltaTime;
				ui.FillBombTimer (requiredTime , currentTime , currentTime);
			}
		}
	}

	public void EnableBombScenerio(bool isEnable){
		animatedBombWitghHands.transform.position = fpsPlayer.transform.position;
		animatedBombWitghHands.SetActive (isEnable);
		fpsPlayer.transform.gameObject.SetActive (!isEnable);
		for (int i = 0; i < fpsStuffToBeDisable.Length; i++) {
			fpsStuffToBeDisable [i].SetActive (!isEnable);
		}
	}

	public void StartBombPlanting(bool isStartPlant , GameObject triggerObj){
		currentTime = 0;
		isPlant = isStartPlant;
		requiredTime = currentTime + bombPlantTime;
		if (animatedBombWitghHands.GetComponent<Animator>()) 
			animatedBombWitghHands.GetComponent<Animator>().SetBool ("startBombPlant" , isStartPlant);
		eventsReceiver.EmptyString ();
		bombTriggerObj = triggerObj;
	}
	IEnumerator TimeOverNowPlantBomb(){
		if (bombTriggerObj != null) bombTriggerObj.SetActive (false);
		plantedBombDigits = eventsReceiver.GetBombDigits ();
		yield return new WaitForSeconds (0.1f);
		if (animatedBombWitghHands.GetComponent<Animator>()) 
			animatedBombWitghHands.GetComponent<Animator>().SetTrigger ("throwBomb");
		BombwithInAnimatedHands.SetActive (false);
		ui.EnableBombFiller (false);
		if (animatedBombWitghHands.GetComponent<Animator>()) 
			animatedBombWitghHands.GetComponent<Animator>().SetBool ("startBombPlant" , false);
		justBomb.SetActive (true);
		justBombText.text = plantedBombDigits;
		justBomb.transform.position = magicBomb.transform.position;
		yield return new WaitForSeconds (1.0f);
		ui.EnableBombHands (false);
		gc.ShowBombPlantedUi ();
		yield return new WaitForSeconds (1.5f);
		BombwithInAnimatedHands.SetActive (true);
		justBomb.SetActive (false);
		SetBombPositionAndText ();
//		GameController.instance.ShowNewBombButton (false, null);
	}

	HurdleBehaviour hurdleBehaviour;
	public void SetBombPositionAndText(){
		if (bombTriggerObj != null) {
//			hurdleBehaviour = bombTriggerObj.transform.parent.GetComponent<HurdleBehaviour> ();
			if (bombTriggerObj.GetComponent<TriggerObjectBehaviour> ()) hurdleBehaviour = bombTriggerObj.GetComponent<TriggerObjectBehaviour> ().hurdleBehaviour;
			if (hurdleBehaviour == null) Debug.Log ("no script found on the parent of trigger");
			hurdleBehaviour.bomb.SetActive(true);
			plantedBomb = hurdleBehaviour.bomb.GetComponent<PlantedBombBehaviour> ();
			plantedBomb.SetString (plantedBombDigits);
		}
	}
}
