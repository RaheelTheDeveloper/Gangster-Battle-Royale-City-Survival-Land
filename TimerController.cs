using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TimerController : MonoBehaviour {
	public Text timerText;
	public int totalTime;
	private float timeLeft;
	private int tempTime;
	private int previousTime;
	public GameController gc;
	public string addtionalText;
	public bool detonateBomb , isCalculateTime;
	public bool isCheckForFastBeep;
	public enum TimerType
	{
		bombPlantingTimer,
		oneBombPlantingTime
	}
	public TimerType timerType;
	// Use this for initialization
	void OnEnable () {
		timerText.text = totalTime.ToString()+" sec";
		timeLeft = (float)totalTime;
		tempTime = totalTime;
		previousTime = tempTime;
		isCalculateTime = false;
		isCheckForFastBeep = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isCalculateTime) {
			if (tempTime > 0) {
				timeLeft -= Time.deltaTime;
				tempTime = (int)timeLeft;
				if (tempTime != previousTime) {
					previousTime = tempTime;
					timerText.text = tempTime.ToString () + " sec " + addtionalText;
				}
				if (tempTime <= 5) {
					CheckForSound ();
				}
			} else {
//				gc.TimeUpBalstTheBomb ();
				isCalculateTime = true;
				this.gameObject.SetActive (false);
				//			if (detonateBomb) {
				//				gc.detotanteBomb();
				//			} else {
				//				gc.hostagesBurnt ();
				//			}
			}
		}
	}
	public void CheckForSound(){
		if (timerType == TimerType.bombPlantingTimer) {
			if (!isCheckForFastBeep) {
				isCheckForFastBeep = true;
//				gc.PlayFastBeep ();
			}
		}
	}
}
