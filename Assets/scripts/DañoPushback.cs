using UnityEngine;
using System.Collections;

public class DañoPushback : MonoBehaviour {
	// Behaviour creado para se aplicado a todo NPC que haga daño y/o empuje a un jugador
	public int damage;		// Cantidad de mana que hace perder al jugador.
	private int dirX = 1; 	// Dirección en la que será empujado (eje x)
	private int dirY = 1;	// Dirección en la que será empujado (eje y)

	void OnStart (){
	}

	void Update (){
	}

	void OnCollisionEnter2D (Collision2D col)					// Cuando algo entre en colisión con el objeto...
	{
		if (col.collider.tag == ("Player")) 					// Si lo que ha colisionado es el jugador...
		{
			if (col.collider.transform.position.x > transform.position.x)	// ... si este viene desde la derecha ...
			{
				dirX = 1;													// ... empujar hacia la derecha (mantener dirección empuje en eje x)
			} else 	{														// ... o si viene desde la izquierda ...
				dirX = -1;													// ... empujar hacia la izquierda (invertir dirección empuje en eje x)
			}

			if (col.collider.transform.position.y >= transform.position.y -0.01f) // ... si viene desde arriba ...
			{
				dirY = 1;														  // ... empujar hacia arriba (mantener dirección empuje en eje y)
			} else {															  // ... si viene desde abajo ...
				dirY = -1;														  // ... empujar hacia abajo (invertir dirección de empuje en eje y)
			}

			GM.instance.herido = true;										// Se activa la variable de control herido en el GM.
			GM.instance.restarMana (damage);								// Se resta tanto mana al jugador como el definido arriba.
			GM.instance.Herido (dirX,dirY);									// Activa la funcion herido que empujara al jugador en la dirección enviada por dirX, dirY.
			col.collider.GetComponent<Movimiento>().enabled = false;		// Desactivar el control de movimiento del jugador (se reactiva en el GM).
			SM.instance.ataque(gameObject.tag);								// Llama al SM para que reproduzca el sonido de ataque.
			//Debug.Log ("Golpeado por " + gameObject.tag);					// Debug opcional para saber que ha golpeado al jugador.
		}
	}
}

