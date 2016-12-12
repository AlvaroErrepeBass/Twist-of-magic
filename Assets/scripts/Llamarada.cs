using UnityEngine;
using System.Collections;
/// <summary>
/// Script que controla la llamarada del jugador.
/// </summary>
public class Llamarada : MonoBehaviour {
	float tiempo = 0f;
	public float destruye = 3;
	public Animator anim;
	public Vector2 llamarada = new Vector2 (0.5f, 0f);
	Vector2 auxLlamarada= new Vector2();

	// Use this for initialization
	void Start () {
		auxLlamarada = llamarada*-1;
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();

		if (GM.instance.mirDerecha) {
			anim.SetBool ("Izquierda", false);
			GetComponent<SpriteRenderer> ().flipX = false;
			rb.velocity = llamarada;
		} else { 
			rb.velocity = auxLlamarada;
			anim.SetBool ("Izquierda", true);
			GetComponent<SpriteRenderer> ().flipX = true;
		}
	}

	// Update is called once per frame
	void Update () {
		tiempo = tiempo + Time.deltaTime;

		if (tiempo >= destruye) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (!col.collider.isTrigger)
			Destroy (gameObject);
			SM.instance.MuerteLlama ();
	}

}