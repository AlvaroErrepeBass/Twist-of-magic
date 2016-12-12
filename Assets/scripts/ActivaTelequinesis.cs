using UnityEngine;
using System.Collections;
// version Alvaro

public class ActivaTelequinesis : MonoBehaviour {
	Vector2 direction;
//	Movible movible;

	GameObject caja;			//GameObject objetivo de la telequinesis
	int pop=1;                  //variable que detecta si la telequinesis está activa pop = 1 desactivada, pop = 2 activada
	public float distancia;		//Distancia a la que se puede activar la telequinesis
	public LayerMask layer;		//

	
	void Update () {

		if ((Input.GetKeyDown(KeyCode.C) && pop== 2) || (GM.instance.herido == true && GM.instance.recuperado >=1f && pop ==2) || 
			(GM.instance.mana <= 0 && pop ==2))
		{
			//GM.instance.TelequinesisDesactivada ();
			try {
			caja.GetComponent<Telequinesis>().enabled = false;
			caja.GetComponent<AudioSource>().Stop();
			caja.transform.GetChild(0).gameObject.SetActive(false);
			caja.transform.GetChild(1).gameObject.GetComponent<BoxCollider2D>().enabled = true;
			if (!GM.instance.cajaHundida)
			caja.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range(-0.5f,1f), 5f);
			} catch {
				Debug.Log ("No se detecta la caja especifica");
			}
			// convendría descativar la camara principal por colisiones con el el GUI y el audio Listener

			pop =1;

			GM.instance.TelequinesisDesactivada();
			GetComponent<Movimiento>().enabled = true;
			SM.instance.telequinesis();

		}
		else if (Input.GetKeyDown(KeyCode.C)&& GM.instance.mana > 0 && pop == 1)
		{
			
			if (GM.instance.mirDerecha)
				direction = new Vector2 (1f, 0f);
			else
				direction = new Vector2 (-1f, 0f);
			RaycastHit2D hit;

			//if (Physics2D.Raycast (transform.position, direction, distancia, layer)) {
			hit = Physics2D.Raycast (transform.position, direction, distancia, layer);
			try{
				caja = hit.collider.gameObject;
				if (!caja.GetComponent<Movible> ().mover (1f)) {
					//GM.instance.TelequinesisActiva ();
					caja.GetComponent<Telequinesis> ().enabled = true;
					GetComponent<Movimiento> ().enabled = false;
					caja.transform.GetChild (0).gameObject.SetActive (true);
					caja.transform.GetChild(1).gameObject.GetComponent<BoxCollider2D>().enabled = false;
					caja.GetComponent<AudioSource>().Play();
					pop = 2;
					GM.instance.cajaHundida = false;
					GM.instance.TelequinesisActiva();
					SM.instance.telequinesis();
				}
			} catch {
				Debug.Log ("No detectada caja para telequinesis");
			}

		}
	}
}

