using UnityEngine;
using System.Collections;
/// <summary>
/// Script usado para los creditos
/// </summary>
public class Creditos : MonoBehaviour {


    public GameObject boton, texto;
    public float m;
	// Update is called once per frame
	void Update () {
        m += Time.deltaTime;
        if (m > 27)   //Cuando pasan los 27 segundos (lo que dura la animacion) se activa el botón de salir.
        {
            boton.SetActive(true);
            texto.SetActive(true);
        }
    }
}
