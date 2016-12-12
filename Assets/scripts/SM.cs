using UnityEngine;
using System.Collections;
/// <summary>
/// Controla todos los sonidos del juego y los reptroduce cuando corresponde
/// </summary>
public class SM : MonoBehaviour {

	public static SM instance;

	Vector3 distCaida;
	public GameObject player;
	Vector3 posicion;
	private bool escuchado = false; //controla fx que no se deben oir por duplicado
	// Por si nos da por ofrecer un control de los FX de sonido
	public float volume;
	// Para asignar los diferentes sonidos  al objeto por el editor
	public AudioClip[] pasosAgua;
	public AudioClip movAgua;
	public AudioClip[] atraviesaPortal;
	public AudioClip[] lanzaLlama;
	public AudioClip[] hechizoFuego;
	public AudioClip[] choqueFuego;
	public AudioClip[] rafagaTelequinesis;
	public AudioClip[] gestoSalto;
	public AudioClip[] salto1;
	public AudioClip[] salto2;
	public AudioClip[] caidaCaja;
	public AudioClip[] caidaMetal;
	public AudioClip[] gestoCaida;
	public AudioClip[] enemigoAtaque;
	public AudioClip[] enemigoMuerte;
	public AudioClip[] cerrojo;
	public AudioClip desbloqueo;
	public AudioClip[] menuPaginas;
	public AudioClip[] deslizaMenu;
	public AudioClip[] eligeMenu;
	public AudioClip[] hechizoViento;
	public AudioClip[] hechizoAgua;
	public AudioClip[] muerteGesto;
	public AudioClip[] pocion;
	public AudioClip[] voladorAtaque;
	public AudioClip[] voladorMuerte;
	public AudioClip[] rompeCaja;
	public AudioClip cierraPuerta;

	bool telequinesisok;
	public float fadeTime;
	private float espera;
	private float esperaAndar;
	private bool llamaLanzada;
	public bool waterWalk;


	void Start () {
		telequinesisok = false;
		distCaida = player.transform.position;
		waterWalk = false;
		instance = this;
		DontDestroyOnLoad (gameObject);
		fadeTime = 0f;
		llamaLanzada = false;

	}

	public void portal()
	{
		AudioSource.PlayClipAtPoint (atraviesaPortal [Random.Range (0, atraviesaPortal.Length)], posicion, Random.Range (0.3f, 0.6f)* volume);
	}
	public void llamaradaHechizo()
	{

		AudioSource.PlayClipAtPoint (hechizoFuego [Random.Range (0, hechizoFuego.Length)], posicion, Random.Range (0.3f, 0.45f)* volume);
		llamaLanzada = true;
		espera = 1f;
		AudioSource.PlayClipAtPoint (lanzaLlama [Random.Range (0, lanzaLlama.Length)], posicion, Random.Range (0.6f, 0.9f)* volume);

	}


	public void MuerteLlama()
	{
		AudioSource.PlayClipAtPoint (choqueFuego [Random.Range (0, choqueFuego.Length)], posicion, Random.Range (0.3f, 0.6f)* volume);
	}

	public void salto()
	{
		AudioSource.PlayClipAtPoint (salto1 [Random.Range (0, salto1.Length)], posicion, Random.Range (0.2f,  0.25f)* volume);
		AudioSource.PlayClipAtPoint (gestoSalto [Random.Range (0, gestoSalto.Length)], posicion, Random.Range (0.3f , 0.5f )* volume);
	}

	public void dobleSalto()
	{
		AudioSource.PlayClipAtPoint (salto2 [Random.Range (0, salto2.Length)], posicion,Random.Range (0.4f, 0.55f )* volume);
		AudioSource.PlayClipAtPoint (gestoSalto [Random.Range (0, gestoSalto.Length)], posicion, Random.Range (0.3f , 0.5f) * volume);
	}

	public void caida(string objeto, Vector3 pos)
	{
		
		if (objeto == "Jugador") {
			
			AudioSource.PlayClipAtPoint (gestoCaida [Random.Range (0, gestoCaida.Length)], pos, Random.Range (0.85f, 0.95f) * volume);
			AudioSource.PlayClipAtPoint (caidaCaja [Random.Range (0, caidaCaja.Length)], pos, Random.Range (0.9f, 1f) * volume);
			} 
		else
			AudioSource.PlayClipAtPoint (caidaCaja [Random.Range (0, caidaCaja.Length)], pos, 2f * volume);
	}

	public void muerte()
	{
		

			AudioSource.PlayClipAtPoint (muerteGesto [Random.Range (0, muerteGesto.Length)], posicion, Random.Range (0.6f, 0.9f)* volume);

	}

