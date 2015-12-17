using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour {

    public AEnemy[] enemyArray;
   // private Transform enemyHolder;

    bool spawnMob = true;


    private LevelManager()
    {
       
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
        EntityThing instance = Instantiate(enemy, new Vector2(1, 1), Quaternion.identity) as EntityThing;
     
        int x = Random.Range(0, MapManager.rows-1);
        int y = Random.Range(0, MapManager.columns-1);
        Vector2 pos = new Vector2((float)x,(float)y);
        MapManager.putCharacter(instance, pos);

    }

    public IEnumerator Wait(int sec)
    {
        yield return new WaitForSeconds(sec);
        spawnMob = true;
    }


    public void Update()
    {
        if (spawnMob)
        {
            enemySpawner();

        }

    }

}
