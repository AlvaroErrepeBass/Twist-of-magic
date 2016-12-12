using UnityEngine;
using System.Collections;
/// <summary>
/// Script del generador de enemigos
/// Genera un vampiro en una posición determinada, con la opción de destruirse fuera de camara, con una velocidad determinada y con una tiempo determinado para andar en una dirección
/// </summary>
public class GeneradorEnemigos : MonoBehaviour {

	public GameObject vampiro;
	public GameObject generador;
	public float velGeneracion;
	public bool derecha;
	public bool desaparece;
	public float velocidad;
	public float cambioDireccion;
	public float empujeX;
	public float empujeY;


	IEnumerator CorutinaGeneracion(){
		while (true) {
			yield return new WaitForSeconds (velGeneracion);
			//código a ejecutar cada velGeneracion segundos
			GameObject.Instantiate (vampiro);
			vampiro.transform.position = new Vector2 (generador.transform.position.x-1.4f, generador.transform.position.y-0.8f);
			vampiro.GetComponent<Vampiro> ().derecha = derecha;
			vampiro.GetComponent<Vampiro> ().desaparece = desaparece;
			vampiro.GetComponent<Vampiro> ().velocidad = velocidad;
			vampiro.GetComponent<Vampiro> ().cambioDireccion = cambioDireccion;
			vampiro.GetComponent<Vampiro> ().empujeX = empujeX;
			vampiro.GetComponent<Vampiro> ().empujeY = empujeY;
		}
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(CorutinaGeneracion());
	}

	void Update () {

	}
}
