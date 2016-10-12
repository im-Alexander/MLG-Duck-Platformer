using UnityEngine;
using System.Collections;

public class Enemy_Behavior : MonoBehaviour 
{
	// Public Instance Variables
	public int health = 5;

	// Use this for initialization
	void Start ()
	{
		
	}
		
	// Update is called once per frame
	void Update () 
	{
		
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

	public void HealthLoss( )
	{
		if (health <= 0) 
		{
			Destroy (gameObject);
		}
	}
}
