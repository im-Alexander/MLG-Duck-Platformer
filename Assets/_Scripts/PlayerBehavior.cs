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

	//public instance variables
	public float Velocity = 10f;
	public float AirVelocity = 5f;
	public float JumpForce = 100f;
	public Camera camera;
	public Transform SpawnPoint;
	public Animator anim;

	// Use this for initialization
	void Start () 
	{
		this._initalize();
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
				this._jump = 1f;
			}
			if(Input.GetKeyDown(KeyCode.Comma))
			{
				this._jump = 1f;
			}

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
		if (other.gameObject.CompareTag ("Death"))
		{
			this._transform.position = this.SpawnPoint.position;
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
}
