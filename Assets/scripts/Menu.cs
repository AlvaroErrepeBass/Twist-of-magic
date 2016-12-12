using UnityEngine;
using System.Collections;
/// <summary>
/// sScript que controla la animación del menu  y activa los botones correspondientes en función del tiempo de las animaciones.
/// </summary>
public class Menu : MonoBehaviour {

	
    public GameObject libro, b1, b2,b3,t1,t2,t3; //boton1, texto1...
    public float m;
    
    // Update is called once per frame
    void Update()
    {
        m += Time.deltaTime;

		if (m < 1.5 && m > 1.48) {
			SM.instance.pasarPagina ();
		}
		if (m < 4 && m > 3.98) {
			SM.instance.pasarPagina ();
		}

        if (m > 5.5)
        {
            b1.SetActive(true);
            t1.SetActive(true);
        }
        if (m > 7)
        {
            b2.SetActive(true);
            t2.SetActive(true);

        }
         if (m > 9)
        {
            b3.SetActive(true);
            t3.SetActive(true);

        }
        //libro.GetComponent<Animation>().e

    }
}
