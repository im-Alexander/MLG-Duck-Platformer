using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Game_Controller : MonoBehaviour 
{
	//Public Instance Variables
	[Header ("User Interface")]
	public Text Health;
	public Text DeathCounter;
	public Button OpenScreen;
	public Button Otions;
	public Button StartGame;

	//Private Instance Variables
	private int hp = 5;
	private int death = 0;

	// Use this for initialization
	void Start () 
	{
		Health.text = "Health: 5/5";
		DeathCounter.text = "Rekt Counter: 0";
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void DecreaseHP (int Decrease)
	{
		hp -= Decrease;
		Health.text = "HP: "+ hp +"/5";
		if (hp <= 0) 
		{
			hp = 5;
			Health.text = "Health: "+ hp +"/5";
			death += 1;
			DeathCounter.text = "Rekt Counter: "+ death;
		}
	}

	public void OptionsButton_Click ()
	{
		SceneManager.LoadScene ("Options_Screen");
	}

	public void StartButton_Click ()
	{
		SceneManager.LoadScene ("MLG-Duck-Platformer");
	}

	public void BackButton_Click ()
	{
		SceneManager.LoadScene ("Open_Screen");
	}
}
