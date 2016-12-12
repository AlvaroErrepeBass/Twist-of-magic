using UnityEngine;
using System.Collections;
/// <summary>
/// Trigger que engloba las zonas de agua ralentizando al juador, bajando la fuerza del saltoy restando maná.
/// </summary>
public class ZonaAgua : MonoBehaviour {

	public GameObject jugador;
	float veljug;
	float velVjug;
	float podSalto;

	//===============================
	void Start () {                                                         //Guardamos la velocidad del jugador y la fuerza del salto.
		veljug = jugador.GetComponent<Movimiento> ().speed;
		velVjug = jugador.GetComponent<Movimiento> ().vspeed;
		podSalto = jugador.GetComponent<Movimiento> ().jumpPower;
	}
    //EN EL AGUA
	void OnTriggerStay2D (Collider2D info) {
		if (info.tag == jugador.tag) {                                  //Si lo que choca es el jugador...
			GM.instance.enAgua = true;                                  //Cambiamos la velocidad  y el poder de salto.
			Movimiento.instance.speed = 2f;
			Movimiento.instance.vspeed = 2f;
			Movimiento.instance.jumpPower = 7f;
		}
	}
   
    //AL SALIR DEL AGUA
        void OnTriggerExit2D (Collider2D info) {
		if (info.tag == jugador.tag) {                                  //Si sale el jugador del agua se restablecen los valores iniciales de velocidad y salto
			GM.instance.enAgua = false;
			Movimiento.instance.speed = veljug;
			Movimiento.instance.vspeed = velVjug;
			Movimiento.instance.jumpPower = podSalto;
		}
	}
}
