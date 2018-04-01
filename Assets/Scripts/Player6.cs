using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player6 : MovingObject {

	private Animator animator;
	private Vector2 temp;

	// Use this for initialization
	protected override void Start(){
		
		animator = GetComponent<Animator> ();
		temp = this.transform.position;
		GameManager.instance.playerHistory.Add (temp);
		base.Start ();
	}

	// Update is called once per frame
	private void Update () {
		if (!GameManager.instance.playersTurn)
			return;


		int horizontal = 0;
		int vertical = 0;
	
		RaycastHit2D hit;
		horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
		vertical = (int) (Input.GetAxisRaw ("Vertical"));

		bool check;
		if (horizontal != 0)
			vertical = 0;
		if(horizontal != 0 || vertical != 0)
		{
			//Call AttemptMove passing in the generic parameter Wall, since that is what Player may interact with if they encounter one (by attacking it)
			//Pass in horizontal and vertical as parameters to specify the direction to move Player in.
			AttemptMove<White6> (horizontal, vertical,out check);
		}
	}
	protected override void AttemptMove <T> (int xDir,int yDir,out bool check){

	

		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir, yDir);

		base.AttemptMove <T> (xDir, yDir,out check);

		if (check) {
			if (xDir == 0 && yDir == 1) {
				animator.SetTrigger ("player6Up");
			} else if (xDir == 0 && yDir == -1) {
				animator.SetTrigger ("player6Down");
			} else if (xDir == -1 && yDir == 0) {
				animator.SetTrigger ("player6Left");
			} else if (xDir == 1 && yDir == 0) {
				animator.SetTrigger ("player6Right");
			}
			GameManager.instance.playerHistory.Add (end);
		} 

		//RaycastHit2D hit;
		//CheckIfGameOver ();
		GameManager.instance.playersTurn = false;
		GameManager.playerMoving = false;

	}
		

	protected override void OnCantMove <T> (T Component){
        GameManager.instance.playersTurn = true;

    }

    protected override void OnCantMove(){
		GameManager.instance.playersTurn = true;
	}

	private void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Enemy") {
			other.gameObject.SetActive (false);
			animator.SetTrigger ("player6Died");
		}

	}
}
