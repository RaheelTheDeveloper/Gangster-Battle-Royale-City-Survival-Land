using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Text))]
public class TypeWritingEffect : MonoBehaviour
{
	public float speed = 0.1f;
	public float startDelay = 0;
	private Text textComponent;
	private string textData;

	void Start ()
	{
		textComponent = GetComponent<Text> ();
		textData = textComponent.text;
		StartCoroutine (TypeWrite (textData));
	}


	// Update is called once per frame
	IEnumerator TypeWrite (string _textData)
	{
		if (startDelay > 0)
			yield return new WaitForSeconds (startDelay);
		textComponent.text = "";
       
		for (int i = 0; i < _textData.Length; i++) {
			textComponent.text += _textData [i];
			yield return new WaitForSeconds (speed);
		}
        
	}
}
