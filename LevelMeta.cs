using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TypeOfLevel
{
	none,
	PlantBombAndOpenPath,
	ShootASniper,
	KillAllEnemies,
	TaskOneByOne,
	levelWithConditions
}

public enum GunType
{
	none,
	knife,
	M1911,
	ShotGun,
	MP5,
	AK47,
	M4,
	Sniper,
}
public enum InitialGun
{
	none,
	knife,
	M1911,
	ShotGun,
	MP5,
	AK47,
	M4,
	Sniper,
}
public enum Task
{
	none,
	GetACar,
	GiveChalanToWronglyParkedCars,
	PickAPistol,
	PickShotGun,
	PickMp5,
	PickAk47,
	PickSniper,
	PickRocketLauncher,
	PickGrenade,
}
	[System.Serializable]
	public class PlantBombAndFindPathData
	{
		public int totalBombCount = 2 , totalEnemies = 5;
	}
	[System.Serializable]
	public class ShootASniperData
	{
		public GameObject mainTarget;
		public bool isMainTaskAchieve;
	}
	[System.Serializable]
	public class KillAllEnemiesData
	{
		public int totalEnemies = 13 , bombCount = 6;
	}
	[System.Serializable]
	public class TaskOneByOne
	{
		public string taskNumber = "TaskNumber";
		public GameObject objectToActive;
		public Task task;
		public bool isIncremental;
		public int amount = 0;
		public GameObject objectsToDeActivate;
	}
[System.Serializable]
public class LevelWithConditions
{
	public int totalBombCount = 2 , totalEnemies = 5 , headShotCount = 0 , specificGunKillCount = 0, grenadeKillEnemyCount = 0;
	public bool isGiveInitialSpecialGun , isplantBomb , isKillEnemies , isKillWithHeadShot , isKillWithSpecificGun , isKillWithGrenade;
	public string specificGunName = "";
}

public class LevelMeta : MonoBehaviour {
	public TypeOfLevel levelType;
	public GunType gunType;
	public InitialGun initialGun;
	public PlantBombAndFindPathData plantBombAndFindData;
	public ShootASniperData shootASniper;
	public KillAllEnemiesData killAllTheEnemies;
	public List<TaskOneByOne> taskOneByOne;
	public LevelWithConditions levelConditions;
	public bool  showInitialGunPanel , showBombProperties, showEnemiesProperties , showHeadShotProperties , showSpecificGunProperties , showGrenadeProperties;
	public int taskOneByOneListSize = 0;

	void PrintLevelData(){
		print ("Is Initial Gun" + levelConditions.isGiveInitialSpecialGun + " Gun is : " + initialGun + "isPlantBomb : " + levelConditions.isplantBomb + "Level data is : " + levelConditions.totalBombCount + " : " + 
			levelConditions.totalEnemies + " :headShot Kill : " + levelConditions.isKillWithHeadShot + " HeadShot Count : " +
			levelConditions.headShotCount + " : SpecificGun : " + levelConditions.isKillWithSpecificGun + " Gun Name : " + gunType + 
			" Gun Kill Count "+ levelConditions.specificGunKillCount +
			" With Grenade : " + levelConditions.isKillWithGrenade + "Granade Count : " + levelConditions.grenadeKillEnemyCount);
	}

	public TaskOneByOne GetTaskData(int taskId){
		return taskOneByOne [taskId - 1];
	}
}
