using UnityEngine;
using System.Collections;

public class ColumBolas : MonoBehaviour {

	public float gradXsec = 1;		// Variable publica que se puede modificar para variar la velocidad de rotación
	public float empujeX;			// Fuerza que debe aplicarse en el eje X al jugador si choca contra la columna (Flare), sustituible por dañoPushback.
	public float empujeY;			// Fuerza que debe aplicarse en el eje Y al jugador si choca contra la columna (Flare), sustituible por dañoPushback.
	public int danyo;				// Cantidad de mana que debe restarse al jugador si impacta contra la columna de bolas (Flare).

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate (0f, 0f, gradXsec*Time.deltaTime);			// Movimiento de la columna de bolas (sustituible por movimientofijo) hace girar el objeto a la velicidad definida arriba en gradXsec
	}

	void OnCollisionEnter2D (Collision2D col)												// Cuando se detecte la colision
	{		
		if (col.collider.tag == ("Player")) {												// Si el objeto que colisiona es el jugador...
			if (col.collider.transform.position.x > transform.position.x) {					// ... si el jugador esta a la derecha de la columna ...
				col.collider.GetComponent<Movimiento> ().enabled = false;					// ... bloquear el movimiento del jugador ...
				GM.instance.herido = true;													// ... activar el metodo herido en el GM ...
				GM.instance.mana = GM.instance.mana - danyo;								// ... restar el mana al jugador ...
				col.collider.attachedRigidbody.velocity = new Vector2 (empujeX, empujeY);	// ... empujar al jugador. (Metodo sustituible con el nuevo dañoPushback)
				//Debug.Log ("Golpeado" + empujeX + empujeY);
			}else{																			// si el jugador impacta por la izquierda...
				col.collider.GetComponent<Movimiento>().enabled = false;					// ... aplicamos el mismo procedimiento pero con el empuje en sentido contrario.
				GM.instance.herido = true;
				GM.instance.mana = GM.instance.mana - danyo;
				col.collider.attachedRigidbody.velocity  = new Vector2 (empujeX * -1, empujeY);
				//Debug.Log ("Golpeado" + empujeX + empujeY);
			}
		}
	}
}
