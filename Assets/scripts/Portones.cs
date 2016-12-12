using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Portones : MonoBehaviour {

	public bool activado;
	public int destino;					// 1 = Biblioteca, 2 = Tutorial, 3 = nivel1, 4 = nivel2, 5 = nivel3, 6=nivel4, 7=flashback
	public Animator anim;
	public string estado;
	public bool sobrePuerta;
	public bool pulsado;

	// Use this for initialization
	void Start () {
		if (estado == "Abierto") anim.SetBool ("Abierto", true);
		if (estado == "Cerrado") anim.SetBool ("Cerrado", true);
		if (estado == "Sellado") anim.SetBool ("Sellado", true);
		}

	void Update (){
		if (activado) {
			gameObject.GetComponent<Animator> ().SetBool ("Sellado", false);
			anim.SetBool ("Cerrado", true);
			estado = "Cerrado";
		}
	}

	void OnTriggerEnter2D()
	{
		SM.instance.puerta (estado);	
	}

	void OnTriggerStay2D () {
		sobrePuerta = true;
		if (estado == "Cerrado") 
			{
				gameObject.GetComponent<Animator> ().SetBool ("Abierto", true);	
				gameObject.GetComponent<Animator> ().SetBool ("Cerrado", false);
				gameObject.GetComponent<Animator> ().SetBool ("Sellado", false);
				estado = "Abierto";
			}
		if (estado == "Abierto") 
			{
			if (Input.GetKeyDown (KeyCode.V) && destino !=-1) {
				SceneManager.LoadScene (destino);
				}
			}
	}

	void OnTriggerExit2D (Collider2D info) {
		if (estado == "Abierto" || estado == "Cerrado") {
			if (info.name == "Jugador") {
				sobrePuerta = false;
				gameObject.GetComponent<Animator> ().SetBool ("Cerrado", true);
				gameObject.GetComponent<Animator> ().SetBool ("Abierto", false);
				SM.instance.puerta (estado);
				estado = "Cerrado";
			}
		}
	}
}