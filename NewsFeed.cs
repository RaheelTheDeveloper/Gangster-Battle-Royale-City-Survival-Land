using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewsFeed : MonoBehaviour
{
    bool startfading = false;
    public Image[] newsImages;
    public Text[] newsText;
    int currentIndex = 0;
    int nextIndex = 1;
    // Use this for initialization
    void OnEnable()
    {
        StartCoroutine(FadeInOut());
    }

    // Update is called once per frame
    IEnumerator FadeInOut()
    {
      
        yield return new WaitForSeconds(5);
        startfading = true;
        for (int i = 0; i < newsText.Length; i++)
        {
            if (currentIndex + 1 == newsText.Length)
            {
                newsText[0].gameObject.SetActive(true);
            }
            else if (currentIndex + 1 == i)
            {
                newsText[i].gameObject.SetActive(true);
            }
            else
            {
                newsText[i].gameObject.SetActive(false);
            }
        }
        yield return new WaitForSeconds(5);
        startfading = false;
        if (currentIndex < newsImages.Length - 1)
        {
            currentIndex += 1;
            if (currentIndex == newsImages.Length - 1)
                nextIndex = 0;
            else
            {
                nextIndex = currentIndex + 1;
            }
        }
        else
        {
            currentIndex = 0;
            nextIndex = currentIndex + 1;
        }



        StopCoroutine(FadeInOut());
        StartCoroutine(FadeInOut());
    }
    void Update()
    {
        if (startfading)
        {
            newsImages[currentIndex].CrossFadeAlpha(0, 1, true);
            newsImages[nextIndex].CrossFadeAlpha(1, 1, true);
        }
    }
}
