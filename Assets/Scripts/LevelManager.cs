using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    // enemy variety
    public GameObject _gameAction;
    public AEnemy[] enemyArray;
    public AFriend[] friendArray;

    // actual enemies
    public static List<AEnemy> enemiesSpawned = new List<AEnemy>();
    public static List<AFriend> friendsSpawned = new List<AFriend>();

   

    // just to make it clean
    private Transform enemyHolder;
    private Transform friendsHolder;

    private bool enemySpawnMob = true;
    private float enemySpawnDelay = 2;

    public GameObject gameScore;
    private static int _score;

    public void Start()
    {

        enemyHolder = new GameObject("EnemyHolder").transform;
        friendsHolder = new GameObject("friendsHolder").transform;
        enemyHolder.SetParent(_gameAction.transform);
        friendsHolder.SetParent(_gameAction.transform);

        //createEnemy(enemyArray[0], new Vector2(0, (int)MapManager.yRow - 1));
        //createEnemy(enemyArray[0], new Vector2(0, (int)MapManager.yRow - 2));
        //createEnemy(enemyArray[0], new Vector2(0, (int)MapManager.yRow - 3));
        //createEnemy(enemyArray[0], new Vector2(0, (int)MapManager.yRow - 4));
        //createEnemy(enemyArray[0], new Vector2(0, (int)MapManager.yRow - 5));
        //createEnemy(enemyArray[0], new Vector2(0, (int)MapManager.yRow - 6));

        //createEnemy(enemyArray[0], new Vector2(1, (int)MapManager.yRow - 1));
        //createEnemy(enemyArray[0], new Vector2(1, (int)MapManager.yRow - 2));
        //createEnemy(enemyArray[0], new Vector2(1, (int)MapManager.yRow - 3));
        //createEnemy(enemyArray[0], new Vector2(1, (int)MapManager.yRow - 4));
        //createEnemy(enemyArray[0], new Vector2(1, (int)MapManager.yRow - 5));
        //createEnemy(enemyArray[0], new Vector2(1, (int)MapManager.yRow - 6));

        //createEnemy(enemyArray[0], new Vector2(2, (int)MapManager.yRow - 1));
        //createEnemy(enemyArray[0], new Vector2(2, (int)MapManager.yRow - 2));
        //createEnemy(enemyArray[0], new Vector2(2, (int)MapManager.yRow - 3));
        //createEnemy(enemyArray[0], new Vector2(2, (int)MapManager.yRow - 4));
        //createEnemy(enemyArray[0], new Vector2(2, (int)MapManager.yRow - 5));
        //createEnemy(enemyArray[0], new Vector2(2, (int)MapManager.yRow - 6));

        //createEnemy(enemyArray[0], new Vector2(3, (int)MapManager.yRow - 1));
        //createEnemy(enemyArray[0], new Vector2(3, (int)MapManager.yRow - 2));
        //createEnemy(enemyArray[0], new Vector2(3, (int)MapManager.yRow - 3));
        //createEnemy(enemyArray[0], new Vector2(3, (int)MapManager.yRow - 4));
        //createEnemy(enemyArray[0], new Vector2(3, (int)MapManager.yRow - 5));
        //createEnemy(enemyArray[0], new Vector2(3, (int)MapManager.yRow - 6));



        createFriend(friendArray[0], new Vector2(MapManager.xCol - 1, (int)MapManager.yRow - 1));
        createFriend(friendArray[0], new Vector2(MapManager.xCol - 1, (int)MapManager.yRow - 2));
        createFriend(friendArray[0], new Vector2(MapManager.xCol - 1, (int)MapManager.yRow - 3));
        createFriend(friendArray[0], new Vector2(MapManager.xCol - 1, (int)MapManager.yRow - 4));
        createFriend(friendArray[0], new Vector2(MapManager.xCol - 1, (int)MapManager.yRow - 5));
        createFriend(friendArray[0], new Vector2(MapManager.xCol - 1, (int)MapManager.yRow - 6));

        createFriend(friendArray[0], new Vector2(MapManager.xCol - 2, (int)MapManager.yRow - 1));
        createFriend(friendArray[0], new Vector2(MapManager.xCol - 2, (int)MapManager.yRow - 2));
        createFriend(friendArray[0], new Vector2(MapManager.xCol - 2, (int)MapManager.yRow - 3));
        createFriend(friendArray[0], new Vector2(MapManager.xCol - 2, (int)MapManager.yRow - 4));
        createFriend(friendArray[0], new Vector2(MapManager.xCol - 2, (int)MapManager.yRow - 5));
        createFriend(friendArray[0], new Vector2(MapManager.xCol - 2, (int)MapManager.yRow - 6));

        //print(" friends spawned :" + friendsSpawned.Count);
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
            coll.gameObject.SendMessage("ApplyDamage", 10);

    }

    private void createEnemy(AEnemy enemy)
    {
        bool spawned = false;
        int x = 0;// Random.Range(0, MapManager.rows-1);

        int counter = 0;

        while (spawned == false)
        {           
           
            int y = Random.Range(0, MapManager.yRow - 1);


            
             Vector3 pos = new Vector2(x, y);


            if(Physics2D.OverlapCircleAll(pos, .4f).Length <1)
            {

                createEnemy(enemy, pos);

                spawned = true;
            }



        
       

            if (counter > MapManager.yRow)
            {
                spawned = true;
            }
            counter++;
 
        }
       
    }

    private IEnumerator spawnWait(float sec)
    {
        yield return new WaitForSeconds(sec);
        enemySpawnMob = true;
    }


    private void createEnemy(AEnemy enemy, Vector2 pos)
    {
        AEnemy instance = Instantiate(enemy, pos, Quaternion.identity) as AEnemy;
        instance.transform.SetParent(enemyHolder.transform);
        enemiesSpawned.Add(instance);
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


    private void SpawnEnemies()
    {
        if (enemySpawnMob)
        {
            enemySpawnMob = false;
            createEnemy(enemyArray[0]);
            StartCoroutine(spawnWait(enemySpawnDelay));
            
        }
    }

    private void MoveEnemies()
    {
        foreach (AEnemy enemy in enemiesSpawned)
        {
            
            if (enemy._action)
            {
                enemy._action = false;
                enemy.move();
                
                StartCoroutine(waitAction(enemy));
                
            }

        }

    }

    private void FriendsAttack()
    {
        foreach (AFriend friend in friendsSpawned)
        {
            if (friend._action)
            {
                friend._action = false;
                //friend.attack();
                StartCoroutine(waitAction(friend));
            }
        }
    }

    private IEnumerator waitAction(EntityThing entity)
    {
        yield return new WaitForSeconds(entity._speed);
        entity._action = true;
    }

    // default actions
    public void Update()
    {
 
        SpawnEnemies();

        MoveEnemies();

       // FriendsAttack();

    }



    public void OnGUI()
    {
        gameScore.GetComponent<Text>().text = "Score:" + _score;
    }



    public static void addScore(int score)
    {
        _score += score;
    }

    public static int getScore()
    {
        return _score;
    }
}
