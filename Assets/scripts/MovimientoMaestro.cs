using UnityEngine;
using System.Collections;
/// <summary>
/// Script que controla el movimiento del maestro sobre el eje X si h terminado la narrativa
/// </summary>
public class MovimientoMaestro : MonoBehaviour {

    public bool narrativa = true; // para que actue de cierto modo solo durante el tutorial.
    public float ejeX;
    public float incrementoX = 0.1f;
    private float topeDer, topeIzq;
    private bool derecha;
    public Vector3 destino;

    Animator anim;
    // Use this for initialization
    void Start () {
       
        topeDer = transform.position.x + ejeX;
        topeIzq = transform.position.x - ejeX;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update () {
        if (narrativa)
        {
            if (transform.position.x + incrementoX >= topeDer)
                incrementoX = -incrementoX;
            if (transform.position.x + incrementoX <= topeIzq)
                incrementoX = -incrementoX;
            // Aqui se define hacia donde mira
            if (incrementoX < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (incrementoX > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            //Línea de código que modifica la posición
            transform.Translate(incrementoX * Time.deltaTime, 0, 0);
            anim.SetFloat("velx",Mathf.Abs(incrementoX * Time.deltaTime));
        } // fin condicion narrativa
        else
        {
            //Vector3.Lerp(transform.position, destino, 1 / 20f);
            Vector3 despl = Vector3.LerpUnclamped (transform.position, destino, Time.deltaTime);
            transform.position = despl;
            float dir = destino.x - transform.position.x;
            if (dir< 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (dir> 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            if (transform.position.x > destino.x - 0.5f && transform.position.x < destino.x + 0.5f)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                anim.Stop();
            }

            
          
        }

    }
}
