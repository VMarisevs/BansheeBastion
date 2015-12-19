
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

public class AEnemy : EntityThing {

    public int _maxDamage;
    public float _speed;
    public bool _air;
    public int _attackRange;
    public AFriend _friendTarget;
    public bool _moveBool = true;
    //public bool _attackBool = false;

    private Vector2 nextStep;
    
    
    bool cantMove;


    public void Start()
    {
        changeTarget((int)transform.position.x, (int)transform.position.y);
    }



    public void move()
    {

        //if I am not at the target then move
        bool _inTheAttackRange = inTheAttackRange();

        if (!cantMove && !_inTheAttackRange)
        {
            nextStep = getNextStep();

            if (nextStep != Vector2.zero)
            {
                this.run(nextStep);
            }
            else
            {
                if (!changeTarget((int)transform.position.x, (int)transform.position.y))
                {
                    cantMove = true;
                }
            }
            //  MapManager.pathArray[(int)transform.position.x + (int)nextStep.x, (int)transform.position.y + (int)nextStep.y] = '#';
            nextStep = Vector2.zero;

        }
        else if (_inTheAttackRange) {
            //attackTheTarget
            print("Attack the target!!!");
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

    private bool inTheAttackRange()
    {
        int myX = (int)transform.position.x;
        int myY = (int)transform.position.y;
        int targetX = (int)_friendTarget.transform.position.x;
        int targetY = (int)_friendTarget.transform.position.y;

        int distance = MapCalc.GetMinCost(new Vector2(myX, myY), new Vector2(targetX,targetY));


        //print("myX:" + myX + " myY" + myY + " targetX:"+ targetX + " targetY:" + targetY + " Distance " + distance);
        if ( distance <= _attackRange)
            return true;

        return false;
    }
    
    private bool atTheTarget(float x, float y)
    {
        int targetX = (int)_friendTarget.transform.position.x;
        int targetY = (int)_friendTarget.transform.position.y;

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
                    MapManager.mapArray[x, y] != _friendTarget &&
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
        _friendTarget = target;
    }

    private void attackTarget()
    {
        if (_friendTarget != null)
        {
            int damage = Random.Range(0, _maxDamage);
            //
        }
    }

    //public int attackDistance()
    //{
    //    // if target is in the attack range
    //    int distance = MapCalc.GetMinCost(
    //        new Vector2(transform.position.x, transform.position.y),
    //            new Vector2( friendTarget.transform.position.x, friendTarget.transform.position.y));
    //    return distance;
    // }


}
