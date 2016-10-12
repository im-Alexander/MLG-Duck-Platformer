using UnityEngine;
using System.Collections;

public class Laser_Behavior : MonoBehaviour 
{
	//public instance variables
	public Vector2 speed;
	public float delay;
	public int damage = 1;


	//private instance variables
	private Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () 
	{
		rigidbody = GetComponent<Rigidbody2D>();
		rigidbody.velocity = speed;

		Destroy (gameObject, delay);
	}
	
	// Update is called once per frame
	void Update () 
	{
		rigidbody.velocity = speed;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.gameObject.CompareTag("Player"))
		{
			Destroy (gameObject);
		}
	}
}
