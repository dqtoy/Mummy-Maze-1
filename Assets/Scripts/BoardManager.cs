using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

	public int columns = 6;
	public int rows = 6;
	public GameObject[] floorTiles;
	public GameObject[] outerWallTiles;
	public GameObject player6;
    public GameObject backdrop;
    public GameObject floor;


    public static List<Vector2> cantMoveUp = new List<Vector2>();
    public static List<Vector2> cantMoveDown = new List<Vector2>();
    public static List<Vector2> cantMoveLeft = new List<Vector2>();
    public static List<Vector2> cantMoveRight = new List<Vector2>();



    private Transform boardHolder;
	private Transform backgroudPosition;
	private List<Vector3> gridPosition = new List<Vector3> ();

	void InitialiseList()
	{
       

        /*  */

		gridPosition.Clear ();

		for (int x = 1 ; x < columns -1; x++) {
			for (int y = 1; y < rows -1; y++) {
				gridPosition.Add (new Vector3 (x, y, 0f));
			}
		}
	}

	void BoardSetup()
	{
		
		boardHolder = new GameObject ("Board").transform;

		for (int x = -1; x < columns + 1; x++) {
			for (int y = -1; y < rows + 1; y++) {
				
				GameObject toInstantiate = null;
				if (x == -1 || x == columns || y == -1 || y == rows) {
					toInstantiate = outerWallTiles[0];
				}
				if(y >= 0 && y < rows && y%2 == 0){
					if( x>= 0 && x < columns && x%2==0 )
						toInstantiate = floorTiles [0];
					else if( x>= 0 && x < columns && x%2==1)
						toInstantiate = floorTiles [1];
				}
				else if(y >= 0 && y < rows && y%2 == 1){
					if( x>= 0 && x < columns && x%2 == 0)
						toInstantiate = floorTiles [1];
					else if( x>= 0 && x < columns && x%2==1)
						toInstantiate = floorTiles [0];
				}
				GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
				instance.transform.SetParent (boardHolder);

			}
		}

		//boardHolder.transform.localScale = new Vector3(1.25f,1.25f,0f);
		//boardHolder.transform.position = new Vector3 (-1.6f, -3.52f, -10f);

	}

	void SetupWallList()
	{
		//cantMoveDown.Add(new Vector2 (5f,4f));




  //      cantMoveUp.Add(new Vector2 (5f,3f));
       
  //      cantMoveLeft.Add(new Vector2 (5f,4f));
		//cantMoveRight.Add(new Vector2 (4f,4f));


		//cantMoveLeft.Add(new Vector2 (4f,4f));
		//cantMoveRight.Add(new Vector2 (3f,4f));

		//cantMoveDown.Add(new Vector2 (2f,1f));
		//cantMoveUp.Add(new Vector2 (2f,0f));

	
	}



	public void SetupScence(){
		BoardSetup ();
       // InitialiseList ();
		//SetupPlayer (3, 3);
		SetupWallList();

	}

	Vector3 RandomPosition(){
		int randomIndex = Random.Range (0,gridPosition.Count);
		Vector3 randomPosition = gridPosition [randomIndex];
		gridPosition.RemoveAt (randomIndex);
		return randomPosition;
	}

	void SetupPlayer(int xDir,int yDir){

		Vector3 indexPlayer = new Vector3(0f,0f,0f);
		GameObject player = Instantiate (player6,indexPlayer,Quaternion.identity) as GameObject;

		player.transform.localScale = new Vector3 (1.65f, 1.65f, 0f);




	}
	// Use this for initialization
	//void Start () {
		
	//}
	
	// Update is called once per frame
	//void Update () {
		
	//}
}
