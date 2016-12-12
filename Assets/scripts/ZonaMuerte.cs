using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
/// <summary>
/// Script de zona de muerte
/// Cuando el player toca con la zona de muerte vuelve al último respawn. Los enemigos al tocar se destruyen. Se pueden asignar cajas para que al caer vuelvan al respawn
/// </summary>
public class ZonaMuerte : MonoBehaviour {

	public GameObject respawnCaja;
    public bool destruyeEnemigo = false;
     

    void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == ("Player")) { 
			GM.instance.mana = 0;
			col.GetComponent<Animator> ().SetBool ("muerto", true);
		} else if (col.tag == ("Caja")) {
			try {
				col.transform.position = respawnCaja.transform.position;
			} catch {
				Debug.Log("No se ha especificado ningun respawn para cajas que caigan a zona muerte");
			}
		}
        if (destruyeEnemigo)
        {
            if (col.tag == ("Pollo"))
                
            try { Destroy(col.gameObject); }
            catch { }
        }
	}
}