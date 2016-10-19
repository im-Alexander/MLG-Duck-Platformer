using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour 
{
	//private instance variables
	private Transform _transform;
	private Rigidbody2D _rigidbody;
	private float _move;
	private float _jump;
	private bool _isFacingRight;
	private bool _isGrounded;
	private Transform firePos;
	private Game_Controller controller;
	private bool _isJumpable;

	//public instance variables
	[Header ("Player Mechanics")]
	public float Velocity = 10f;
	public float AirVelocity = 5f;
	public float JumpForce = 100f;
	public int HP = 5;
	public Transform SightStart;
	public Transform SightEnd;

	[Header ("Camera")]
	public Camera camera;

	[Header ("Spawning")]
	public Transform SpawnPoint;

	[Header ("Animation and sound")]
	public Animator anim;
	public AudioClip JumpSound;
	public AudioClip LaserFire;
	public AudioClip DeathSound;

	[Header ("Laser")]
	public GameObject DuckLaserLeft; 
	public GameObject DuckLaserRight;

	// Use this for initialization
	void Start () 
	{
		this._initalize();

		//To link to the game controller
		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent <Game_Controller>();

		//To Update the interface when the game starts
		controller.DecreaseHP(0);

		this._isJumpable = false;
	}
	
	// Update is called once per frame (physics)
	void FixedUpdate () 
	{
		anim.SetInteger("State", 2); //Jumping Animation

		if (this._isGrounded)
		{

			this._move = Input.GetAxis("Horizontal");
			if (this._move > 0f)
			{
				this._move = 1f;
				this._isFacingRight = true;
				this._flip ();
				anim.SetInteger("State", 1); //Running Animation
			}
			else if (this._move < 0f)
			{
				this._move = -1f;
				this._isFacingRight = false;
				this._flip ();
				anim.SetInteger("State", 1); //Running Animation
			}
			else
			{
				this._move = 0f;
				anim.SetInteger("State", 0); //Idle Animation
			}
			
			//Check for keydown
			if(Input.GetKeyDown(KeyCode.Z))
			{
				this._isJumpable = Physics2D.Linecast (this.SightStart.position, this.SightEnd.position, 1 << LayerMask.NameToLayer("Solid"));
				if (this._isJumpable == true) 
				{
					this._jump = 1f;
					GetComponent<AudioSource> ().PlayOneShot (JumpSound);
				}
			}
			if(Input.GetKeyDown(KeyCode.Comma))
			{
				this._isJumpable = Physics2D.Linecast (this.SightStart.position, this.SightEnd.position, 1 << LayerMask.NameToLayer("Solid"));
				if (this._isJumpable == true) 
				{
					this._jump = 1f;
					GetComponent<AudioSource> ().PlayOneShot (JumpSound);
				}
			}

			//For Debug
			Debug.DrawLine(this.SightStart.position, this.SightEnd.position);

			this._rigidbody.AddForce(new Vector2(this._move * this.Velocity, this._jump * this.JumpForce), ForceMode2D.Force);
		}
		else
		{
			this._jump = 0f;

			this._move = Input.GetAxis("Horizontal");
			if (this._move > 0f)
			{
				this._move = 1f;
				this._isFacingRight = true;
				this._flip ();
			}
			else if (this._move < 0f)
			{
				this._move = -1f;
				this._isFacingRight = false;
				this._flip ();
			}
			else
			{
				this._move = 0f;
			}
			this._rigidbody.AddForce(new Vector2(this._move * this.AirVelocity, 0f), ForceMode2D.Force);
		}

		this.camera.transform.position = new Vector3(this._transform.position.x, this._transform.position.y, -10f);

		if (Input.GetKeyDown (KeyCode.Period)) 
		{
			GetComponent<AudioSource> ().PlayOneShot (LaserFire);
			Fire ();
		}

		if (Input.GetKeyDown (KeyCode.X)) 
		{
			GetComponent<AudioSource> ().PlayOneShot (LaserFire);
			Fire ();
		}
	}

	//Private Functions
	private void _initalize()
	{
		this._transform = GetComponent<Transform> ();
		this._rigidbody = GetComponent<Rigidbody2D> ();
		this._move = 0;
		this._isFacingRight = true;
		this._isGrounded = false;

		//For animation
		this.anim = GetComponent<Animator> ();

		//For fire Position - finds the fire position
		this.firePos = transform.FindChild("FirePos");

	}

	private void _flip() 
	{
		if(this._isFacingRight)
		{
			this.transform.localScale = new Vector2(1f, 1f);
		}
		else 
		{
			this.transform.localScale = new Vector2(-1f, 1f);
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		// This if statement is to -> die -> Play the death sound -> respawn -> reset the user interface when a death barrier has been hit.
		if (other.gameObject.CompareTag ("Death"))
		{
			controller.DecreaseHP(5);
			GetComponent<AudioSource> ().PlayOneShot (DeathSound);
			this._transform.position = this.SpawnPoint.position;
			HP = 5;
			controller.DecreaseHP(0);
		}

		if (other.gameObject.CompareTag ("Enemy")) 
		{
			HP--;
			controller.DecreaseHP(1);
			if (HP <= 0) 
			{
				this._transform.position = this.SpawnPoint.position;
				HP = 5;
				//To reset interface on respawn
				controller.DecreaseHP(0);
				GetComponent<AudioSource> ().PlayOneShot (DeathSound);
			}
		}
	}

	private void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Platform"))
		{
			this._isGrounded = true;
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		this._isGrounded = false;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("PowerUp")) 
		{
			if (HP < 5) 
			{
				HP++;
				controller.DecreaseHP(-1);
			}
		}
	}

	private void Fire()
	{
		if (_isFacingRight)
			Instantiate (DuckLaserRight, firePos.position, Quaternion.identity);
		
		if (!_isFacingRight)
			Instantiate (DuckLaserLeft, firePos.position, Quaternion.identity);
	}
}
