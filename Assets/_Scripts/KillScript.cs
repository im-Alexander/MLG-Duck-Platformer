using UnityEngine;
using System.Collections;

public class KillScript : MonoBehaviour 
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player"))
		{
			Destroy (this.gameObject);
		}
	}
}
