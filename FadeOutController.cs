using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FadeOutController : MonoBehaviour {
	void OnEnable() {
		StartCoroutine (fadeOut ());
	}

	IEnumerator fadeOut() {
		Color col = Color.black;
		GetComponent<Image> ().color = col;
		while (col.a != 0) {
			col.a = Mathf.MoveTowards (col.a, 0, 0.5f * Time.deltaTime);
			GetComponent<Image> ().color = col;
			yield return null;
		}
//		gameObject.SetActive (false);
	}
}
