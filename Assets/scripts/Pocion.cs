using UnityEngine;
using System.Collections;
/// <summary>
/// Poción, recupera el maná 
/// </summary>
public class Pocion : MonoBehaviour {

	public int pocion;
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player") {
			//Debug.Log ("destruye pocion");
			GM.instance.mana += pocion;
			Destroy (gameObject);
			SM.instance.usaPocion ();
		}
	}
}
