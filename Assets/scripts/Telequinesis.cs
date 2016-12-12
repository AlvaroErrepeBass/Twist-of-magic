using UnityEngine;
using System.Collections;
/// <summary>
/// Script que controla la telequinesis La mueve en los ejes x e y
/// </summary>
public class Telequinesis : MonoBehaviour {

    public float speed;
    float mHorizontal;
    float mVertical;

    
    Rigidbody2D rb;
    Vector2 movement;

    //=======================
    void Start()
    {
		
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector2();
    }
    //=======================
    void Update()
    {
        mHorizontal = Input.GetAxis("Horizontal");
        mVertical = Input.GetAxis("Vertical");

    }
    //=======================
    void FixedUpdate()
    {
        movement = rb.velocity;
        movement.x = mHorizontal * speed;
        movement.y = mVertical * speed;
        rb.velocity = movement;

    }

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Water")
			GM.instance.cajaHundida = true;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Water")
			GM.instance.cajaHundida = false;
	}
}