// Código por Antonio Cardona Costa.

using UnityEngine;
using System.Collections;

public class MovimientoFijo : MonoBehaviour {

	public float ejeX;
	public float ejeY;
	public int gradXsec;
	public float incrementoX = 0.1f;
	public float incrementoY = 0.1f;
	private float topeDer, topeIzq, topeArb, topeAbj;
	private bool derecha;

	// Use this for initialization
	void Start () {
		//Ajustamos los topes de movimiento del enemigo desde la posición donde se crea
		topeDer = transform.position.x + ejeX;
		topeIzq = transform.position.x - ejeX;
		topeArb = transform.position.y + ejeY;
		topeAbj = transform.position.y - ejeY;
	}

	// Update is called once per frame
	void Update () {

		//Bloque de código que cambia de sentido cuando llega al tope definido.
		if (transform.position.x + incrementoX >= topeDer)
			incrementoX = -incrementoX;
		if (transform.position.x + incrementoX <= topeIzq)
			incrementoX = -incrementoX;
		if (transform.position.y + incrementoY >= topeArb)
			incrementoY = -incrementoY;
		if (transform.position.y + incrementoY <= topeAbj)
			incrementoY = -incrementoY;

		//Aqui se define hacia donde mira el enemigo dependiendo de la dirección que lleva.
		if (incrementoX < 0) {
			GetComponent<SpriteRenderer> ().flipX = false;
		} else if (incrementoX > 0) {
			GetComponent<SpriteRenderer> ().flipX = true;
		}

		//Línea de código que modifica la posición del bichete y su rotación
		transform.Translate (incrementoX * Time.deltaTime, incrementoY * Time.deltaTime, 0);
		gameObject.transform.Rotate (0f, 0f, gradXsec*Time.deltaTime);
	}

}
