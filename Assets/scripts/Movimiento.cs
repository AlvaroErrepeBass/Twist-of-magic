using UnityEngine;
using System.Collections;

public class Movimiento : MonoBehaviour
{
	public static Movimiento instance;
	public int numSaltos = 0;
	public float speed;
	public float vspeed;
    public float jumpPower = 10f;
    public float mHorizontal;
	public float mVertical;
	public float knockbackX;
	public float knockbackY;
	public float knockbackLenght;
	public float knockbackcount;
	public bool knockfromRight;
    public bool grounded;
	public bool sobreEscalera;
	public bool enAgua;
	public bool enAire;
	public float jumpTim = 0;
    
	private float storeJumpPw;
	private float gravityStore;
	private bool salto;

    Animator anim;
	Rigidbody2D rb;
    Vector2 movement;
	RaycastHit2D hitDer;
	RaycastHit2D hitIzq;
	public Transform groundcheckpoint;
	public LayerMask ground;


    //=======================
    void Start()
    {
		instance = this;
		storeJumpPw = jumpPower;
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector2();
        grounded = true;
		gravityStore = rb.gravityScale;
        anim = GetComponent<Animator>();
	

    }
    //=======================
    void Update()
    {
		
		//ANIMACIONES
        anim.SetFloat("HorSpeed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("VerSpeed", Mathf.Abs(rb.velocity.y));
		anim.SetBool ("sobreEscalera", sobreEscalera);


      //  Debug.Log(numSaltos);


		// Gestion Grounded
		if (Physics2D.OverlapCircle (groundcheckpoint.position, 0.02f, ground)) 
		{
			GM.instance.enAire = false;
			grounded = true;
			numSaltos = 0;
		}
		else if (!Physics2D.OverlapCircle (groundcheckpoint.position, 0.02f, ground) && numSaltos == 2)
		{

			numSaltos = 0;
		}

		//INPUTS
		mHorizontal = Input.GetAxis("Horizontal");					//Obtiene el input de movimiento horizontal
		mVertical = Input.GetAxis("Vertical");						//Obtiene el input de movimiento vertical
		if (Input.GetButton("Jump"))
		{
			salto = true;											//Obtiene el input de salto, llama al invoke porque con GetButtonDown a veces pasaba a false antes de hacer el salto.

		}
		if (Input.GetButtonUp ("Jump")) {
			salto = false;
		}

    }

    //=======================
    void FixedUpdate()
    {


		jumpTim += Time.deltaTime;
		if (knockbackcount <= 0){  //OJO ESTE IF ES ENORME Y ENGLOBA A LO QUE EL JUGADOR PUEDE CONTROLAR DEL PERSONAJE
			//MOVIMIENTO DEL JUGADOR
			movement = rb.velocity;   	
			movement.x = mHorizontal * speed;

			//SALTO
			if (salto && grounded && numSaltos == 0)
			{
				salto = false;
				if (!GM.instance.enAgua)
					SM.instance.salto ();

				movement = new Vector2( mHorizontal * speed,jumpPower);
				GM.instance.enAire = true;
				grounded = false;
				numSaltos = 1;
				jumpTim = 0;

			}
			//DOBLE SALTO
			else if (salto && numSaltos == 1 && jumpTim > 0.2f)
			{
				salto = false;
				if (!GM.instance.enAgua)
					SM.instance.dobleSalto ();
				movement = new Vector2( mHorizontal * speed,jumpPower *0.9f);
				Debug.Log ("2salto");
				numSaltos = 2;
				jumpTim = 0;

			}



			// Para manejar la capacidad de salto
			if (Input.GetKeyDown (KeyCode.X) && GM.instance.enAire == false) {
				jumpPower = 0f;
			}
			if (Input.GetKeyUp (KeyCode.X)) {
				jumpPower = storeJumpPw;
			}

			//ESCALERAS
			if (sobreEscalera){
				if (Input.GetAxis ("Vertical") != 0) {//Moverse en eje Y al estar sobre escalera
					movement.y = mVertical * speed;
					rb.gravityScale = 0f;
				} else if (Input.GetAxis ("Vertical") == 0){
					movement.y = 0;
					}
				} else if (!sobreEscalera) {
					rb.gravityScale = gravityStore;
				}
		} else {
			if (knockfromRight) { //nuestro enemigo esta a la derecha
				rb.velocity = new Vector2 (-knockbackX, knockbackY);					
			}
			if (!knockfromRight) {
				gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (knockbackX, knockbackY);
			}
			knockbackcount -= Time.deltaTime;
		}

		//DETECTORES DE COLISION
		Vector2 position = new Vector2 (transform.position.x + 0.4f, transform.position.y + 0.6f ); //¿PARA QUE ALMACENAMOS ESTA POSICION?
		hitDer = Physics2D.Raycast (new Vector2 (transform.position.x - 0.4f, transform.position.y + 0.6f),	new Vector2 (0f, -1f), 1f, ground);
		hitIzq = Physics2D.Raycast (position, new Vector2 (0f, -1f), 1f, ground);

		//DETENER MOVIMIENTO HORIZONTAL CUANDO SE GOLPEA CONTRA PARED PARA EVITAR ENGANCHARSE A ELLAS AL SALTAR
		if (hitDer.collider != null && !hitDer.collider.isTrigger)
		{
			//Debug.Log("Jugador colisiona con algo por la derecha");
			if (mHorizontal < 0)
				movement.x = 0f;
		}
		if (hitIzq.collider != null && !hitIzq.collider.isTrigger)
		{
			//Debug.Log("Jugador colisiona con algo por la izquierda");
			if (mHorizontal > 0)
				movement.x = 0f;
		}

		rb.velocity = movement;  //(Toni) ESTA LINEA ESTA REPETIDA PERO INVERTIDA ARRIBA DEL FIXED UPDATE ¿PORQUE? Supongo que es para devolver el movimiento al jugador.


		//CONTROL DE HACIA DONDE MIRA EL JUGADOR
		if (mHorizontal < 0) {
			GetComponent<SpriteRenderer> ().flipX = false;
			GM.instance.andando = true;
		}
		else if (mHorizontal > 0) 
		{
			GetComponent<SpriteRenderer> ().flipX = true;
			GM.instance.andando = true;
		}

		// Si el jugador esta quieto avisa al game Manager, necesario para la gestion del audio

		if (mHorizontal == 0)
			GM.instance.andando = false;
   }

	// Para Que suene al caer
	void OnCollisionEnter2D(Collision2D col)
	{
		if (!grounded || rb.velocity.y < -1f)
			SM.instance.caida ("Jugador", transform.position);

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "MuerteCaja")
			GM.instance.mana = 0;
	}

	//Controla el segundo salto y pasa salto a false despues de haber saltado

}