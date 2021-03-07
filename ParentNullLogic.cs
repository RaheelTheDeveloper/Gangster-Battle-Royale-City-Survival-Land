using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentNullLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.transform.SetParent (null);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
