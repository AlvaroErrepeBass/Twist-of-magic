using UnityEngine;
using System.Collections;

public class MovimientoProyectil : MonoBehaviour {

	public float velocidad = 1f;
	public Vector2 direccion;
	public bool destrOnImpact=true;
	public bool soloImpactJugad = true;
	public float tiempovida=50;
	private float lifetime;

	// Use this for initialization
	void Start () {
		direccion = new Vector2 (direccion.x * velocidad, direccion.y * velocidad);
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		rb.velocity = direccion;
	}

	void Update (){
		lifetime = lifetime + Time.deltaTime;
		if (lifetime >= tiempovida)
			Destroy (gameObject);
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		//Solo se destruira al impactar si tiene activo destrOnImpact
		if (!col.collider.isTrigger && destrOnImpact && !soloImpactJugad) //Si puede impactar con cualquier rigidbody y destruirse
			Destroy (gameObject);
		if (soloImpactJugad && destrOnImpact) {	//Si solo puede impactar con el jugador y destruirse.
			if (col.collider.tag == "Player")
				Destroy (gameObject);
		}
	}
}
