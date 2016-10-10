using UnityEngine;
using System.Collections;

public class SpawnBehavior : MonoBehaviour 
{
	//Private Instance Variables

	private Transform _transform;
	private GameObject SpawnPoint;

	// Use this for initialization
	void Start () {
		this._transform = GetComponent<Transform> ();
		this.SpawnPoint = GameObject.FindWithTag("SpawnPoint");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player"))
		{
			this.SpawnPoint.transform.position = this._transform.position;
		}
	}
}
