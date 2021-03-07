using UnityEngine;

public class PolicyPopUp : MonoBehaviour
{
	public GameObject popUpPrefab;
	public GameObject popUpParent;
	public static int PopUpEnablingCounter = 0;
	[Tooltip ("when to show next give int value e.g 1,2,3 etc")]
	public int DelayNextToShow = 3;

	void OnEnable ()
	{
		if (PlayerPrefs.GetFloat ("PolicyAgreed") == 0) {
			if (PopUpEnablingCounter == 0) {
				//show policy popup
				GameObject pop_obj = Instantiate (popUpPrefab) as GameObject;
				pop_obj.GetComponent<DialogBox> ().SetPopUpDetails ("Privacy Policy Guide", "Privacy Policy Guide", "Yes", "No", "Do you want to subscribe updates ?", null, null);

				if (popUpParent == null) {
					popUpParent = GameObject.FindObjectOfType<Canvas> ().gameObject;
				}
				pop_obj.GetComponent<RectTransform> ().SetParent (popUpParent.transform, false);

				PopUpEnablingCounter++;
			} else {
				PopUpEnablingCounter++;
				if (PopUpEnablingCounter > DelayNextToShow) {
					PopUpEnablingCounter = 0;
				}
			}

		}
	}

}
