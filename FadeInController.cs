using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeInController : MonoBehaviour
{
    private Text text;
    private Image image;
    Color col;
    float targetAlpha;
    // Use this for initialization
    void OnEnable()
    {
        if (GetComponent<Image>() != null)
        {
            image = GetComponent<Image>();
            col = image.color;
            targetAlpha = 1;
            col.a = 0;
            image.color = col;
            StartCoroutine(FadeInImage());
        }
        else {
            text = GetComponent<Text>();
            col = text.color;
            targetAlpha = 1;
            col.a = 0;
            text.color = col;
            StartCoroutine(FadeInText());
        }
    }

    IEnumerator FadeInText()
    {
        while (col.a != targetAlpha)
        {
            col.a = Mathf.MoveTowards(col.a, targetAlpha, 0.9f * Time.deltaTime);
            text.color = col;
            yield return null;
        }
    }
    IEnumerator FadeInImage()
    {
        while (col.a != targetAlpha)
        {
            col.a = Mathf.MoveTowards(col.a, targetAlpha, 0.9f * Time.deltaTime);
            image.color = col;
            yield return null;
        }
    }

}
