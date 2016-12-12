using UnityEngine;
using System.Collections;
/// <summary>
/// Script que comprueba si la caja se puede mover o no
/// </summary>
public class Movible : MonoBehaviour {
	bool suena = false;			//para no oir todas las cajas segun empieza el nivel
	float espera = 0;			//por eso esperamos un segundo
	public LayerMask layer;

	void Update()
	{
		if (espera <= 1)
			espera += Time.deltaTime;
		else if (espera > 1)
			suena = true;


	}

	public bool mover (float xInc){
		
		Vector2 pos = new Vector2 (transform.position.x - 0.4f, transform.position.y + 1.1f);
		RaycastHit2D hit;

		hit = Physics2D.Raycast (pos, new Vector2 (xInc,0f), 0.8f);
		bool inmovil;

		if (hit.collider != null && !hit.collider.isTrigger) 
		{
			inmovil = true;
	//		Debug.Log ("algo encima");

		} 
		else
		{
			inmovil = false;
	//		Debug.Log ("nada encima");
		}

		return inmovil;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.tag != "Player") {
			if (!gameObject.GetComponent<Telequinesis> ().enabled && suena) {
				SM.instance.caida ("Caja", transform.position);

			}
		}

	}

}
