
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;
using System;

public class AEnemy : EntityThing {

    public int _maxDamage;
    public bool _air;
    public int _attackRange;
	
    public AFriend _friendTarget;

    private Vector2 nextStep;

    public void move()
    {
        bool _haveAtarget = haveATarget();
        //print("do I have a target" + _haveAtarget);
        if (_haveAtarget)
        {
            bool _inTheAttackRange = inTheAttackRange();

            // move to a target
            if (!_inTheAttackRange)
            {
                nextStep = getNextStep();

                if (nextStep != Vector2.zero)
                {
                    this.run(nextStep);
                }
            }
            // attack the target if close
            else if (_inTheAttackRange)
            {
                attackTarget();
            }
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
        
        Rigidbody2D rg = gameObject.GetComponent<Rigidbody2D>();

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

    private bool haveATarget()
    {
        bool result = false;

        if (LevelManager.friendsSpawned.Contains(_friendTarget))
        {
            return true;
        }

        result = changeTarget();
        

        return result;
    }

    private bool changeTarget()
    {
        int bestTarget = -1; // means no target
        int bestScore = 1000;
        int myX = (int)transform.position.x;
        int myY = (int)transform.position.y;
        Vector2 myPosition = new Vector2(myX, myY);

        // if there is atleast 1 friend, then checking and returning index
        if (!(LevelManager.friendsSpawned.Count < 1))
        {
            //print("trying to change target");
            // looking for best target
            for (int i = 0; i < LevelManager.friendsSpawned.Count; i++)
            {

                AFriend potentialTarget = LevelManager.friendsSpawned[i];

                Vector2 potentialTargetPosition = new Vector2(
                                                                (int)potentialTarget.transform.position.x,
                                                                    (int)potentialTarget.transform.position.y);

                int score = MapCalc.GetMinCost(myPosition, potentialTargetPosition);

                if (score < bestScore)
                {
                    bestScore = score;
                    bestTarget = i;
                }
            }

            // better to add try catch, cause some of the enemies might try to swap into unexisting char
            try
            {
                _friendTarget = LevelManager.friendsSpawned[bestTarget];
            } catch (Exception e)
            {
                print("ERROR!" + e.StackTrace);
            }
            

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
            _friendTarget.hit(damage);
        }
    }

/*
    public methods
*/    
    public void hit(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            // print("I am dead!");
            LevelManager.enemiesSpawned.Remove(this);
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }
    
}
