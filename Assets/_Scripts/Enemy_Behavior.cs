using UnityEngine;
using System.Collections;

public class Enemy_Behavior : MonoBehaviour 
{
	// Private Instance Variables
	private Transform _transform;
	private Rigidbody2D _rigidbody;
	private bool _isGrounded;
	private bool _isGroundAhead;

	// Public Instance Variables
	public int health = 5;
	public float speed = 2f;
	public Transform SightStart;
	public Transform SightEnd;

	// Use this for initialization
	void Start ()
	{
		this._transform = GetComponent<Transform> ();
		this._rigidbody = GetComponent<Rigidbody2D> ();
		this._isGrounded = false;
		this._isGroundAhead = true;
	}
		
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (this._isGrounded) 
		{
			this._rigidbody.velocity = new Vector2 (this._transform.localScale.x, 0) * this.speed;

			this._isGroundAhead = Physics2D.Linecast (this.SightStart.position, this.SightEnd.position, 1 << LayerMask.NameToLayer("Solid"));
		
			//For Debug
			Debug.DrawLine(this.SightStart.position, this.SightEnd.position);

			if (this._isGroundAhead == false) 
			{
				this._flip ();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name.Contains ("Laser"))
		{ 
			Laser_Behavior laser = other.gameObject.GetComponent ("Laser_Behavior") as Laser_Behavior;
			health -= laser.damage;
			HealthLoss ();
		}
	}

	void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("Platform")) 
		{
			this._isGrounded = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Platform")) 
		{
			this._isGrounded = false;
		}
	}

	public void HealthLoss( )
	{
		if (health <= 0) 
		{
			Destroy (gameObject);
		}
	}

	private void _flip() 
	{
		if(this._transform.localScale.x == 1)
		{
			this.transform.localScale = new Vector2(-1f, 1f);
		}
		else 
		{
			this.transform.localScale = new Vector2(1f, 1f);
		}
	}
}
