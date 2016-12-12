using UnityEngine;
using System.Collections;
/// <summary>
/// Script que cambia el renderizado del cursor.
/// </summary>
public class Cursor : MonoBehaviour {

    
    public Texture2D p;
    public Vector2 hotspot;
	void Start () {
        
        UnityEngine.Cursor.SetCursor(p,hotspot, CursorMode.Auto);
	}
	
	
}
