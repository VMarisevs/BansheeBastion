
using System.Collections;
using System.Collections.Generic;
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
        // update my position
       // int oldx = (int)transform.position.x;
       // int oldy = (int)transform.position.y;
       // MapManager.pathArray[oldx, oldy] = 8;

        nextStep = getNextStep();

        if (nextStep != Vector2.zero)
        {
            
            //print("x=" + nextStep.x + " y=" + nextStep.y);
            //MapManager.pathArray[(int)nextStep.x, (int)nextStep.y] = 1;
            MapManager.putCharacter(this, nextStep);
            this.run(nextStep);
            // MapManager.pathArray[oldx, oldy] = 0;
            MapManager.displayPathMap();
        }

        
        nextStep = Vector2.zero;
        

    }
   
    private Vector2 getNextStep()
    {
        if (findPath((int)transform.position.x + 1, (int)transform.position.y))
            return new Vector2(1, 0);
        else if (findPath((int)transform.position.x, (int)transform.position.y + 1))
            return new Vector2(0, 1);
        else if (findPath((int)transform.position.x, (int)transform.position.y - 1))
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
    /*
     map path: 
        -2 unchecked
        -1 marked
        -0 empty
        1 somebody there
        8 me
    */
    private bool findPath(int x, int y)
    {
        if (x < 0 || x >= MapManager.xCol || y < 0 || y >= MapManager.yRow)
            return false;

        if (x == 10 && y == 4)
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
        //print("*WhereToGo in Run() x=" + whereToGo.x + " y=" + whereToGo.y +"");
        Rigidbody2D rg = gameObject.GetComponent<Rigidbody2D>();
        //rg.MovePosition(whereToGo);
        rg.MovePosition(this.transform.position + whereToGo);
    }

    
    

}
