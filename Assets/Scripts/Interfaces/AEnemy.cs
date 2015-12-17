
using System.Collections;
using UnityEngine;

public class AEnemy : EntityThing {

    public int damage;
    public int speed;
    public bool air;
    public int attack_range;
    private AFriend target = new AFriend();
    private Vector2 nextStep;
    //private Rigidbody2D rigibody;



       
    public void move()
    {
        //  this.run();
        //nextStep = new  Vector2(1, 0);//getNextStep();
        nextStep = getNextStep();

        if (nextStep != null)
        {
       
            MapManager.putCharacter(this, nextStep);
            this.run(nextStep);
        }
       

        nextStep = Vector2.zero;


    }
   
    private Vector2 getNextStep()
    {
        if (findPath((int)transform.position.x + 1, (int)transform.position.y))
            return new Vector2(1, 0);
        if (findPath((int)transform.position.x, (int)transform.position.y + 1))
            return new Vector2(0, 1);
        if (findPath((int)transform.position.x, (int)transform.position.y - 1))
            return new Vector2(0, -1);

        return Vector2.zero;
        /*
        if (findPath((int)transform.position.x + 1, (int)transform.position.y))
        {
            print(new Vector2((int)transform.position.x + 1, (int)transform.position.y));
            return new Vector2((int)transform.position.x + 1, (int)transform.position.y);
        }
        else if (findPath((int)transform.position.x, (int)transform.position.y + 1))
        {
            print(new Vector2((int)transform.position.x, (int)transform.position.y +1 ));
            return new Vector2((int)transform.position.x , (int)transform.position.y +1 );
        }
        else if (findPath((int)transform.position.x, (int)transform.position.y - 1))
        {
            print(new Vector2((int)transform.position.x, (int)transform.position.y -1));
            return new Vector2((int)transform.position.x, (int)transform.position.y -1);
        }

        return Vector2.zero;
        */
    }

    private bool findPath(int x, int y)
    {
        if (x < 0 || x >= MapManager.rows || y < 0 || y >= MapManager.columns)
            return false;

        if (x == 7 && y == 6)
            return true;
       // print("x=" + x + "y=" + y);
        if (MapManager.pathArray[x, y] != 0)
            return false;
        
        // marking
        MapManager.pathArray[x, y] = 2;

        if (findPath(x + 1, y))
            return true;
        if (findPath(x, y + 1))
            return true;
        if (findPath(x, y - 1))
            return true;

        MapManager.pathArray[x, y] = 3;

        return false;

        /*
        //bool result = false;
        //4,4 is the target
        if ((x < 0   || x >= 4) ||
                (y >= MapManager.rows || y < 0))
            return false;

        //if (MapManager.mapArray[x, y] == target)
        if (x ==4 && y== 4)
            return true;

        if (MapManager.mapArray[x, y] != null)
            return false;

        //// marking as path
        if (MapManager.pathArray[x, y] == 0)
        {
            MapManager.pathArray[x, y] = 2;
            if (nextStep == null)
            {
                nextStep = new Vector2(x, y);
            }
        }

        // forward
        if (findPath(x+1, y))
        {
            return true;
        }

        if (findPath(x , y +1))
        {
            return true;
        }

        if (findPath(x , y -1))
        {
            return true;
        }

        // marking as path
        if (MapManager.pathArray[x, y] == 0)
        {
            MapManager.pathArray[x, y] = 3;
        }

        return false;*/
    }

    public void run(Vector3 whereToGo)
    {
        Rigidbody2D rg = gameObject.GetComponent<Rigidbody2D>();
        //rg.MovePosition(whereToGo);
        rg.MovePosition(this.transform.position + whereToGo);
    }

    
}
