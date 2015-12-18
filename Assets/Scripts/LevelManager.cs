using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

    // enemy variety
    public AEnemy[] enemyArray;
    public AFriend[] friendArray;

    public static List<AEnemy> enemiesSpawned = new List<AEnemy>();
    public static List<AFriend> friendsSpawned = new List<AFriend>();

    private Transform enemyHolder;
    private Transform friendsHolder;

    private bool spawnMob = true;
    private bool moveBool = true;
    private float enemySpawnDelay = 2;

    public void Start()
    {
        enemyHolder = new GameObject("EnemyHolder").transform;
        friendsHolder = new GameObject("friendsHolder").transform;

        createFriend(friendArray[0], new Vector2(MapManager.xCol - 1, (int)MapManager.yRow / 2));

        createFriend(friendArray[0], new Vector2(MapManager.xCol - 1, (int)MapManager.yRow -1));
        //print(" friends spawned :" + friendsSpawned.Count);
    }


    private void enemySpawner()
    {
        spawnMob = false;
        createEnemy(enemyArray[0]);
      
        StartCoroutine(Wait(enemySpawnDelay));
    }


    private void createEnemy(AEnemy enemy)
    {
        bool spawned = false;
        int x = 0;// Random.Range(0, MapManager.rows-1);

        int counter = 0;

        while (spawned == false)
        {           
           
            int y = Random.Range(0, MapManager.yRow - 1);

            if (MapManager.pathArray[x, y] == '.')
            {
            
                Vector3 pos = new Vector2(x, y);

                AEnemy instance = Instantiate(enemy, pos, Quaternion.identity) as AEnemy;

                instance.setTarget(friendsSpawned[0]);

                instance.transform.SetParent(enemyHolder.transform);             
                //MapManager.mapArray[x, y] = instance;
                MapManager.pathArray[x,y] = '#';
                enemiesSpawned.Add(instance);
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

    private void createFriend(AFriend friend, Vector2 pos)
    {
        if (MapManager.mapArray[(int)pos.x, (int)pos.y] == null)
        {     
            AFriend instance = Instantiate(friend, pos, Quaternion.identity) as AFriend;

            instance.transform.SetParent(friendsHolder.transform);

            MapManager.mapArray[(int)pos.x, (int)pos.y] = instance;
            friendsSpawned.Add(instance);
            
        }
    }

    public IEnumerator Wait(float sec)
    {
        yield return new WaitForSeconds(sec);
        spawnMob = true;
    }

    public IEnumerator WaitMove(AEnemy enemy)
    {
        print("speed" + enemy.speed + "\ntarget" + enemy.friendTarget);
        yield return new WaitForSeconds(enemy.speed);
        enemy.moveBool = true;
    }



    public void MoveEnemies()
    {
        foreach (AEnemy enemy in enemiesSpawned)
        {
            if (enemy.moveBool)
            {
                enemy.moveBool = false;
                enemy.move();
                StartCoroutine(WaitMove(enemy));
            }

        }

    }

    public void Update()
    {
        //if (spawnMob)
        //{
        //    enemySpawner();

        //}
        if (enemiesSpawned.Count < 5)
        {
            if (spawnMob)
            {
                enemySpawner();

            }
        }

        if (moveBool)
        {
            MoveEnemies();
        }

    }

}
