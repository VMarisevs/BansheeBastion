
using System.Collections;
using UnityEngine;

public class AEnemy : EntityThing {

    public int damage;
    public int speed;
    public bool air;
    public int attack_range;

    private float inverseMoveTime;

    protected bool Move(int xDir, int yDir)
    {
        // defining starting and end point of movement
        Vector2 start = transform.position;
        Vector2 end = new Vector2(xDir, yDir);

        // checking if can be moved
       // boxCollider2D.enabled = false;
        //blocked = Physics2D.Linecast(start, end, blockingLayer);
     //   boxCollider2D.enabled = true;

       // if (blocked.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }

      //  return false;

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


    public void attack()
    {

    }

    public void Update()
    {
        RaycastHit2D ray = new RaycastHit2D();
        Move(1, 0);
    }
}
