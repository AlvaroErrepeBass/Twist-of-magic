﻿// -----------------------------------------------------------------
// Material adicional de la práctica 3.
// Motores de Videojuegos - FDI - UCM
// Modificado para su uso en Proyecto I - Twist of Magic - FDI - UCM
// -----------------------------------------------------------------
// Profesor: Marco Antonio Gómez Martín
// Modificaciones: Antonio Cardona Costa
// -----------------------------------------------------------------


using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GeneradorNivel : MonoBehaviour
{

    /// <summary>
    /// Método llamado cuando se selecciona la opción del editor.
    /// </summary>
    [MenuItem("Twist Of Magic/Generar Nivel")]
    public static void buildLevel()
    {
        var path = EditorUtility.OpenFilePanel("Selecciona fichero de texto", "", "txt"); //Declara variable Path donde se guarda la raiz del archivo a leer

        if (path.Length == 0)													//Si el archivo esta vacio...
            return;																//... termina el metodo

        if (!CheckEmptyRootNode()) 												//?????? Ver mas abajo el metodo
            return;																//Creo que comprueba que la escena este vacia. Si la escena contiene elementos termina el metodo.

        initPrefabsBD();														//Llamada al metodo initPrefabsBD() - VER METODO ABAJO

        GameObject root = GameObject.Find("Nivel");							//Declara e inicializa el GO root como el GO llamado Static.
	
        try
        {
            int y = 0;															//Declara e inicializa variable y que definira la posicion y del objeto creado
            string line;														//Declara variable line de tipo string

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(path);		//Lee el fichero
			while ((line = file.ReadLine()) != null)							//line almacena la primera linea del archivo almacenado en file y este no sea null (se haya terminado)
            {
				Debug.Log("Lo leido es :" + line);
				Debug.Log("El valor de y es: " + y);
				for (int x = 0; x < line.Length; ++x)								//Bucle lectura, va recorriendo las posiciones de cada caracter en linea.
                {
                    var c = line[x];											//Declara e Inicializa variable 'c' que contiene el caracter en la posicion x de la linea leida en line.
                    if (c == ' ')												//Si C es un espacio...
                        continue;												//Continuar el codigo ???

                    if (!prefabs.ContainsKey(c))								// Si los prefabs no contienen el caracter almacenado en C tira linea debug. viene de initPrefabsBD()?
                    {
                        Debug.LogError("Caracter '" + c + "' no entendido en (" + (x + 1) + ", " + (y + 1) + "."); //Linea del debug.
                        continue;
                    }
                    Object prefab = prefabs[c];									//Declara e Inicia un objeto llamado prefab que contiene el objeto "c" de prefabs
                    if (prefab != null)
					{											//Si no da null...
                        createObject(prefab, root, new Vector3(x, -y, 0));		//... crea el objeto prefab obtenido de prefabs[c], del GameObject padre root ¿?, en la posicion x,-y actua
					}
				}
                y++;															//Aumentamos y, pasamos a la siguiente fila del archivo de texto
            }
            file.Close();														//Al salir del while por que ya no quedan mas lineas en el archivo, cerramos el archivo de texto.
        }
        catch (IOException)														//Si detectamos un error de In/Out...
        {
            Debug.LogError("Error de lectura del fichero " + path + ".");		//...devolvemos el mensaje Error de lectura de fichero + raiz + .
        }
    }

    private static bool CheckEmptyRootNode()	
    {
        // Sacamos la raíz de la escena
        GameObject sceneRoot = GameObject.Find("Nivel"); 						//Declara e Inicializa el GO sceneRoot con una busqueda de gameobject llamado "Static"

        if (sceneRoot == null)													//Si no encuentra ese objeto...
        {
            sceneRoot = new GameObject();										//Inicializa sceneRoot como un nuevo GO vacio...
            sceneRoot.name = "Nivel";											//... y le da el nombre Static.
        }

        if (sceneRoot.transform.childCount > 0)									//Si sceneRoot es un objeto con ¿'hijos'? en su gerarquia...
        {
            if (!EditorUtility.DisplayDialog("Confirmación", "El GameObject 'Static' no está vacio. ¿Continuar?", "Sí", "No"))  //... lanza el mensaje...
                return false;													//... y devuelve FALSE a la salida del metodo
        }

        return true;															//Si el sceneRoot (raiz de la escena) esta vacio, se crea el objeto Static y devuelve TRUE a la salida.
    }


    private struct PrefabInfo													//Struct de variables de distintos tipos
    {
        public char code;														//Declaracion variable char llamada code
        public string file;														//Declaracion variable string llamada file
        public PrefabInfo(char c, string f) { code = c; file = f; } 			//Declaracion contenedor PrefabInfor con argumentos Char 'c' y String 'f', donde almacenamos la codificacion de letra junto a prefab.
    }

    static PrefabInfo[] all = new PrefabInfo[] {								// Array que almacenalos valores con el vinculo entre el caracter elegido y el prefab que coloca en escena.
		new PrefabInfo('A', "Assets/Prefabs/Agua/Tile-Agua.prefab"),			// Memorizamos que M coloca el prefab Muro.prefab
		new PrefabInfo('a', "Assets/Prefabs/Agua/Tile-Superficie.prefab"),
		new PrefabInfo('B', "Assets/Prefabs/Objetos/Tile-Barricada.prefab"),
		new PrefabInfo('b', "Assets/Prefabs/Objetos/Tile-BarricadaDestruida.prefab"),
		new PrefabInfo('C', "Assets/Prefabs/Objetos/CajaMadera.prefab"),	// <<<------ EN ESTA ZONA INTRODUCIR LOS TILES Y SU LOCALIZACION
		new PrefabInfo('c', "Assets/Prefabs/Objetos/CajaMetal.prefab"),
		new PrefabInfo('D', "Assets/Prefabs/Objetos/Trampilla-Base.prefab"),
		new PrefabInfo('E', "Assets/Prefabs/Objetos/Tile-EscaleraMadera.prefab"),
		new PrefabInfo('e', "Assets/Prefabs/Objetos/Tile-EscaleraSuperior.prefab"),
		new PrefabInfo('F', "Assets/Prefabs/Suelos/Tile-FloorPiedra.prefab"),	// ...
		new PrefabInfo('f', "Assets/Prefabs/Suelos/Tile-FloorMadera.prefab"),
		new PrefabInfo('G', "Assets/Prefabs/Suelos/Tile-Fondos.prefab"),
		new PrefabInfo('g', "Assets/Prefabs/Suelos/Tile-FloorTierra.prefab"),
		new PrefabInfo('H', "Assets/Prefabs/Atrezzo/Porton.prefab"),
		new PrefabInfo('h', "Assets/Prefabs/Atrezzo/Tile-Cartel.prefab"),
		new PrefabInfo('J', "Assets/Prefabs/Jugador.prefab"),
		new PrefabInfo('L', "Assets/Prefabs/Suelos/PasarelaIzquierda.prefab"),
		new PrefabInfo('M', "Assets/Prefabs/Suelos/PasarelaCentral.prefab"),
		new PrefabInfo('N', "Assets/Prefabs/Suelos/PasarelaDerecha.prefab"),
		new PrefabInfo('o', "Assets/Prefabs/Objetos/Pocion Mana.prefab"),
		new PrefabInfo('O', "Assets/Prefabs/Objetos/Tile-PocionGra.prefab"),
		new PrefabInfo('P', "Assets/Prefabs/Objetos/Tile-Portal.prefab"),
		new PrefabInfo('Q', "Assets/Prefabs/Objetos/Generador enemigos.prefab"),
		new PrefabInfo('R', "Assets/Prefabs/Atrezzo/Tile-Aplique.prefab"),
		new PrefabInfo('S', "Assets/Prefabs/Objetos/Tile-CandelabroOFF.prefab"),
		new PrefabInfo('s', "Assets/Prefabs/Objetos/Tile-CandelabroON.prefab"),
		new PrefabInfo('T', "Assets/Prefabs/Objetos/Antorcha.prefab"),
		new PrefabInfo('U', "Assets/Prefabs/Objetos/Tile-Mesa.prefab"),
		new PrefabInfo('V', "Assets/Prefabs/Objetos/Tile-Silla-Izq.prefab"),
		new PrefabInfo('W', "Assets/Prefabs/Objetos/Tile-Silla-Der.prefab"),
		new PrefabInfo('X', "Assets/Prefabs/Atrezzo/Tile-Estanteria.prefab"),
		new PrefabInfo('Y', "Assets/Prefabs/Objetos/Boton.prefab"),
		new PrefabInfo('Z', "Assets/Prefabs/Objetos/Glifo.prefab"),
		new PrefabInfo('z', "Assets/Prefabs/Objetos/Tile-Barril.prefab"),




	};

    static Dictionary<char, Object> prefabs;								//Genera un indice de relaciones entre caracter y objetos llamado 'prefabs'

    private static void initPrefabsBD()										//Metodo llamado en BuildLevel
    {
        if (prefabs == null)												//Si la biblioteca/diccionario prefabs esta vacia...
            prefabs = new Dictionary<char, Object>();						//inicializa la biblioteca/diccionario.
        foreach (var info in all)											//por cada variable 'info' dentro de todo
            addPrefab(info.code, info.file);								//llama metodo añadir prefab con los parametros info.code, info.file. ¿De donde vienen estos parametros?
    }

    private static void addPrefab(char key, string prefabName)							//Metodo añade prefab. ¿Private es un public que afecta solo a este componente? No, eso seria static. ¿Que hace?
    {																					//Este metodo sirve para crear nuevas asociaciones de caracteres de nuestro texto con prefabs de unity																		
        if (prefabs.ContainsKey(key))													//Si la biblioteca contiene el caracter key de la llamada es que ya esta utilizado...
            // La BD ya está inicializada (por otra invocación a la opción del menú)
            return;																		//... por lo que termina el metodo. ¿Deberia dar un mensaje para que se sepa que el caracter ya esta en uso?

        Object prefab = AssetDatabase.LoadAssetAtPath(prefabName, typeof(GameObject));	//Declaracion e inicializacion del Objeto 'prefab' como el objeto prefabName, de tipo (gameObject). ¿¿DUDAS??
        if (prefab == null)																//Si prefab esta vacio...
        {
            Debug.LogError("Prefab " + prefabName + " no encontrado.");					//.., es que no se ha encontrado el prefab desado...
            return;																		//... y cierra el metodo.
        }
        prefabs.Add(key, prefab);														//Añade al indice de prefabs la sociacion de char y prefab si el caracter no esta usado y se ha localizado el prefab a asociar.
    }

    /// <summary>
    /// Instancia el prefab en la posición indicada; el objeto creado será
    /// hijo del objeto recibido como parámetro.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="parent"></param>
    /// <param name="pos"></param>
    static GameObject createObject(Object prefab, GameObject parent, Vector3 pos)		//Crea el objeto prefab, del objeto padre parent (no entiendo la funcion de esto), en la posicion pos)
    {
        GameObject go;																	//Declaramos un nuevo GameObject llamado go
        go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;						//Inicializamos go como el objeto solicitado prefab.
        go.transform.position = pos;													//Colocamos go en la posicion deseada.
        go.transform.rotation = Quaternion.identity;									//Ni idea de que es esto, rotacion, si, ¿pero para que? Supuestamente es la misma directamente que la del prefab
        go.transform.parent = parent.transform;											//No entiendo la funcion de los parent/children en todo esto.
        return go;																		//Devuelve el GameObject go.
    }
}
