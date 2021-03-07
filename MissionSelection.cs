using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MissionSelection : MonoBehaviour
{
	[System.Serializable]
	public class Mission
	{
		[SerializeField] string name;
		public GameObject _Object;
		public Image lockImage;
		public Image progressBar;
		public Text percentage;
		
	}

	[SerializeField] private List<Mission> missions;

	// Use this for initialization
	void Start ()
	{
//		clearedMissions = MainMenuController.Instance.mainMenuManager.UnlockedLevel;
//		print ("percentage :" + clearedMissions / totalLevelOfMission);
//		MissionBars [currentMission].fillAmount = (clearedMissions / totalLevelOfMission);
//		MissionBars [currentMission].GetComponentInChildren<Text> ().text = (MissionBars [currentMission].fillAmount * 100).ToString () + " %";
	}

}
