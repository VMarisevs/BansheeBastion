
using UnityEngine;

public class Slime : AEnemy {
    
    
    public Slime()
    {
        //health = 20 * Level;
        health = 20;
        armour = 0;
        damage = 2;
        speed = 2;
        air = true;
        attack_range = 1;
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
}
