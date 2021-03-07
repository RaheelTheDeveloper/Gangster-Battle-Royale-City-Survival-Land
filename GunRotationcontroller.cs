using UnityEngine;
using System.Collections;

public class GunRotationcontroller : MonoBehaviour
{
	[Range (1, 100)]
	[SerializeField] float speed = 60f;
	// Update is called once per frame
	void Update ()
	{
		transform.Rotate (0, speed * Time.deltaTime, 0);
	}
}
