using UnityEngine;
using System.Collections;
/// <summary>
/// Comportamiento del enemigo "Pollo"
/// </summary>
public class Vampiro : MonoBehaviour {

	public bool derecha;
	public int danyo;
	public float velocidad;
	Rigidbody2D rb;
	public float cambioDireccion;
	float contVuelta;
	public float empujeX;
	public float empujeY;
	public float destruye;
	public GameObject jugador;
	float acumulaDestruye;
	public bool desaparece;
	bool fueraCamara;

	void Start()
	{
		acumulaDestruye = 0;
		contVuelta = 0;
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{

        //SE GUARDA EL TIEMPO QUE ESTÁ "VIVO" EL ENEMIGO
		if (!gameObject.GetComponent<Renderer> ().isVisible)
			acumulaDestruye += Time.deltaTime;

		else
			acumulaDestruye = 0;
        //SI EL TIEMPO LLEGA A SU TOPE (DESTRUYE) SE DESTRUYE EL ENEMIGO
		if (acumulaDestruye>=destruye && desaparece) 
		{
			Destroy (gameObject);
		}

	}
    //cONTROLA TODO EL MOVIMIENTO 
	void FixedUpdate () {
		//SE CAMBIA LA DIRECCION
		contVuelta += Time.deltaTime;
		if (contVuelta >= cambioDireccion) 
		{
			if (derecha)
				derecha = false;
			else
				derecha = true;
			contVuelta = 0;
		}
		if (derecha) {
			rb.velocity = new Vector2(velocidad, rb.velocity.y);
			GetComponent<SpriteRenderer> ().flipX = false;
		} else {
			rb.velocity = new Vector2(velocidad *-1, rb.velocity.y);
			GetComponent<SpriteRenderer> ().flipX = true;
		}

	
	}

}
