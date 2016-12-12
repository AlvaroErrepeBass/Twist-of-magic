
using UnityEngine;
using System.Collections;
using System.IO;
/// <summary>
/// Primera versión de la narrativa.
/// La idea original de la narrativa era hacerla con este script pero por temas de aspecto visul y relación de tamaño optamos por usar el otro script.
/// Está sin usar
/// Carga los comentarios de un txt y los reproduce creandoun objeto con el texto encima de la cabeza de la persona que habla.
/// </summary>
public class Narrativa : MonoBehaviour {
    public bool onLevel = true;
    bool firstime = true;                                    //centinela que capta si es la primera colision del trigger
	public GameObject maestro;
	public GameObject allen;
	public GameObject bocadillo;
	public GameObject textoBocadillo;
    Animator anim;
	int i = 0;
    public float time;
    public string texto;
    bool triggered = false;
       
	public struct Comentario
    {
        public string text;
        public string pers;

    }
    Comentario[] comentarios;
    //======================================================================================
	public void LlamarBocadillo (Comentario coment){
        GameObject personaje;
        string texto = coment.text;
        if (coment.pers[0] == 'a' || coment.pers[0] == 'A')
        {
            personaje = allen;
            anim.SetBool("hablando", false);
        }
        else
        {
            personaje = maestro;
            anim = personaje.GetComponent<Animator>();
            anim.SetBool("hablando", true);
        }

		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag ("Bocadillo");
		foreach (GameObject target in gameObjects) {
			GameObject.Destroy (target);
		}

		textoBocadillo.GetComponent<TextMesh>().text = texto;
        
        GameObject.Instantiate (bocadillo,new Vector3 (personaje.transform.position.x+2, personaje.transform.position.y+2,0),personaje.transform.rotation);
        Debug.Log(texto + "    pop: " + texto.Length);
        
         bocadillo.transform.localScale = new Vector3(4+(texto.Length / 10f), 1, 1);
        //textoBocadillo.transform.localScale = new Vector3(0.003f - (texto.Length / 17000f), 0.03f - (texto.Length / 10000f), 0);
    }

	//====================================================================================================
	void Start () {
       // if (onLevel) time = GM.time;
        firstime = GM.firsttime;
        anim = maestro.GetComponent<Animator>();

        string dir = "Assets/textos/" + texto;
        StreamReader entrada = new StreamReader(dir, System.Text.Encoding.Default);
        int lon = int.Parse(entrada.ReadLine());
        comentarios = new Comentario [lon];
        for (int i = 0; i< lon; i++)
        {
            comentarios[i].text = entrada.ReadLine();
            comentarios[i].pers = entrada.ReadLine();

        }
        entrada.Close();
	}

	//==================================================================================================
	void Update () {
        if (triggered)
        {
            allen.GetComponent<Movimiento>().enabled = false;
            
        }
        else allen.GetComponent<Movimiento>().enabled = true;
        if (Input.GetKeyDown (KeyCode.Return) && triggered){
            
            if (i < (comentarios.Length))
            {
                LlamarBocadillo(comentarios[i]);
                i++;
            }
            else
            {
                triggered = false;
                bocadillo = GameObject.FindGameObjectWithTag("Bocadillo");
                DestroyObject(bocadillo);
                anim.SetBool("hablando", false);
                
                maestro.GetComponent<MovimientoMaestro>().enabled = true;
                maestro.GetComponent<MovimientoMaestro>().narrativa = false;
            }
            
        }
        
    }
    //=======================================================================================
    void OnTriggerEnter2D(Collider2D info){
        if (firstime)
        {
            triggered = true;
            GM.firsttime = false;
            maestro.GetComponent<MovimientoMaestro>().enabled = false;
            LlamarBocadillo(comentarios[i]);
            i++;
            firstime = false;
        }
        if (onLevel)
        {
            int i;

            if (time > 15) i = Random.Range(6, 9);
            else if (time < 15 && time > 10) i = Random.Range(3, 6);
            else i = Random.Range(0, 3);
            LlamarBocadillo(comentarios[i]);
        }
    }
 }