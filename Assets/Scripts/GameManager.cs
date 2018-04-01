
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	
	public float turnDelay = .1f;	
	public static GameManager instance = null;
	[HideInInspector] public bool playersTurn = true;
	[HideInInspector] public bool enemyTurn = true;
	[HideInInspector] public  List<Vector2> playerHistory = new List<Vector2>();
	[HideInInspector] public  List<Vector2> whiteHistory  = new List<Vector2>();

	public static bool playerMoving = true;

	private BoardManager boardScript;
	private List<White6> enemies;
	private bool enemiesMoving;
	private bool doingSetup = true;		

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		enemies = new List<White6>();

		boardScript = GetComponent<BoardManager>();

		InitGame();
	}

	void InitGame()
	{
		enemies.Clear ();

		boardScript.SetupScence();
	}
	public void GameOver(){
		enabled = false;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update()
	{
        
      
        //Check that playersTurn or enemiesMoving or doingSetup are not currently true.
        if (playersTurn || enemiesMoving )

			//If any of these are true, return and do not start MoveEnemies.
			return;
        //Start moving enemies.
        StartCoroutine (MoveEnemies ());

	}



	public void AddEnemyToList(White6 script)
	{
		//Add Enemy to List enemies.
		enemies.Add(script);
	}

	//Coroutine to move enemies in sequence.
	IEnumerator MoveEnemies(){
		//While enemiesMoving is true player is unable to move.
		enemiesMoving = true;
		//Wait for turnDelay seconds, defaults to .1 (100 ms).
		yield return new WaitForSeconds(turnDelay);
		//If there are no enemies spawned (IE in first level):
		if (enemies.Count == 0) {
			//Wait for turnDelay seconds between moves, replaces delay caused by enemies moving when there are none.
			yield return new WaitForSeconds(turnDelay);
		}
		//Loop through List of Enemy objects.
		for (int i = 0; i < enemies.Count; i++){
			//Call the MoveEnemy function of Enemy at index i in the enemies List.
			yield return new WaitForSeconds(enemies[i].moveTime);
			enemies[i].MoveEnemy ();
			yield return new WaitForSeconds(1f);
			enemies[i].MoveEnemy ();
			//Wait for Enemy's moveTime before moving next Enemy, 
			}
		//Once Enemies are done moving, set playersTurn to true so player can move.
		playersTurn = true;
		//Enemies are done moving, set enemiesMoving to false.
		enemiesMoving = false;

	}

}
