using UnityEngine;
using System.Collections;

public class Glifo : MonoBehaviour {

	public int AntNecesarias = 10;
	public bool activado;
	public GameObject objetivo;
	public GameObject destruye = null;	//Si queremos que destruya algo al activarse.
	public GameObject destruye2 = null;	//Si queremos que destruya algo al activarse.
	public GameObject destruye3 = null;	//Si queremos que destruya algo al activarse.

	// Use this for initialization
	void Start () {
		activado = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (GM.instance.antorchasActivadas >= AntNecesarias) {
			activado = true;
		}

		if (activado) {
			if (destruye != null) Destroy(destruye);
			if (destruye2 != null) Destroy(destruye2);
			if (destruye3 != null) Destroy(destruye3);
			if (objetivo.tag == "Trampilla") {
				objetivo.GetComponentInChildren<Trampilla> ().activado = true;
			} else if (objetivo.tag == "PuertaPortal") {
				objetivo.GetComponent<Portones> ().activado = true;
			}
		}
	}

	/*void aumentaEncendidas()
		{
			encendidas++;
			if (++encendidas >= necesarias)
				activado = true;
		}*/
	}
