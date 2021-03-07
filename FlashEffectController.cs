using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlashEffectController : MonoBehaviour {
	void OnEnable() {
		StopAllCoroutines ();
		StartCoroutine (fadeIn ());
		StartCoroutine (fadeOut ());
	}

	IEnumerator fadeIn() {
		Color col = GetComponent<Image> ().color;
		while (col.a != 1) {
			col.a = Mathf.MoveTowards (col.a, 1, 100 * Time.deltaTime);
			GetComponent<Image> ().color = col;
			yield return null;
		}
	}
	IEnumerator fadeOut() {
		yield return new WaitForSeconds (0.7f);
		Color col = GetComponent<Image> ().color;
		while (col.a != 0) {
			col.a = Mathf.MoveTowards (col.a, 0, 2 * Time.deltaTime);
			GetComponent<Image> ().color = col;
			yield return null;
		}
		transform.parent.gameObject.SetActive (false);
	}
}
