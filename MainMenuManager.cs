using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum GraphicsSettings
{
	Low = 0,
	Medium = 1,
	High = 2
}

[System.Serializable]
public class MainMenuDetails
{
	
	[Header ("--- Cash _ Coins _ XP _ Spins ---")]
	[Space (3)]
	public int totalCash = 0;
	public int totalCoins = 0;
	public int totalXP = 0;
	public int totalSpins = 0;

	[Header ("---Settings---")]
	[Space (3)]
	public GraphicsSettings graphicsSettings = GraphicsSettings.Medium;
	[Range (0, 1)]
	public float soundsVolume = 1;
	[Range (0, 1)]
	public float sensitivityValue = 1;

	[Range (0, 1)]
	public float musicVolume = 1;
	public bool isSoundsON = true;
	public bool isMusicON = true;

	public Sprite SoundOnIcon;
	public Sprite SoundOffIcon;


	[Header ("---Missions---")]
	[Space (3)]
	public int totalMissions = 1;
	public int totalLevel = 10;
	public int selectedLevel = 1;
	public int selectedMission = 1;
	public int unlockedLevels = 1;

	[Header ("---Menu---")]
	[Space (3)]
	public bool isCollectedStartCash = false;
}



[CreateAssetMenu (fileName = "MainMenuManager", menuName = "MainMenu Manager", order = 0)]
public class MainMenuManager : ScriptableObject
{
	public MainMenuDetails detail;


	#region Cash Coins Xp and Spin

	public int GetTotalCoinsCount ()
	{
		return detail.totalCoins;

	}

	public void addCoins (int quantity)
	{
		detail.totalCoins += quantity;
	}

	public void consumeCoins (int quantity)
	{

		if (detail.totalCoins <= 0) {
			detail.totalCoins = 0;
		} else
			detail.totalCoins -= quantity;

	}

	public int GetTotalCashCount ()
	{
		return detail.totalCash;
	}

	public void addCash (int quantity)
	{
		Debug.Log (quantity + " cash Added");
		detail.totalCash += quantity;
	}

	public void consumeCash (int quantity)
	{

		if (detail.totalCash <= 0) {
			detail.totalCash = 0;
		} else
			detail.totalCash -= quantity;

	}

	public int GetTotalXPCount ()
	{
		return detail.totalXP;
	}

	public void addXP (int quantity)
	{
		detail.totalXP += quantity;
	}

	public int GetTotalSpins ()
	{
		return detail.totalSpins;

	}

	public void addSpins (int quantity)
	{
		detail.totalSpins += quantity;

	}

	public void consumeSpin ()
	{

		if (detail.totalSpins <= 0) {
			detail.totalSpins = 0;
		}
		detail.totalSpins -= 1;
	}

	#endregion

	public GraphicsSettings GetGraphicsSettingsLevel ()
	{
		return detail.graphicsSettings;
	}

	public void SetGraphicsSettingsLevel (GraphicsSettings _graphicsSettings)
	{
		detail.graphicsSettings = _graphicsSettings;
	}


	#region Mission

	public int UnlockedLevel {
		get {
			return detail.unlockedLevels;
		}
		set {
			detail.unlockedLevels = value;
		}
	}

	public int SelectedLevel {
		get {
			return detail.selectedLevel;
		}
		set {
			detail.selectedLevel = value;
		}
	}

	public int SelectedMission {
		get {
			return detail.selectedMission;
		}
		set {
			detail.selectedMission = value;
		}
	}

	#endregion

	#region Senstivity

	public float SensitivityValue {
		get {
			return detail.sensitivityValue;
		}
		set {
			detail.sensitivityValue = value;
		}
	}

	#endregion

	#region Sound && Music

	public bool IsSoundOn {
		get {
			return detail.isSoundsON;
		}
		set {
			detail.isSoundsON = value;
		}
	}


	public bool IsMusicOn {
		get {
			return detail.isMusicON;
		}
		set {
			detail.isMusicON = value;
		}
	}

	public float SoundVolume {
		get {
			return detail.soundsVolume;
		}
		set {
			detail.soundsVolume = value;
		}
	}

	public float MusicVolume {
		get {
			return detail.musicVolume;
		}
		set {
			detail.musicVolume = value;
		}
	}

	public Sprite getSoundIcon (bool isOn)
	{
		if (isOn)
			return	detail.SoundOffIcon;
		else
			return	detail.SoundOnIcon;
	}

	#endregion



	#region Statistic




	[Header ("---Statistic----")]
	[Space ()]
	public List<Galassia.Statistics.StatsData> gameStats;

	/// <summary>
	/// Updates the statistics.
	/// </summary>
	/// <param name="_dataType">Data type.</param>
	/// <param name="addValue">Add value.</param>
	public void UpdateStatistics (Galassia.Statistics.Type _dataType, int addValue)
	{
		foreach (var item in gameStats) {
			if (item.type == _dataType) {
				item.AddValue (addValue);
				return;
			}
		}
	}


	#endregion


	#region Achievements




	[Header ("---Achievements----")]
	[Space ()]
	public List <Galassia.Achievements.ItemData> gameAchievements;

	/// <summary>
	/// Updates the achievements.
	/// </summary>
	/// <param name="_dataType">Data type.</param>
	/// <param name="addValue">Add value.</param>
	public void UpdateAchievements (Galassia.Achievements.Type _dataType, int addValue)
	{
		foreach (var item in gameAchievements) {
			if (!item.isCompleted && item.type == _dataType) {
				item.current += addValue;
				return;
			}
		}
	}


	#endregion
}
