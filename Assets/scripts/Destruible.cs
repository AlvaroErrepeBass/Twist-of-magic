using UnityEngine;
using System.Collections;

public class Destruible : MonoBehaviour {

	// Metodo que agregado a un GameObject permite definir la posibilidad de destruirlo al entrar en contacto con cualquiera de los tags más abajo definidos.

	public string tagActivador;			//Tag de los objetos que queremos que puedan destruir al GO que contenga este componente.
	public string tagActivador2;
	public string tagActivador3;
	//	public GameObject nuevoSprite; // (OPCIONAL) si se quiere poner otro objeto que sustituya al anterior destruido.

	void OnCollisionEnter2D(Collision2D info){


		if (info.collider.tag == tagActivador || info.collider.tag == tagActivador2 || info.collider.tag == tagActivador3) {		//Si el GameObject con el que hemos chocado es igual al elegido en "activador"....
																																	//... destruimos el GameObject con este componente.
			//GameObject nuevo = GameObject.Instantiate (nuevoSprite);																// (OPCIONAL) Para cambiar el sprite que hemos destruido por otro. Pero esto requeriria que este objeto no sea un prefab
			//nuevo.transform.position = new Vector2 (transform.position.x, transform.position.y);									// (OPCIONAL) Coloca el nuevo sprite en el sitio que ocupaba el que hemos destruido.
			Destroy (gameObject);																									// Destruye el gameobject que contenga este componente.
			if(gameObject.GetComponent<Renderer>().isVisible){
				SM.instance.destruye (gameObject.tag);																					// Llama al SM para que reproduzca el sonido de destruir.
			}
		}


	}
	void OnTriggerEnter2D(Collider2D info){


		if (info.tag == tagActivador || info.tag == tagActivador2 || info.tag == tagActivador3) {		//Si el GameObject con el que hemos chocado es igual al elegido en "activador"....
			//... destruimos el GameObject con este componente.
			//GameObject nuevo = GameObject.Instantiate (nuevoSprite);																// (OPCIONAL) Para cambiar el sprite que hemos destruido por otro. Pero esto requeriria que este objeto no sea un prefab
			//nuevo.transform.position = new Vector2 (transform.position.x, transform.position.y);									// (OPCIONAL) Coloca el nuevo sprite en el sitio que ocupaba el que hemos destruido.
			Destroy (gameObject);																									// Destruye el gameobject que contenga este componente.
			if(gameObject.GetComponent<Renderer>().isVisible){
				SM.instance.destruye (gameObject.tag);																					// Llama al SM para que reproduzca el sonido de destruir.
			}
		}
	}
}
