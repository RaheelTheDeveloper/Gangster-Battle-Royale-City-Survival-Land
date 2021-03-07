using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LevelBtnAnimationController : MonoBehaviour {

	void OnEnable() {
		StartCoroutine (decreaseOpacity ());
	}

	IEnumerator increasEOpacity() {
		Color col = GetComponent<Image> ().color;
		while (col.a != 1) {
			col.a = Mathf.MoveTowards (col.a, 1, 1.5f * Time.deltaTime);
			GetComponent<Image> ().color = col;
			yield return null;
		}
		StartCoroutine (decreaseOpacity ());
	}

	IEnumerator decreaseOpacity() {
		Color col = GetComponent<Image> ().color;
		while (col.a != 0.3f) {
			col.a = Mathf.MoveTowards (col.a, 0.3f, 1.5f * Time.deltaTime);
			GetComponent<Image> ().color = col;
			yield return null;
		}
		StartCoroutine (increasEOpacity ());
	}

	void OnDisable() {
		StopAllCoroutines ();
	}


}
