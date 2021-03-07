using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HUDTextEffectController : MonoBehaviour {


	// Update is called once per frame
	private Text text;
	private Color col;
	private AudioSource sound;
	void Awake() {
		text = GetComponent<Text> ();
		sound = GetComponent<AudioSource> ();
	}
	void OnEnable() {
		if (sound != null)
			sound.Play ();
		StopCoroutine (moveToTop ());
		col = text.color;
		col.a = 1;
		text.color = col;
		transform.localPosition = new Vector3 (0,50,0);
		StartCoroutine (moveToTop());
	}

	IEnumerator moveToTop() {
		float alpha = 1;
		while (transform.localPosition.y < 140) {
			transform.localPosition = Vector3.Lerp (transform.localPosition,new Vector3(0,150,0),1.6f*Time.deltaTime);
			alpha = Mathf.MoveTowards (alpha, 0, 1.3f * Time.deltaTime);
			col.a = alpha;
			text.color = col;
			yield return null;
		}
		gameObject.SetActive (false);
	}
}
