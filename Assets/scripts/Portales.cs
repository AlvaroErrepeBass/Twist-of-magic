using UnityEngine;
using System.Collections;
/// <summary>
/// Transporta el objeto asignado hasta la posición asignada
/// </summary>
public class Portales : MonoBehaviour {

    public GameObject destino;
    public GameObject jugador;
	public bool sobrePortal;
	public bool activado;

	
    void OnTriggerStay2D(Collider2D info)           // Si está delante del portal y pulsa  v se cambia la posicion del playeer por la del destino.
    {
		if (activado) {
			if (info.tag == "Player") {
				sobrePortal = true;
				GM.instance.respawn = transform.position;
				if (Input.GetKeyDown (KeyCode.V)) {
					SM.instance.portal ();
					Invoke ("atraviesaPortal", 1f);
					jugador.GetComponent<Rigidbody2D> ().gravityScale = 0;
					jugador.GetComponent<Movimiento> ().enabled = false;
					jugador.GetComponent<Rigidbody2D> ().velocity = new Vector2(0f,0f);


				}
			}
		}
    }

	void OnTriggerExit2D(Collider2D info)
	{
		if (info.tag == "Player") {
			sobrePortal = false;
		}
	}

	void atraviesaPortal()
	{
		jugador.transform.position = destino.transform.position;
		Invoke ("transporta", 2f);
	}

	void transporta()
	{
		jugador.GetComponent<Rigidbody2D> ().gravityScale = 2;
		jugador.GetComponent<Movimiento> ().enabled = true;
	}
}
