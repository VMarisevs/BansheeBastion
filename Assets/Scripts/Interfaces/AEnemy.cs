
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEnemy : EntityThing {

    public int damage;
    public float speed;
    public bool _move = true;
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
        //if (!atTheTarget(transform.position.x, transform.position.y))
        {
            nextStep = getNextStep();

            if (nextStep != Vector2.zero)
            {
                this.run(nextStep);
            }
           
            else if(!changeTarget((int)transform.position.x, (int)transform.position.y))
            {
                cantMove = true;
            }
            
            //  MapManager.pathArray[(int)transform.position.x + (int)nextStep.x, (int)transform.position.y + (int)nextStep.y] = '#';
            nextStep = Vector2.zero;

        }
        
    }

    private Vector2 getNextStep()
    {
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;

        int cost1, cost2, cost3;

        cost1 = findPath(x + 1, y, y, 0);

        cost2 = findPath(x, y - 1, y, 0);

        cost3 = findPath(x, y + 1, y, 0);

        if (cost1 > 0 || cost2 > 0 || cost3 > 0)
        {
            if (cost1 > cost2)
            {
                if (cost3 > cost2)
                    return new Vector2(0, -1);//cost2;
                else
                    return new Vector2(0, 1); //cost3
            }
            else if (cost1 > cost3)
                return new Vector2(0, 1);// cost3
            else
                return new Vector2(1, 0);// cost1
        }
            

        //if (findPath(x + 1, y, y))
        //    return new Vector2(1, 0);

        //if (findPath(x, y - 1, y))
        //    return new Vector2(0, -1);

        //if (findPath(x, y + 1, y))
        //    return new Vector2(0, 1);

        //if (findPath((int)transform.position.x-1, (int)transform.position.y))


        return Vector2.zero;

    }

    private int findPath(int x, int y, int oldY, int cost)
    {
        int newCost;
        int testCost;
        // 1 if (x,y outside maze) return false
        if (x < 0 || x >= MapManager.xCol || y < 0 || y >= MapManager.yRow)
            return -100;

        if (MapManager.mapArray[x, y] == null)
            cost ++;

        if (MapManager.mapArray[x, y] is AFriend)
            cost+=2;

        // close to goal!!!
        if (atTheTarget(x, y))
            return cost;

        newCost = findPath(x + 1, y, y, cost);

        if (y - 1 != oldY)
            newCost = (newCost < (testCost = findPath(x, y - 1, y, cost))) ? newCost  : testCost ;


        if (y + 1 != oldY)
            newCost = (newCost < (testCost = findPath(x, y + 1, y, cost))) ? newCost : testCost;   


        return newCost + cost;
    }

    public void run(Vector3 whereToGo)
    {
        /*
        To be removed
            //print("*WhereToGo in Run() x=" + whereToGo.x + " y=" + whereToGo.y +"");
            // Rigidbody2D rg = gameObject.GetComponent<Rigidbody2D>();
            //rg.MovePosition(whereToGo);
            // rg.MovePosition(this.transform.position + whereToGo);
        */
        MapManager.putCharacter(this, transform.position + whereToGo, transform.position);
       
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


        //float closest = 100;
    

        Vector2 pos = new Vector2(posX,posY);


        AFriend bestTarget = null;


        for (int x = posX; x < MapManager.xCol; x++)       
        {
            for (int y = 0; y < MapManager.yRow; y++)
            {

                if(MapManager.mapArray[x,y] is AFriend &&
                    MapManager.mapArray[x, y] != friendTarget)
                    //&& Vector2.Distance(new Vector2(x,y),pos ) < closest)
                {
                   // closest = Vector2.Distance(new Vector2(x, y), pos);
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
