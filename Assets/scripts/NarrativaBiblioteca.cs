using UnityEngine;
using System.Collections;
/// <summary>
/// Controla toda la conversación de la biblioteca con animaciones,
/// </summary>
public class NarrativaBiblioteca : MonoBehaviour {
    public bool onLevel = true;                                  //Variable que controla si estamos en nivel o si estamos en la biblioteca
    public bool firstime = true;                                //centinela que capta si es la primera colision del trigger
    public GameObject pergamino;                                //GO con la imagen que contiene la animacion de la conversación.
    public GameObject allen;                                   
    public GameObject maestro;
    Animator animM, animNa, animAllen;
    int narrativa = 0;                                          //Contador de comentarios, va de 0 a 9 (9 comentarios).
   
   
    bool triggered = false;                                     //Controla si ha entrado en el trigger.

   

    //====================================================================================================
    void Start()
    {
        
        firstime = GM.firsttime;                                     //En el gm se guarda si se ha pasado la narrativa o no, aquí  se recoge ese valor.
        animNa = pergamino.GetComponent<Animator>();                 
        animM = maestro.GetComponent<Animator>();                    //Se guardan los Animator 
        animAllen = allen.GetComponent<Animator>();

    }

    //==================================================================================================
    void Update()
    {
        
       //RECOGE EL IMPUT PARA CAMBIAR DE ANIMACION: AVANZA UNA HACIA ADELANTE
		if  (((Input.GetKeyDown(KeyCode.RightArrow)|| Input.GetKeyDown(KeyCode.Insert))|| Input.GetKeyDown(KeyCode.Space)) && triggered) 
        {
            narrativa++;
            animNa.SetInteger("narrativa", narrativa);
			SM.instance.pasarPagina ();

        }
        //RECOGE EL IMPUT PARA CAMBIAR DE ANIMACION: AVANZA UNA HACIA ATRÁS
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && triggered)
        {
            if (narrativa > 0) narrativa--;
            animNa.SetInteger("narrativa", narrativa);
			SM.instance.pasarPagina ();

        }
         
        if (narrativa == 9) { pergamino.SetActive(false);  activaMovimiento(); }            //Se acaban los comentarios y se "cierra" la imagen. Se vuelve a activar el movimiento del player.
    }
    //METODO UE VUELVE A CONFIGURAR TODO EL MOVIMIENTO DEL JUGADOR.
    void activaMovimiento()
    {
        allen.GetComponent<Movimiento>().enabled = true;
        maestro.GetComponent<MovimientoMaestro>().enabled = true;
        maestro.GetComponent<MovimientoMaestro>().narrativa = false;
        animM.SetBool("hablando", false);
    }
    //=======================================================================================
    void OnTriggerEnter2D(Collider2D info)
    {
        if (firstime)                                                                   //Si es la primera vez que entra en el trigger...
        {
            triggered = true;
            GM.firsttime = false;                                                       //Se actualiza el valor de la variable firsttime del GM
            pergamino.SetActive(true);                                                  //Se activa la imagen con la narrativa.
            allen.GetComponent<Movimiento>().enabled = false;
            animAllen.SetFloat("VerSpeed", 0);
            animAllen.SetFloat("HorSpeed", 0);
            animAllen.SetBool("sobreEscalera", false);
            maestro.GetComponent<MovimientoMaestro>().enabled = false;
            animM.SetBool("hablando", true);
            firstime = false;
        }
       
    }
}
