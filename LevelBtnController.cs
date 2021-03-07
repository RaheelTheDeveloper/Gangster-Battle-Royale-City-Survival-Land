using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelBtnController : MonoBehaviour
{
    public int levelNumber;
    public GameObject selectedCircle;
    public GameObject lockImage;
    public GameObject completed;
    void OnEnable()
    {

        lockImage.SetActive(false);
        if (PlayerPrefs.GetInt("LevelsCleared", 0) + 1 < levelNumber)
        {
            gameObject.GetComponent<Button>().interactable = false;
            return;
        }

        if (PlayerPrefs.GetInt("LevelsCleared", 0) == levelNumber - 1)
        {
            selectedCircle.SetActive(true);
            Globals.currentLevelNumber = levelNumber;
        }
        else
        {
            selectedCircle.SetActive(false);
        }

        if (PlayerPrefs.GetInt("LevelsCleared", 0) + 1 > levelNumber)
        {
            completed.SetActive(true);
        }
        else
        {
            completed.SetActive(false);
        }
    }
}
