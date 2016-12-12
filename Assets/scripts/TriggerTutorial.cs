using UnityEngine;
using System.Collections;
/// <summary>
/// Trigger que activa la imagen con el texto que informa del tutorial.
/// </summary>
public class TriggerTutorial : MonoBehaviour
{
    public GameObject imagen;       //texto que va salir como comentario.
    public GameObject allen;        //GO del player.
    Animator animAllen;           
    bool triggered = false;         //Detecta si ha entrado en el trigger.
    bool firsttime = true;          //Detecta si es la primera vez que entra.

    //=======================================================
    void Start()
    {
        animAllen = allen.GetComponent<Animator>();
    }
    // ==========================================================
    void Update()
    {
		if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown (KeyCode.V)) && triggered)    //Si se pulsa intro o espacio se desactiva y el player recupera el movimiento.
            {
            imagen.SetActive(false);
            allen.GetComponent<Movimiento>().enabled = true;
            triggered = false;
            }
    }
    void OnTriggerEnter2D(Collider2D info)
    {
        if (firsttime)                                                  //Si es la primera vez que choca se activa la imagen la información y se desactiva el movimiento del player.
        {
            triggered = true;
            imagen.SetActive(true);
            allen.GetComponent<Movimiento>().enabled = false;
            animAllen.SetFloat("VerSpeed", 0);
            animAllen.SetFloat("HorSpeed", 0);
            animAllen.SetBool("sobreEscalera", false);
            firsttime = false;                                         //se pone la variable en falso por si se vuelve a chocar con el trigger.
        }
    }
}
