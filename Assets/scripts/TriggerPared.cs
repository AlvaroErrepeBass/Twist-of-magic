using UnityEngine;
using System.Collections;
/// <summary>
/// Script que controla cuando se mete una caja del tipo deseado en la pared  y activa una trampilla o destruye otro objeto.
/// </summary>
public class TriggerPared : MonoBehaviour {
	public GameObject accion = null;                //Trampilla que va a activar
    public GameObject destruye = null;              //Objeto que va a destruir.
    public bool variasAcciones = false;             //Variable que controla si se van a activar varias trampillas o solo una
    public GameObject accion2 = null;               //Segunda trampilla
    
    public GameObject tipocaja;                     //Controla que caja es la que tiene que chocar con el trigger de la pared.
    public bool cajaencima;                         //Controla si ya hay una caja chocando con el trigger.
    

    //SE HA COLOCADO LA CAJA
    void OnTriggerEnter2D(Collider2D info)
    {
        
        cajaencima = true;
        if (info.GetComponent<Collider2D>() == tipocaja.GetComponent<Collider2D>())         //Si la caja coincide con el tipo de caja ...
        {
            if (destruye != null) Destroy(destruye);                                        //Si hay que destruir algo se destruye
            if (accion != null && accion.tag == "Trampilla")                                //Se activa la trampilla
            {
                accion.GetComponentInChildren<Trampilla>().activado = true;
            }
            if (variasAcciones) {                                                           //Si hay otra acción se realiza.
                if (accion2 != null && accion2.tag == "Trampilla")
                {
                    accion2.GetComponentInChildren<Trampilla>().activado = true;
                }
            }
        }
      
    }
    //SI SE QUITAN LAS CAJAS, LAS TRAMPILLAS VUELVEN A SU ESTADO ANTERIOR
    void OnTriggerExit2D(Collider2D info)
    {
        cajaencima = false;
        if (accion != null && accion.tag == "Trampilla")
        {
            accion.GetComponentInChildren<Trampilla>().activado = false;
        }
        if (variasAcciones)
        {
            if (accion2 != null && accion2.tag == "Trampilla")
            {
                accion2.GetComponentInChildren<Trampilla>().activado = false;
            }
        }
       
    }
}
