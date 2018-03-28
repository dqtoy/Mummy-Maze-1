using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour {

	public float moveTime = 0.1f;
	public LayerMask blockingLayer;

	private BoxCollider2D boxCollider;
	private Rigidbody2D rb2D;
	private float inverseMoveTime;

	// Use this for initialization
	protected virtual void Start () {
		boxCollider = GetComponent<BoxCollider2D> ();
		rb2D = GetComponent<Rigidbody2D> ();
		inverseMoveTime = 1f / moveTime;
	}


	protected bool Move(int xDir, int yDir,out RaycastHit2D hit){
        bool checkList = true;
		Vector2 start = transform.position;

		Vector2 end = start + new Vector2 (xDir, yDir);

	

		if (xDir == 0 && yDir == 1) {
			for (int i = 0; i < BoardManager.cantMoveUp.Count; i++) {
				if (BoardManager.cantMoveUp [i].x == start.x && BoardManager.cantMoveUp [i].y == start.y) {
					checkList = false;
				}
			}
		} else if (xDir == 0 && yDir == -1) {
			for (int i = 0; i < BoardManager.cantMoveDown.Count; i++) {
				if (BoardManager.cantMoveDown [i].x == start.x && BoardManager.cantMoveDown [i].y == start.y) {
					checkList = false;
				}
			}
		} else if (xDir == -1 && yDir == 0) {
			for (int i = 0; i < BoardManager.cantMoveLeft.Count; i++) {
				if (BoardManager.cantMoveLeft [i].x == start.x && BoardManager.cantMoveLeft [i].y == start.y) {
					checkList = false;
				}
			}
		} else if (xDir == 1 && yDir == 0) {
			for (int i = 0; i < BoardManager.cantMoveRight.Count; i++) {
				if (BoardManager.cantMoveRight [i].x == start.x && BoardManager.cantMoveRight [i].y == start.y) {
					checkList = false;
				}
			}
		}

        boxCollider.enabled = false;

		hit = Physics2D.Linecast (start, end, blockingLayer);
        if(!checkList)
        {
            return false;
        }
		boxCollider.enabled = true;

		if (hit.transform == null) {
			StartCoroutine (SmoothMovement (end));
			return true;
		}
        return false;
	}

	protected IEnumerator SmoothMovement(Vector3 end){
		
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

		while (sqrRemainingDistance > float.Epsilon) {
			
			Vector3 newPosition = Vector3.MoveTowards (rb2D.position, end, inverseMoveTime * Time.deltaTime);

			rb2D.MovePosition (newPosition);

			sqrRemainingDistance = (transform.position - end).sqrMagnitude;
			yield return null;
		}
	}

	protected virtual void AttemptMove <T> (int xDir,int yDir,out bool canMove) where T : Component{
		RaycastHit2D hit;

		canMove = Move (xDir, yDir, out hit);


		if (hit.transform == null) {
			return;
		}

		T hitCompoment = hit.transform.GetComponent<T> ();

		if (!canMove && hitCompoment != null) {
			OnCantMove (hitCompoment);
			return;
		}
		if (!canMove) {
			OnCantMove ();
			return;
		}
	}

	protected abstract void OnCantMove <T> (T Component) where T : Component;
	protected abstract void OnCantMove () ;
	// Update is called once per frame
	void Update () {

	}
		
}
