using UnityEngine;
using System.Collections;
/// <summary>
/// Script que controla las antorchas y su animación. Pueden estar encendidas o encenderse al dispararlas
/// </summary>
public class Torch : MonoBehaviour {

	public bool encendido;
	public Animator anim;
	private Light luzpropia;
	public int variacion = 1;
	public float rangoMax = 2.2f;
	public float rangoMin = 2f;

	void Start (){
		luzpropia = GetComponentInChildren<Light>();
		if (encendido) 
		{
			anim.SetBool ("Encendido", true);
			luzpropia.enabled = true;
		}
	}

	void Update (){
		if (encendido) {
			//luzpropia.intensity += variacion * (Time.deltaTime * 1f);
			luzpropia.range += variacion * (Time.deltaTime * 0.4f);
			if (luzpropia.range <= rangoMin) {
				luzpropia.range = rangoMin;
				variacion = -variacion;
			} else if (luzpropia.range >= rangoMax) {
				luzpropia.range = rangoMax;
				variacion = -variacion;
			}
		}

	}

	void OnTriggerEnter2D (){
		if (!encendido){
			//Debug.Log ("Algo entro en trigger de la antorcha");
			anim.SetBool ("Encendido", true);
			GM.instance.antorchasActivadas = GM.instance.antorchasActivadas+1;
			luzpropia.enabled = true;
			encendido = true;
			//Debug.Log ("La llamarada entro en colisión de la antorcha");
		}
	}
}
