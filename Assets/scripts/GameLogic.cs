/// <summary>
/// Game logic.
/// Manages the whole logic of the game, moves sprites and keeps scores, hps, mps, and winning and loosing the game.
/// </summary>
using UnityEngine;
using System.Collections;


public class GameLogic : TouchLogic {

	// ************************PUBLIC VARIABLES
		public Transform	ninja;
		// All the transforms of the different types of monsters in the game
		public Transform 	dracula;

		// Text for debugging the windows phone 8.
		public GUIText 		debug;

	// ************************PRIVATE VARIABLES 

		// Keeps a list of all the monsters currently in the gamefield
		private ArrayList monsters;

		// Saves a list of indexes of monsters to attack whenever the monster select slots are depleted or the touch ends
		private ArrayList monstersToAttack;

		// Time that the monsters take to respawn
		private float monstersSpawnTime=5;

		// Time elapsed since last spawn monster
		private float monstersSpawnTimeElapsed=0;

	/// <summary>
	/// Initializes the game.
	/// </summary>
	void Start () {

		// Start the list of monsters that will exist in the playing field 
		monsters = new ArrayList();
		monstersToAttack = new ArrayList();
		// When instantiating a Transform unity needs to keep the instantiated object in a variable to access its components
		ninja = (Transform)Instantiate (ninja, new Vector2(0,0),Quaternion.identity);

		// Instantiate the first monster to start the game
		SpawnMonster();

	}


	//***************************************************************UPDATE FUNCTIONALITY**************************************************//
	// Update is called once per frame
	void Update () {

		// See if the player has touched or sent any input to the game.
		VerifyInputs();

		// Every frame the game checks to see if a new monster should be spawn
		monstersSpawnTimeElapsed+=Time.deltaTime;
		if(monstersSpawnTimeElapsed>monstersSpawnTime)
		{
			monstersSpawnTimeElapsed=0;
			SpawnMonster();
		}


	}



	//*****************************************************Touch Inputs Controls*************************************************//

	public override void OnTouchBeganAnywhere()
	{
		debug.text="Beggining";
		VerifyMonstersTouched(currentTouch.position);
	}

	public override void OnTouchMovedAnywhere()
	{
		debug.text="Moving";
		VerifyMonstersTouched(currentTouch.position);
	}

	public override void OnTouchStayedAnywhere()
	{
		debug.text="Staying";
		VerifyMonstersTouched(currentTouch.position);
	}

	public override void OnTouchEndedAnywhere()
	{
		debug.text="Ending: looking for monsters to attack: total "+ monstersToAttack.Count;
		for(int i=0; i<monstersToAttack.Count; i++)
		{

			int monsterIndex = (int)monstersToAttack[i];
			Transform monsterToHit = (Transform)monsters[monsterIndex];

			debug.text+="\nWent in for loop checking moster number: "+ monsterIndex;

			// Get the ninja's strength to hit the monsters 
			int strengthOfPlayer = ninja.GetComponent<Ninja>().strength;

			// Hit the current monster with the ninja's strength
			monsterToHit.GetComponent<Monster>().changeHP (-strengthOfPlayer);

			debug.text+="\nRemaining HP"+monsterToHit.GetComponent<Monster>().hp;

			if(monsterToHit.GetComponent<Monster>().hp<=0)
			{
				monsterToHit.GetComponent<Monster>().Die();
				monsters.RemoveAt (monsterIndex);
			}
		}
		monstersToAttack.Clear();
		debug.text+="\n List got cleared";
	}
	

	//*****************************************************HELPING FUNCTIONS************************************************************//

	// Sends a signal to all the functions that verify the possible inputs (buttons, touches, mouse) in the game
	private void VerifyInputs()
	{
		CheckTouches();// This method is inside TouchLogic.cs class the father of this class
		VerifyMouseButtons();
		VerifyOtherButtons ();
	}

	// Checks to see if any mouse button has been pressed
	private void VerifyMouseButtons()
	{
		if(Input.GetMouseButtonDown(0))
		{
			VerifyMonstersTouched(Input.mousePosition);
			if(monsters.Count>0)
			{
				Debug.Log ("Entered "+((Transform)monsters[0]).GetComponent<Dracula>().mp);
				((Transform)monsters[0]).GetComponent<Dracula>().SpecialAttack(5);
			}
		}
		
		//Simulate the on ending touch method using the mouse
		if(Input.GetMouseButtonUp(0))
		{
			OnTouchEndedAnywhere();
		}
	}


	/// <summary>
	/// Verifyies some other buttons like the back button in a phone or the escape button in a pc
	/// </summary>
	private void VerifyOtherButtons()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			// debug.text="It entered the application quit method";
			Application.Quit();
		}
	}


	// Spawns a new monster to the world using some random coordinates around the main ninja player
	// This function should also take the new monster from a pool of available monsters in a random way.
	private void SpawnMonster()
	{
		// Spawn the monster on a random place inside the game world
		int radius = 4;
		float randomAngle= Random.Range(0,360);

		// Use a circle function to make the monster appear around the ninja
		float monsterStartX = (Mathf.Cos(randomAngle)*radius);
		float monsterStartY = (Mathf.Sin(randomAngle)*radius);
		//Debug.Log(monsterStartX+" "+monsterStartY);

		// Instantiate the new monster in the random coordinates
		Transform tempMonster = (Transform)Instantiate(dracula,new Vector2(monsterStartX,monsterStartY),Quaternion.identity);
		
		// The monsters need a collider to be detectable by the touch rays casted into the game
		tempMonster.gameObject.AddComponent<BoxCollider>();

		// When instantiating a Transform unity needs to keep the instantiated object in a variable to access its components
		monsters.Add(tempMonster);
	}





	// Iterate through all the monsters to see if the player has touched/clicked at least one of them
	private void VerifyMonstersTouched(Vector3 toucher)
	{
//		debug.text+="\nVerifying monsters touched \ncurrentTouch: x: "+currentTouch.position.x+" y: "+currentTouch.position.y;
//		debug.text="Monsters Count: "+monsters.Count;
		for(int i = 0; i< monsters.Count; i++)
		{
			Transform tempMonster =  (Transform)monsters[i];

			// Create a ray from the touch position in the screen and send it towards the game to see if it
			// hits any game object, especially monsters.
			Ray rayTouch=Camera.main.ScreenPointToRay(toucher);
			RaycastHit rayHit = new RaycastHit();

			if (Physics.Raycast (rayTouch, out rayHit)) 
			{
				GameObject monsterHit = rayHit.collider.gameObject;


				// If the object touched is one of the monsters in the game and that monster haven't been added to the monsters to attack list
					// add it to the monstersToAttack list.
				if(monsterHit.transform.Equals(tempMonster) && !monstersToAttack.Contains (i))
				{
					// Add the touched monster to the list of monsters to attack next
					monstersToAttack.Add(i);

				}
			}
		}
	}


	private bool ElementWeakness(char attackingElement, char defendingElement)
	{
		switch(attackingElement)
		{
		case 'f':
			if(defendingElement=='v')
				return true;
			break;
		case 'a':
			if(defendingElement=='f')
				return true;
			break;
		case 't':
			if(defendingElement=='a')
				return true;
			break;
		case 'v':
			if(defendingElement=='t')
			   	return true;
			break;
		}

		return false;

	}

}
