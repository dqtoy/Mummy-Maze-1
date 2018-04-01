using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class White6 : MovingObject {
	
	private Animator animator;							
	private Transform target;						
	private bool skipMove;					
	private Vector2 temp;



	protected override void Start ()
	{
		GameManager.instance.AddEnemyToList (this);

		//Get and store a reference to the attached Animator component.
		animator = GetComponent<Animator> ();


		target = GameObject.FindGameObjectWithTag ("Player").transform;

		base.Start ();
	}
		


	protected override void AttemptMove <T> (int xDir,int yDir, out bool checkMove){
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir, yDir);

	
		base.AttemptMove <T> (xDir, yDir, out checkMove);

		if (checkMove) {
			if (xDir == 0 && yDir == 1) {
				animator.SetTrigger ("white6UP");
			} else if (xDir == 0 && yDir == -1) {
				animator.SetTrigger ("white6Down");
			} else if (xDir == -1 && yDir == 0) {
				animator.SetTrigger ("white6Left");
			} else if (xDir == 1 && yDir == 0) {
				animator.SetTrigger ("white6Right");
			}
				

			GameManager.instance.whiteHistory.Add (end);
		}
	}



	public void MoveEnemy ()
	{


		int xDir = 0;
		int yDir = 0;

		if(Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)
			yDir = target.position.y > transform.position.y ? 1 : -1;
		else
			xDir = target.position.x > transform.position.x ? 1 : -1;
		
		bool check;
		AttemptMove <Player6> (xDir, yDir,out check);

		xDir = 0;
		yDir = 0;
		if (!check) {
			if(Mathf.Abs (target.position.y - transform.position.y) < float.Epsilon)
				xDir = target.position.x > transform.position.x ? 1 : -1;
			else
				yDir = target.position.y > transform.position.y ? 1 : -1;
			AttemptMove <Player6> (xDir, yDir,out check);
		}
	}



	protected override void OnCantMove <T> (T component)
	{



	}
	protected override void OnCantMove (){
	}


}