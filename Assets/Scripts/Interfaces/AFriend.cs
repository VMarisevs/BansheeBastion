using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class AFriend : EntityThing {

    public int _maxDamage;
    
    public bool _air;
    public int _attackRange;

    private bool waitForAttack = true;
    private AEnemy _enemyTarget;

/*
    public methods
*/
    public void hit(int damage)
    {

        _health -= damage;

        //print("I've been attack. by " + damage + " my health: " + _health);

        if (_health <= 0)
        {
           // print("I am dead!");
            LevelManager.friendsSpawned.Remove(this);
            UnityEngine.Object.Destroy(this.gameObject);
        }

    }

    public void attack()
    {

        if (haveATarget())
		{
			if (waitForAttack)
			{
				waitForAttack = false;

				
				// then check the range and attack
				if (inRangeToAttackTarget())
					attackTarget();
				

				//StartCoroutine(WaitMove());

				waitForAttack = true;

			}
		}

        
    }

/*
    private methods
*/
    private bool changeTarget()
    {
        int bestTarget = -1; // means no target
        int bestScore = 1000;
        int myX = (int)transform.position.x;
        int myY = (int)transform.position.y;
        Vector2 myPosition = new Vector2(myX, myY);

        // if there is atleast 1 friend, then checking and returning index
        if (!(LevelManager.enemiesSpawned.Count < 1))
        {
            //print("trying to change target");
            // looking for best target
            for (int i = 0; i < LevelManager.enemiesSpawned.Count; i++)
            {

                AEnemy potentialTarget = LevelManager.enemiesSpawned[i];

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
                _enemyTarget = LevelManager.enemiesSpawned[bestTarget];
            }
            catch (Exception e)
            {
                print("ERROR!" + e.StackTrace);
            }


            return true;
        }



        return false;
    }

    private bool haveATarget()
    {
        bool result = false;

        if (LevelManager.enemiesSpawned.Contains(_enemyTarget))
        {
            return true;
        }

        result = changeTarget();


        return result;
    }

    private bool inRangeToAttackTarget()
    {
        int myX = (int)transform.position.x;
        int myY = (int)transform.position.y;
        int targetX = (int)_enemyTarget.transform.position.x;
        int targetY = (int)_enemyTarget.transform.position.y;

        int distance = MapCalc.GetMinCost(new Vector2(myX, myY), new Vector2(targetX, targetY));

        if (distance <= _attackRange)
            return true;

        return false;
    }

    private void attackTarget()
    {
        if (_enemyTarget != null)
        {
            int damage = Random.Range(0, _maxDamage);
            _enemyTarget.hit(damage);
        }
    }

}
