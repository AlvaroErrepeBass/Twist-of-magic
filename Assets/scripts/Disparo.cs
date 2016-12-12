// Componente para que el GO asociado pueda disparar elementos, se diferencian jugador(llamarada) y NPC (bola azul).
using UnityEngine;
using System.Collections;
										
public class Disparo : MonoBehaviour {
	public GameObject llamarada;	// Público para poder definir que objeto se creara al disparar una llamarada.
	public GameObject bolaAzul;		// Público para poder definir que objeto se creara
	public GameObject jugador;		// Que GO es el jugador.
	public float frecuencia;		// Variable pública para definir con que frecuencia disparan los NPC la bolaAzul
	private float tiempo = 0;		// Variable que acumula tiempo.
	public bool llama = true;		//OJO! Para que funcione solo uno de los dos puede estar a true.
	public bool blueBall = false;

	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		//Avisamos de que estan ambos activos. 
		if (llama && blueBall) {  			
			Debug.Log ("¡OJO! El GO " + gameObject.name + "no puede tener llama y blueBall en TRUE al mismo tiempo");
			Debug.Log ("en su componente Disparo.");
		}

		//Para no disparar sin ton ni son, acumula tiempo hasta alcanzar el valor de frecuencia.
		tiempo += Time.deltaTime;


		//DISPARO BOLAS DE FUEGO DEL JUGADOR
		if (Input.GetKeyDown (KeyCode.LeftArrow) && llama) {		// Control de hacia donde mira el jugador y por lo tanto hacia donde las dispara.
			GM.instance.mirDerecha = false;
		}
		if (Input.GetKeyDown (KeyCode.RightArrow) && llama) {
			GM.instance.mirDerecha = true;
		}
		if (Input.GetButtonDown ("Fire") && GM.instance.mana > 0) // Cuando recibe el input de disparo y tiene mana suficiente
		{
			if (llama && !blueBall && tiempo >= frecuencia) {	  
				SM.instance.llamaradaHechizo ();
				disparo ();
				tiempo = 0;
			}
		}

		//DISPARO DE LLAMAS DE NPCS
		if (!llama && blueBall) {
			if (tiempo >= frecuencia) {
				GameObject dispara = GameObject.Instantiate (bolaAzul);
				dispara.transform.position = new Vector2 (transform.position.x, transform.position.y);
				tiempo = 0;
			}
		}

			
	}

	void disparo()
	{
		GameObject dispara = GameObject.Instantiate (llamarada);
		dispara.transform.position = new Vector2 (jugador.transform.position.x, jugador.transform.position.y);
		GM.instance.Disparo ();
	}
}
