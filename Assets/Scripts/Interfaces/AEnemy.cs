
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
    private int targetX, targetY;
    private Vector2 nextStep;
    //private Rigidbody2D rigibody;
    bool moveBool = true;
    bool cantMove;

    public int minCost;

    public void Start()
    {
        changeTarget((int)transform.position.x, (int)transform.position.y);
    }

    public int GetMinCost(int aX, int aY, int bX, int bY)
    {
        return Mathf.Abs(aX - bX) + Mathf.Abs(aY - bY);
    }

    private int[] findDirection(int aX, int aY, int bX, int bY)
    {

        int[] result = new int[2];


        return result;

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

        minCost = GetMinCost(x, y, (int)friendTarget.transform.position.x, (int)friendTarget.transform.position.y);
        print(minCost);

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


        //if (Mathf.Abs(aX - bX) != 0 && Mathf.Abs(aY - bY) != 0)
        //{
        //    if (Random.value >= 0.5)
        //    {
        //        result[0] = 1;
        //    }
        //    else
        //    {
        //        if (aY < bY)
        //        {
        //            result[1] = 1;


        //        }
        //        else
        //        {
        //            result[1] = -1;
        //        }

        //    }
        //}


        int newCost;
        int testCost;

        if (x < 0 || x >= MapManager.xCol || y < 0 || y >= MapManager.yRow)
            return 1000;

        if (MapManager.mapArray[x, y] is AEnemy)
            return 1000;

        if (MapManager.mapArray[x, y] == null)
            cost ++;

        if (MapManager.mapArray[x, y] is AFriend)
            cost+=3;

        // close to goal!!!
        if (atTheTarget(x, y))
            return cost;

        newCost = findPath(x + 1, y, y, cost);
        if (newCost == minCost) return newCost;

        if (y - 1 != oldY)
        {
            testCost = findPath(x, y - 1, y, cost);
            if (testCost == minCost) return testCost;
            newCost = (newCost < testCost) ? newCost : testCost;
        }


        if (y + 1 != oldY)
        {
            testCost = findPath(x, y + 1, y, cost);
            if (testCost == minCost) return testCost;
            newCost = (newCost < testCost) ? newCost : testCost;
        }

        return  cost;
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
        targetX = (int)target.transform.position.x;
        targetY = (int)target.transform.position.y;
    }


   


}
