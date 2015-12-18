
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEnemy : EntityThing {

    public int damage;
    public int speed;
    public bool air;
    public int attack_range;
    private AFriend friendTarget;
    private Vector2 nextStep;
    //private Rigidbody2D rigibody;
    bool moveBool = true;

    public AEnemy()
    {
        changeTarget();
    }

    public void move()
    {

        //if I am not at the target then move
        if (!atTheTarget(transform.position.x, transform.position.y))
        {
            nextStep = getNextStep();

            if (nextStep != Vector2.zero)
            {
                //removing my self from map
                MapManager.pathArray[(int)transform.position.x, (int)transform.position.y] = '.';
                // placing in a new position to regenerate new path for my self
                MapManager.pathArray[(int)transform.position.x + (int)nextStep.x, (int)transform.position.y + (int)nextStep.y] = 's';

                this.run(nextStep);
               
                // just displaying the path algorithm
                //MapManager.displayPathMap();

                // clearing the path, otherwise it will do just 1 step
                MapManager.clearPathFromPath();

            }

            MapManager.pathArray[(int)transform.position.x + (int)nextStep.x, (int)transform.position.y + (int)nextStep.y] = '#';
            nextStep = Vector2.zero;

        }
        else
        {
            changeTarget();
        }
    }
   
    private Vector2 getNextStep()
    {
        if (findPath((int)transform.position.x + 1, (int)transform.position.y))
            return new Vector2(1, 0);

        if (findPath((int)transform.position.x , (int)transform.position.y - 1))
            return new Vector2(0, -1);
       
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
        // close to goal!!!
        if  (atTheTarget(x,y))            
            return true;

        // 3 if (x,y not open) return false
        if (MapManager.pathArray[x, y] != '.')
            return false;

        // 4 mark x,y as part of solution path
        MapManager.pathArray[x, y] = '+';

        if (findPath(x + 1, y))
            return true;

        if (findPath(x, y -1))
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

    private bool atTheTarget(float x, float y)
    {
        float targetX = friendTarget.transform.position.x;
        float targetY = friendTarget.transform.position.y;

        //float targetX = 10;
        //float targetY = 4;

        if ((x == targetX - 1   && y == targetY     && MapManager.pathArray[(int)targetX - 1, (int)targetY] == '.') ||
             (x == targetX + 1  && y == targetY     && MapManager.pathArray[(int)targetX + 1, (int)targetY] == '.') ||
             (x == targetX      && y == targetY + 1 && MapManager.pathArray[(int)targetX, (int)targetY + 1] == '.') ||
             (x == targetX      && y == targetY - 1 && MapManager.pathArray[(int)targetX, (int)targetY - 1] == '.'))
            return true;
        //else
        //{
            // switch the target
            
        //}

        return false;
    }

    private void changeTarget()
    {
        int index = LevelManager.friendsSpawned.IndexOf(friendTarget);
       // int indexOrig = LevelManager.friendsSpawned.IndexOf(target);
        print("Target index:" + index);

        //if (target == null && LevelManager.friendsSpawned.Count > 0)
        //{
        //    target = LevelManager.friendsSpawned[0];
        //}
        //else 

        // if (LevelManager.friendsSpawned.Count > 1)
        // {
        //int index = LevelManager.friendsSpawned.FindIndex(d => d == friendTarget);
        // int index = LevelManager.friendsSpawned.IndexOf(friendTarget);
        // print("Target index:" + index);
        /*
        if (index < LevelManager.friendsSpawned.Count)
            target = LevelManager.friendsSpawned[index];
        else
            target = LevelManager.friendsSpawned[0];
            */
        // }
    }

    public void setTarget(AFriend target)
    {
        friendTarget = target;
        //int index = LevelManager.friendsSpawned.IndexOf(friendTarget);
        //int indexOrig = LevelManager.friendsSpawned.IndexOf(target);
        //print("Target index:" + index + " orig index:" + indexOrig);
    }


    public IEnumerator WaitMove(float sec)
    {
        yield return new WaitForSeconds(sec);
        moveBool = true;
    }


}
