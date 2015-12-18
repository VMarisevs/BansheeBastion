
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEnemy : EntityThing {

    public int damage;
    public int speed;
    public bool air;
    public int attack_range;
    public AFriend friendTarget;
    private Vector2 nextStep;
    //private Rigidbody2D rigibody;
    bool moveBool = true;
    bool cantMove;


    public void Start()
    {
        changeTarget((int)transform.position.x, (int)transform.position.y);
    }



    public void move()
    {
    
        //if I am not at the target then move
        if (!cantMove && !atTheTarget(transform.position.x, transform.position.y))
        {
            nextStep = getNextStep();

            if (nextStep != Vector2.zero)
            {
                this.run(nextStep);
            }
            else
            {        
                if(!changeTarget((int)transform.position.x, (int)transform.position.y))
                {
                    cantMove = true;
                }
            }
            //  MapManager.pathArray[(int)transform.position.x + (int)nextStep.x, (int)transform.position.y + (int)nextStep.y] = '#';
            nextStep = Vector2.zero;

        }
        
    }

    private Vector2 getNextStep()
    {
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;

        if (findPath(x + 1, y, y))
            return new Vector2(1, 0);

        if (findPath(x, y - 1, y))
            return new Vector2(0, -1);

        if (findPath(x, y + 1, y))
            return new Vector2(0, 1);

        //if (findPath((int)transform.position.x-1, (int)transform.position.y))


        return Vector2.zero;

    }

    private bool findPath(int x, int y, int oldY)
    {

        // 1 if (x,y outside maze) return false
        if (x < 0 || x >= MapManager.xCol || y < 0 || y >= MapManager.yRow)
            return false;

        if (MapManager.mapArray[x, y] != null)          
            return false;
       
        // close to goal!!!
        if (atTheTarget(x, y))
            return true;

        if (findPath(x + 1, y, y))
            return true;

        if (y - 1 != oldY && findPath(x, y - 1,  y))
            return true;

        if (y + 1 != oldY && findPath(x, y + 1, y))
            return true;


        return false;
    }

    public void run(Vector3 whereToGo)
    {     
        //print("*WhereToGo in Run() x=" + whereToGo.x + " y=" + whereToGo.y +"");
        Rigidbody2D rg = gameObject.GetComponent<Rigidbody2D>();
        //rg.MovePosition(whereToGo);

        MapManager.putCharacter(this, transform.position + whereToGo , transform.position);
        rg.MovePosition(this.transform.position + whereToGo);
    }


    
    private bool atTheTarget(float x, float y)
    {
        int targetX = (int)friendTarget.transform.position.x;
        int targetY = (int)friendTarget.transform.position.y;

        if  ((x == targetX - 1  && y == targetY )   ||
             (x == targetX + 1  && y == targetY )   ||
             (x == targetX      && y == targetY + 1)||
             (x == targetX      && y == targetY - 1))
            return true;

        return false;
    }

    private bool changeTarget(int posX,int posY)
    {
        float closest = 100;
    

        Vector2 pos = new Vector2(posX,posY);
        AFriend bestTarget = null;
        for (int x = posX; x < MapManager.xCol; x++)       
        {
            for (int y = 0; y < MapManager.yRow; y++)
            {
                if(MapManager.mapArray[x,y] is AFriend &&
                    MapManager.mapArray[x, y] != friendTarget &&
                    Vector2.Distance(new Vector2(x,y),pos ) < closest)
                {
                    closest = Vector2.Distance(new Vector2(x, y), pos);
                    bestTarget = (AFriend)MapManager.mapArray[x, y];
                }
            }
        }

        if (bestTarget != null)
        {
            setTarget(bestTarget);
            return true;
        }
        return false;
            


    }

    public void setTarget(AFriend target)
    {
        friendTarget = target;
    }


   


}
