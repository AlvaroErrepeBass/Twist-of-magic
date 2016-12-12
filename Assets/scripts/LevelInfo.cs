using UnityEngine;
using System.Collections;
using System.IO;
/// <summary>
/// Script del prefab LevelInfo: al final de cada nivel guarda en un txt el nivel que se acaba de pasar el jugador y el tiempo que ha tardado
/// si esá en la biblioteca se lee ese documento y se actualiza el marcador con el tiempo y se desbloquean los niveles según el jugador supera los anteriores
/// </summary>
public class LevelInfo : MonoBehaviour {
    public GameObject chascarrillo = null;
	public int level;
    int contadorLevel = 0;
	public GameObject porton1, porton2, porton3, porton4;
    public GameObject texto1, texto2, texto3, texto4, texto5;
    Animator anim;
	// Use this for initialization
	void Start () {
       
		if (level == -1) {  //Si está en la biblioteca carga lo txt actualizando puertas y marcadores.
            int niveltemp;
            string tiempo;
           
                try
                {
                    StreamReader entrada = new StreamReader("Level0Info.txt");
                    niveltemp = int.Parse(entrada.ReadLine());
                    tiempo = entrada.ReadLine();
                    porton1.GetComponent<Portones>().estado = "Cerrado";
                    texto1.GetComponent<TextMesh>().text = tiempo;
                    entrada.Close();
                    contadorLevel++;
                }
                catch { Debug.Log("No se ha pasado el nivel tutorial"); }
            
          
            
                try
                {
                    StreamReader entrada = new StreamReader("Level1Info.txt");
                    niveltemp = int.Parse(entrada.ReadLine());
                    tiempo = entrada.ReadLine();
                    porton2.GetComponent<Portones>().estado = "Cerrado";
                    texto2.GetComponent<TextMesh>().text = tiempo;
                    entrada.Close();
                    contadorLevel++;
                }
                catch { Debug.Log("No se ha pasado el nivel 1"); }
           
                try
                {
                    StreamReader entrada = new StreamReader("Level2Info.txt");
                    niveltemp = int.Parse(entrada.ReadLine());
                    tiempo = entrada.ReadLine();
                    porton3.GetComponent<Portones>().estado = "Cerrado";
                    texto3.GetComponent<TextMesh>().text = tiempo;
                    entrada.Close();
                    contadorLevel++;
                }
                catch { Debug.Log("No se ha pasado el nivel 2"); }
          
                try
                {
                    StreamReader entrada = new StreamReader("Level3Info.txt");
                    niveltemp = int.Parse(entrada.ReadLine());
                    tiempo = entrada.ReadLine();
                    porton4.GetComponent<Portones>().estado = "Cerrado";
                    texto4.GetComponent<TextMesh>().text = tiempo;
                    entrada.Close();
                    contadorLevel++;
                }
                catch { Debug.Log("No se ha pasado el nivel 3"); }
           
                try
                {
                    StreamReader entrada = new StreamReader("Level4Info.txt");
                    niveltemp = int.Parse(entrada.ReadLine());
                    tiempo = entrada.ReadLine();
                    
                    texto5.GetComponent<TextMesh>().text = tiempo;
                    entrada.Close();
                    contadorLevel++;
                }
                catch { Debug.Log("No se ha pasado el nivel 4"); }
           
        }
	}
	
	
	void OnTriggerEnter2D(Collider2D info){

        //Al final del nivel se suelta un chascarrillo. Se activa la imagen con el chascarrillo al tocar con el trigger
        try
        {
            anim = chascarrillo.GetComponent<Animator>();
            chascarrillo.SetActive(true);
            anim.SetFloat("chascarrillo", (GM.instance.tiempo / 60f));
        }
        catch { Debug.Log("No hay chascarrillo"); }
        string textotiempo;
        string archivo = "Level" + level.ToString() + "Info.txt";
        transformaTexto(out textotiempo);
		StreamWriter salida = new StreamWriter (archivo);
		salida.WriteLine (level);
		salida.WriteLine (textotiempo);
		salida.Close ();
	}
    //Método que pone el tiempo del nivel en minutos y segundos y lo pasa a texto.
    void transformaTexto(out string texto)
    {
       float time =  GM.instance.tiempo;
        int min = (int)time / 60;
        int seg = (int)time % 60;
        texto = min.ToString() +":"+seg.ToString();

    }
}
