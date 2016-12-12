using UnityEngine;
using System.Collections;
/// <summary>
/// Reproduce un sonido si el raton se pone encima de un botón
/// </summary>
public class EncimaBoton : MonoBehaviour {

	

	void OnMouseEnter()
	{
		SM.instance.eligeOpcion ();
		Debug.Log ("hola");
	}
	void OnMouseExit()
	{
		SM.instance.eligeOpcion ();
	}
}
