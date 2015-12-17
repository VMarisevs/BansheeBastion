
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
        
        //print("Char at [8,0] = " + MapManager.pathArray[8, 0]);
        nextStep = getNextStep();

        if (nextStep != Vector2.zero)
        {
            MapManager.displayPathMap();
            //print("x=" + nextStep.x + " y=" + nextStep.y);
            //MapManager.pathArray[(int)nextStep.x, (int)nextStep.y] = 1;
            //MapManager.putCharacter(this, nextStep);
            //this.run(nextStep);
            // MapManager.pathArray[oldx, oldy] = 0;
            //MapManager.displayPathMap();
        }

        
        nextStep = Vector2.zero;
        

    }
   
    private Vector2 getNextStep()
    {
        if (findPath((int)transform.position.x , (int)transform.position.y - 1))
            return new Vector2(0, -1);
       if (findPath((int)transform.position.x + 1, (int)transform.position.y))
            return new Vector2(1, 0);
       if (findPath((int)transform.position.x, (int)transform.position.y + 1))
            return new Vector2(0, 1);
       //if (findPath((int)transform.position.x-1, (int)transform.position.y))
       //     return new Vector2(-1, 0);

        return Vector2.zero;
    
    }

    private bool findPath(int x, int y)
    {
        //print("x=" + x + " y=" + y);
        // 1 if (x,y outside maze) return false
        if (x < 0 || x >= MapManager.xCol || y < 0 || y >= MapManager.yRow)
            return false;

        // 2 if (x,y is goal) return true
        if (x == 10 && y == 4)
            return true;

        // 3 if (x,y not open) return false
        if (MapManager.pathArray[x, y] != '.')
            return false;

        // 4 mark x,y as part of solution path
        MapManager.pathArray[x, y] = '+';

        if (findPath(x, y -1))
            return true;
        if (findPath(x + 1, y))
            return true;
        if (findPath(x, y + 1))
            return true;
       // if (findPath(x - 1, y ))
       //    return true;

        // unmark x,y as part of solution path
        MapManager.pathArray[x, y] = 'x';

        return false;
    }

    public void run(Vector3 whereToGo)
    {
        //print("*WhereToGo in Run() x=" + whereToGo.x + " y=" + whereToGo.y +"");
        Rigidbody2D rg = gameObject.GetComponent<Rigidbody2D>();
        //rg.MovePosition(whereToGo);
        rg.MovePosition(this.transform.position + whereToGo);
    }

    
    

}
