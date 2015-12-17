using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

    // enemy variety
    public AEnemy[] enemyArray;

    public static List<AEnemy> enemiesSpawned = new List<AEnemy>();
    public static List<AFriend> friendsSpawned = new List<AFriend>();

    private Transform enemyHolder;

    bool spawnMob = true;
    bool moveBool = true;


    public void Start()
    {
        enemyHolder = new GameObject("EnemyHolder").transform;
        createEnemy(enemyArray[0]);
    }


    private void enemySpawner()
    {
        spawnMob = false;
        if (enemyArray[0] is Slime)
        {
            createEnemy(enemyArray[0]);
           
            //Slime toInstanciateSlime = (Slime)enemyArray[0];
            //temp.attack();
            //GameObject instance = Instantiate(toInstanciateSlime, new Vector2(1, 1), Quaternion.identity) as GameObject;
            //instance.transform.SetParent(enemyHolder);
            // Instantiate(enemyArray[0],new Vector2(1,1));
        }
      
        StartCoroutine(Wait(2));
    }


    private void createEnemy(AEnemy enemy)
    {
        bool spawned = false;
        int x = 0;// Random.Range(0, MapManager.rows-1);

        int counter = 0;

        while (spawned == false)
        {           
           
            int y = Random.Range(0, MapManager.yRow - 1);

            if (MapManager.mapArray[x, y] == null)
            {
            
                Vector3 pos = new Vector2(x, y);

                AEnemy instance = Instantiate(enemy, pos, Quaternion.identity) as AEnemy;

                instance.transform.SetParent(enemyHolder.transform);
                
                MapManager.mapArray[x, y] = instance;
                MapManager.pathArray[x,y] = 's';
                LevelManager.enemiesSpawned.Add(instance);
                spawned = true;
            }

            if (counter > MapManager.yRow)
            {
                spawned = true;
                //spawnMob = false;
            }
            counter++;
 
        }
        

       

        //MapManager.putCharacter(instance, pos);

       
    }



    public IEnumerator Wait(int sec)
    {
        yield return new WaitForSeconds(sec);
        spawnMob = true;
    }

    public IEnumerator WaitMove(float sec)
    {
        yield return new WaitForSeconds(sec);
        moveBool = true;
    }

    public void MoveEnemies()
    {
        foreach(AEnemy enemy in enemiesSpawned)
        {
            StartCoroutine(WaitMove(1f));
            if (moveBool)
            {
                enemy.move();
                moveBool = false;
            }

            // print(enemy.transform.position.x);
            //Wait(1);
            ///enemy.transform.position = new Vector2(enemy.transform.position.x+1,enemy.transform.position.y);
        }
    }

    public void Update()
    {
        //if (spawnMob)
        //{
        //    enemySpawner();

        //}

        MoveEnemies();

    }

}
