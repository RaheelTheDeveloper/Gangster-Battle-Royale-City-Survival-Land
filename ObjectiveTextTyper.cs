using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectiveTextTyper : MonoBehaviour
{
	private Text textComp;
	private string whatText;
	private AudioSource source;
	public AudioClip clip;
	public float letterSpeed = 0.05f;

	void Start ()
	{
		textComp = GetComponent<Text> ();
		if (GetComponent<AudioSource> () != null) {
			source = GetComponent<AudioSource> ();
		} else {
			gameObject.AddComponent<AudioSource> ();
			source = GetComponent<AudioSource> ();
		}
		whatText = textComp.text;
		StartCoroutine (TypeText ());
	}

	IEnumerator TypeText ()
	{
		textComp.text = "";
		foreach (char letter in whatText.ToCharArray()) {
			if (!source.isPlaying) {
				source.PlayOneShot (clip);
			}
			textComp.text += letter;
			yield return new WaitForSeconds (letterSpeed);
		}
	}
}
