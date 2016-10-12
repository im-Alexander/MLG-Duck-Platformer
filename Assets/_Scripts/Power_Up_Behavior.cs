using UnityEngine;
using System.Collections;

public class Power_Up_Behavior : MonoBehaviour {

//Public Instance variables

public AudioClip PowerUpSound;
public AudioClip CheckpointSound;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("PowerUp"))
		{
			GetComponent<AudioSource>().PlayOneShot(PowerUpSound);
		}

		if (other.gameObject.CompareTag ("Checkpoint"))
		{
			GetComponent<AudioSource>().PlayOneShot(CheckpointSound);
		}
	}
}
