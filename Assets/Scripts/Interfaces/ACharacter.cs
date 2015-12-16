using UnityEngine;
using System.Collections;

public abstract class ACharacter : MonoBehaviour {

    // move speed
    public float moveTime = 0.1F;

    public Vector2 position = new Vector2(-1, -1);

    // blocking layer mask - to make units visible
    public LayerMask blockingLayer;

    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rigidbody2D;

    private float inverseMoveTime;

    void Start () {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
    }


    protected bool Move(int xDir, int yDir, out RaycastHit2D blocked)
    {
        // defining starting and end point of movement
        Vector2 start = transform.position;
        Vector2 end = new Vector2(xDir, yDir);

        // checking if can be moved
        boxCollider2D.enabled = false;
        blocked = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider2D.enabled = true;

        if (blocked.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }

        return false;

    }


    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rigidbody2D.position, end, inverseMoveTime * Time.deltaTime);
            rigidbody2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
    }


    protected virtual void AttemptMove<T>(int xDir, int yDir)
        where T : Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);

        if (hit.transform == null)
            return;

        T hitComponent = hit.transform.GetComponent<T>();
        if (!canMove && hitComponent != null)
            onCantMove(hitComponent);
    }

    protected abstract void onCantMove<T>(T component)
        where T : Component;

}
