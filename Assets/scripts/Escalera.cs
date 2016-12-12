using UnityEngine;
using System.Collections;
/// <summary>
/// controla si el player está sobre la escalera
/// </summary>
public class Escalera : MonoBehaviour {

	private Movimiento jugador;	

	// Use this for initialization
	void Start () {
		jugador = FindObjectOfType<Movimiento>();
	}

	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D otro) {
		if (otro.name == jugador.name) {
			jugador.sobreEscalera = true;
		}
	}

	void OnTriggerExit2D (Collider2D otro) {
		if (otro.name == jugador.name) {
			jugador.sobreEscalera = false;
		}
	}
}
