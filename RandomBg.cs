using UnityEngine;
using UnityEngine.UI;
public class RandomBg : MonoBehaviour
{
    public Sprite[] loadingBG;
    // Use this for initialization
    void OnEnable()
    {
        GetComponent<Image>().overrideSprite = loadingBG[Random.Range(0, loadingBG.Length)];
    }

}
