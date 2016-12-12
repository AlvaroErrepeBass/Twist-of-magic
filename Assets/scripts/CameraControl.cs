using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour {

  
    public GameObject target;			// GO objetivo que debe seguir la cámara
    Vector3 offset;						// Desplazamiento.
    //float posX;
	public float posXmax;				// Posición límite a la derecha del mapa que puede mostrar la cámara.
	public float posXmin;				// Posición límite a la izquierda del mapa que puede mostrar la cámara.
	public float posYmax;				// Posición límite superior del mapa que puede mostrar la cámara.
	public float posYmin;				// Posición límite inferior del mapa que puede mostrar la cámara.   (No puede ir más allá)
	public float offsetXmax = 6;		// Diferencia máxima a la derecha entre la cámara y su objetivo.
	public float offsetXmin = 6;		// Diferencia máxima a la izquierda entre la cámara y su objetivo.
	public float offsetYmax = 5;		// Diferencia máxima superior entre la cámara y su objetivo.

    // ==============================
    void Start () {
		// Cuando arranque, definir los límites de la cámara segun el mapa en el que se encuentre.
		if (SceneManager.GetActiveScene ().name == "Biblioteca") {		
			posXmax = 36-offsetXmax;
			posXmin = 0+offsetXmin;
			posYmax = 0-offsetYmax;
			posYmin = -11;
		} else if (SceneManager.GetActiveScene ().name == "NivelTutorial") {
			posXmax = 122-offsetXmax;
			posXmin = -5+offsetXmin;
			posYmax = 18-offsetYmax;
			posYmin = -17;
		} else if (SceneManager.GetActiveScene ().name == "Nivel1") {
			posXmax = 127-offsetXmax;
			posXmin = 0+offsetXmin;
			posYmax = 0-offsetYmax;
			posYmin = -41;
		} else if (SceneManager.GetActiveScene ().name == "Nivel2") {
			posXmax = 84-offsetXmax;
			posXmin = 0+offsetXmin;
			posYmax = 0-offsetYmax;
			posYmin = -43;
		} else if (SceneManager.GetActiveScene ().name == "Nivel3") {
			posXmax = 47-offsetXmax;
			posXmin = 0+offsetXmin;
			posYmax = 0-offsetYmax;
			posYmin = -44;
		} else if (SceneManager.GetActiveScene ().name == "Nivel4") {
			posXmax = 261-offsetXmax;
			posXmin = 0+offsetXmin;
			posYmax = 0-offsetYmax;
			posYmin = -57;
		} else if (SceneManager.GetActiveScene ().name == "Flashback1") {
			posXmax = 200-offsetXmax;
			posXmin = 0+offsetXmin;
			posYmax = 0-offsetYmax;
			posYmin = -100;
		}
		// Desplazamiento de la cámara respecto a su objetivo.
        offset = target.transform.position - transform.position;        
	}
	
	// ==========================
	void LateUpdate () {

		// Limite del offset vertical del jugador y la cámara
		if (offset.y > 1) offset.y = 1 ;
		if (offset.y < -2) offset.y=-2;

        Vector3 orig = transform.position;						// Variable que almacena la posición inicial de la cámara en el frame.
        Vector3 destino = target.transform.position - offset;	// Variable que almacena la posición del target a la que debe moverse la cámara.
        
		// Limites de movimiento de la cámara
		if (destino.x < posXmin) destino.x = posXmin;	
		if (destino.x > posXmax) destino.x = posXmax;
		if (destino.y < posYmin) destino.y = posYmin;
		if (destino.y > posYmax) destino.y = posYmax;

		// Control del movimiento suave de la cámara
        Vector3.Lerp(orig, destino, 1 / 20f);
		Vector3 despl = Vector3.Lerp(orig, destino, Time.deltaTime);
        transform.position = despl;
         
	}
}
