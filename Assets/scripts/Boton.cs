using UnityEngine;
using System.Collections;

public class Boton : MonoBehaviour {

	public GameObject accion;		//GameObject que debe reaccionar al botón
	public GameObject tipocaja;		//Qué tipo de caja activa el botón (madera o metal)
	public bool cajaencima;			//Variable Control para ver desde Unity si ha detectado colisión con una caja
	public bool trampilla;			//Variable Control para ver desde Unity si el objeto a activar se detecta como tipo "trampilla".
	public bool glifo;				//Variable Control para ver desde Unity si el objeto a activar se detecta como tipo "glifo".
	public bool porton;				//Variable Control para ver desde Unity si el objeto a activar se detecta como tipo "porton".
	public bool portal;				//Variable Control para ver desde Unity si el objeto a activar se detecta como tipo "portal".


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D info){								 // Cuando algo entre en el trigger del boton (controlar que afecta o no mediante los layers de unity)
		cajaencima = true;													 // Pone a True la variable de control de si detecta caja encima
		if (accion.tag == "Trampilla") {									 // Si el objeto a activar es tipo trampilla
			trampilla = true;												 // Cambiar la variable de control a True
			accion.GetComponentInChildren<Trampilla> ().activado = true;	 // Buscar el componente "activado" dentro de la trampilla y ponerlo a True para que active sus cosas. (Children porque el componente esta en un children de la trampilla).
		} else if (accion.tag == "Glifo") {									 // Si el objeto a activar es de tipo glifo
			glifo = true;													 // Cambiar la variable de control a True
			accion.GetComponent<Glifo> ().activado = true;					 // Buscar el componente "activado" dentro del glifo y ponerlo a True para que active sus cosas.
		} else if (accion.tag == "PuertaPortal") {
			porton = true;													 // Cambiar la variable de control a True
			try {
				accion.GetComponent<Portones> ().activado = true;			 // Buscar el componente "activado" dentro del porton y ponerlo a True para que active sus cosas.
				accion.GetComponent<Portales> ().activado = true;			 // Buscar el componente "activado" dentro del portal y ponerlo a True para que active sus cosas.
			} catch {
				Debug.Log ("No se ha encontrado el componente en el objetivo");	// Como este tag puede apuntar tanto a portales como a portones, siempre tirara una excepción de uno de los dos, con esto la evitamos.
			}
		}
		SM.instance.mecanismo ();											 // Llama al sound manager para que reproduzca el sonido de mecanismo
	}

	void OnTriggerExit2D (Collider2D info) {								 // Cuando el objeto que activa el botón salga del trigger
				cajaencima = false;											 // Pone a False la variable de control para detectar caja encima
				if (accion.tag == "Trampilla") {							 // Pone a false el resto y desactiva los respectivos "activado" de cada elemento.
					trampilla = false;
					accion.GetComponentInChildren<Trampilla> ().activado = false;
				} else if (accion.tag == "Glifo") {
					glifo = false;
					accion.GetComponent<Glifo> ().activado = false;
				} else if (accion.tag == "PuertaPortal") {
					porton = false;
					accion.GetComponent<Portones> ().activado = false;
				}
		}
	}