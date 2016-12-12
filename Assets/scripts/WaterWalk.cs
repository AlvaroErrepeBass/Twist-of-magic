using UnityEngine;
using System.Collections;
/// <summary>
/// Script de andar sobre el agua.
/// Al pulsar X activa el collider de los objetos superficie
/// </summary>
public class WaterWalk : MonoBehaviour {
	public GameObject jugador;
	float veljug;
	float velVjug;
	private bool activo;
	// Use this for initialization
	void Start () {                                                         //Guardamos la velocidad del jugador y la fuerza del salto.
		veljug = 4;
		velVjug = 4;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.X) && GM.instance.enAire == false && GM.instance.mana > 0 && activo) 
		{
			gameObject.GetComponent<BoxCollider2D> ().isTrigger = false;
			GM.instance.WaterWalkActivo ();
			GetComponent<AudioSource> ().Play ();
			activo = false;
			SM.instance.andarAgua ();
		}

		if (Input.GetKeyUp (KeyCode.X)) 
		{
			gameObject.GetComponent<BoxCollider2D> ().isTrigger = true;
			GM.instance.WaterWalkDesactivado ();
			GetComponent<AudioSource> ().Stop ();
		}

	}
	void OnTriggerEnter2D(Collider2D col)
	{
		activo = true;
		if (col.tag == jugador.tag) {                                  //Si lo que choca es el jugador...
			Movimiento.instance.speed = 2f;
			Movimiento.instance.vspeed = 2f;
		}
	}


	void OnTriggerExit2D(Collider2D col)
	{
		activo = false;
		if (col.tag == jugador.tag) {                                  //Si sale el jugador del agua se restablecen los valores iniciales de velocidad y salto
			Movimiento.instance.speed = veljug;
			Movimiento.instance.vspeed = velVjug;
		}


	}
}
