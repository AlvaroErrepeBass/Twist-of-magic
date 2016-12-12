using UnityEngine;
using System.Collections;

public class FinMuerteCaja : MonoBehaviour {

	// Use this for initialization
	void OnAwake () {
		Invoke ("MuerteCaja", 3);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void MuerteCaja()
	{
		gameObject.SetActive (false);
	}
}
