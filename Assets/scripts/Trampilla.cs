using UnityEngine;
using System.Collections;
/// <summary>
/// Activa la trampilla invirtiendo la posición
/// </summary>
public class Trampilla : MonoBehaviour {

	//public static Trampilla instance;
	public bool activado;


	// Use this for initialization
	void Start () {
		//instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (activado)
			gameObject.transform.localPosition = new Vector3 (0, -1, 0);
		else if (!activado)
			gameObject.transform.localPosition = new Vector3 (0, 1, 0);
	}
}
