using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
/// <summary>
/// script para cambiar de escena espués del flashback o durante;
/// </summary>
public class CambioDeEscena : MonoBehaviour {
    
		
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);

        }
    }
}
