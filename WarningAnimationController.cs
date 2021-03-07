using UnityEngine;
using System.Collections;

public class WarningAnimationController : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
		InvokeRepeating ("animate", 0.8f, 0.8f);
	}


	void animate() {
		transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);
		LeanTween.scale (gameObject, Vector3.one, 0.2f).setEase (LeanTweenType.easeOutBack);
	}
	void OnDisable() {
		CancelInvoke ();
	}
	

}
