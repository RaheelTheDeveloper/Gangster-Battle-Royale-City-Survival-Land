using UnityEngine;
using System.Collections;

public class UIElementAnimationController : MonoBehaviour
{
    [Header("Position")]
    public Vector3 initialPosition;
    public Vector3 finalPosition;



    public float speed = 0.1f;
    public float positionStartWait;
    public LeanTweenType type = LeanTweenType.easeOutBack;
    public bool isRotate;
    //	public Vector3 rotateVector;
    public float rotateSpeed = 1;
    public bool isMoveCustom;
    [Header("Scaling")]
    public bool isAnimateScale = false;
    public Vector3 initialScale;
    public Vector3 finalScale;
    public float scalingStartWait;
    void OnEnable()
    {
        GetComponent<RectTransform>().anchoredPosition = initialPosition;
        if (isAnimateScale)
            GetComponent<RectTransform>().localScale = initialScale;
        if (!isMoveCustom)
            Invoke("animatePosition", positionStartWait);
        if (isAnimateScale)
        {
            Invoke("animateScaling", scalingStartWait);
        }
    }

    void animatePosition()
    {
        if (isRotate)
        {
            LeanTween.rotateAround(GetComponent<RectTransform>(), Vector3.forward, -1080f, 12f);
        }
        else
        {
            if (type == LeanTweenType.pingPong)
                LeanTween.move(GetComponent<RectTransform>(), finalPosition, speed).setLoopPingPong(100).setEase(type);
            else
                LeanTween.move(GetComponent<RectTransform>(), finalPosition, speed).setEase(type);
        }

    }
    void animateScaling()
    {
        if (type == LeanTweenType.pingPong)
            LeanTween.scale(GetComponent<RectTransform>(), finalScale, speed).setLoopPingPong(100).setEase(type);
        else
            LeanTween.scale(GetComponent<RectTransform>(), finalScale, speed).setEase(type);

    }


    public void MoveIntoScreen()
    {
        animatePosition();
    }
    public void MoveOutFromScreen()
    {
        LeanTween.move(GetComponent<RectTransform>(), initialPosition, speed).setEase(type);
    }

}