	public void telequinesis()
	{
		if (!telequinesisok) {
			AudioSource.PlayClipAtPoint (hechizoViento [Random.Range (0, hechizoViento.Length)], posicion, 5F * volume);
			AudioSource.PlayClipAtPoint (rafagaTelequinesis [Random.Range (0, rafagaTelequinesis.Length)], posicion, Random.Range (0.1f, 0.25f) * volume);

			telequinesisok = true;
		} else {
			AudioSource.PlayClipAtPoint (rafagaTelequinesis [Random.Range (0, rafagaTelequinesis.Length)], posicion, Random.Range (0.2f, 0.4f) * volume);
			telequinesisok = false;
		}

	}

	public void usaPocion()
	{
		AudioSource.PlayClipAtPoint (pocion [Random.Range (0, pocion.Length)], posicion, Random.Range (0.9f, 1f)* volume);
	}

	public void ataque(string enemigo)
	{
		if (enemigo == "Pollo") 
		{
			AudioSource.PlayClipAtPoint (enemigoAtaque [Random.Range (0, enemigoAtaque.Length)], posicion, Random.Range (0.4f, 0.6f)* volume);

		}
		else if (enemigo == "Volador") 
		{
			AudioSource.PlayClipAtPoint (voladorAtaque [Random.Range (0, voladorAtaque.Length)], posicion, Random.Range (0.4f, 0.6f)* volume);

		}
	}

	public void destruye (string objeto)
	{
		if (objeto == "Pollo") {
			AudioSource.PlayClipAtPoint (enemigoMuerte [Random.Range (0, enemigoMuerte.Length)], posicion, Random.Range (0.4f, 0.5f)* volume);

		}
		else if (objeto == "Volador") {
			AudioSource.PlayClipAtPoint (voladorMuerte [Random.Range (0, voladorMuerte.Length)], posicion, Random.Range (0.4f, 0.5f)* volume);

		}
		else if (objeto == "Caja") {
			AudioSource.PlayClipAtPoint (rompeCaja [Random.Range (0, rompeCaja.Length)], posicion, Random.Range (0.2f, 0.5f)* volume);
		}
	}

	public void mecanismo()
	{
		AudioSource.PlayClipAtPoint (desbloqueo, posicion, 5f* volume);
	}

	public void puerta(string estado)
	{
		if (estado == "Cerrado")
			AudioSource.PlayClipAtPoint (cerrojo [Random.Range (0, cerrojo.Length)], posicion, Random.Range (0.8f, 0.9f)* volume);
		if (estado == "Abierto")
			Invoke ("cerrarPuerta", 0.6f);
	}

	void cerrarPuerta()
	{
		AudioSource.PlayClipAtPoint (cierraPuerta, posicion, Random.Range (0.8f, 0.9f)* volume);
		AudioSource.PlayClipAtPoint (desbloqueo, posicion, Random.Range (0.3f, 0.5f)* volume);
	}

	public void andarAgua()
	{
		AudioSource.PlayClipAtPoint (hechizoAgua [Random.Range (0, hechizoAgua.Length)], posicion, Random.Range (0.8f, 0.9f)* volume);

	}

	public void pasarPagina()
	{
		AudioSource.PlayClipAtPoint (menuPaginas [Random.Range (0, menuPaginas.Length)], posicion, 5* volume);
	}

	public void eligeOpcion()
	{
		AudioSource.PlayClipAtPoint (deslizaMenu [Random.Range (0, deslizaMenu.Length)], new Vector3 (0f,0f,0f), 5* volume);
		Debug.Log ("mouse sobre objeto");
	}

	public void clickOpcion()
	{
		AudioSource.PlayClipAtPoint (eligeMenu [Random.Range (0, eligeMenu.Length)], new Vector3 (0f,0f,0f), 5* volume);

	}

	void Update ()
	{

		//Maneja en que lugar esta el jugador en cada momento
		posicion = player.transform.position;

		// Para manejar tiempos de espera
		espera -= Time.deltaTime;
		esperaAndar -= Time.deltaTime;
		if (espera < 0)	espera = 0;

		// Sonido de lanzar llama despues de oir el hechizo
	

		if (GM.instance.enAgua  && GM.instance.andando && esperaAndar <0) {
			AudioSource.PlayClipAtPoint (pasosAgua [Random.Range (0, pasosAgua.Length)], posicion, Random.Range (0.05f, 0.1f)* volume);
			esperaAndar = 0.5f;
		}	


	}
}
