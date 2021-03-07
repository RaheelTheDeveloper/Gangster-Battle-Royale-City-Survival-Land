using UnityEngine;
using System.Collections;

[System.Serializable]
public class Tweens
{
    public Vector3 initialVector;
    public Vector3 finalVector;
    public float speed = 0.1f;
    public float startingWait;
    public TweenType tweenType;
    public LeanTweenType type;
}
public enum TweenType
{
    position, rotation, scale
}
public class TweenerUI : MonoBehaviour
{
    public Tweens[] tweens;
    Tweens currentPositionTween;
    Tweens currentRotationTween;
    Tweens currentScalingTween;
    void OnEnable()
    {
        for (int i = 0; i < tweens.Length; i++)
        {

            if (tweens[i].tweenType == TweenType.position)
            {
                currentPositionTween = tweens[i];
                GetComponent<RectTransform>().anchoredPosition = tweens[i].initialVector;
                Invoke("animatePosition", tweens[i].startingWait);

            }
            if (tweens[i].tweenType == TweenType.rotation)
            {
                currentRotationTween = tweens[i];
                GetComponent<RectTransform>().localEulerAngles = tweens[i].initialVector;
                Invoke("animateRotation", tweens[i].startingWait);
            }
            if (tweens[i].tweenType == TweenType.scale)
            {
                currentScalingTween = tweens[i];
                GetComponent<RectTransform>().localScale = tweens[i].initialVector;
                Invoke("animateScaling", tweens[i].startingWait);
            }
        }
    }

    void animatePosition()
    {
        if (currentPositionTween.type == LeanTweenType.pingPong)
            LeanTween.move(GetComponent<RectTransform>(), currentPositionTween.finalVector, currentPositionTween.speed).setLoopPingPong(100).setEase(currentPositionTween.type);
        else
            LeanTween.move(GetComponent<RectTransform>(), currentPositionTween.finalVector, currentPositionTween.speed).setEase(currentPositionTween.type);
    }
    void animateRotation()
    {
        LeanTween.rotateX(gameObject, (currentRotationTween.finalVector.x - currentRotationTween.initialVector.x), currentRotationTween.speed).setLoopType(currentRotationTween.type);
        LeanTween.rotateY(gameObject, (currentRotationTween.finalVector.y - currentRotationTween.initialVector.y), currentRotationTween.speed).setLoopType(currentRotationTween.type);
        LeanTween.rotateZ(gameObject, (currentRotationTween.finalVector.z - currentRotationTween.initialVector.z), currentRotationTween.speed).setLoopType(currentRotationTween.type);
    }
    void animateScaling()
    {
        if (currentScalingTween.type == LeanTweenType.pingPong)
            LeanTween.scale(GetComponent<RectTransform>(), currentScalingTween.finalVector, currentScalingTween.speed).setLoopPingPong(100).setEase(currentScalingTween.type);
        else
            LeanTween.scale(GetComponent<RectTransform>(), currentScalingTween.finalVector, currentScalingTween.speed).setEase(currentScalingTween.type);
    }
}
