using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour {

    // public variables
    public static int columns = 16;
    public static int rows = 8;
    
    // public object arrays
    public GameObject[] groundTiles;
    
    public static EntityThing[,] mapArray = new EntityThing[rows, columns];





    public static void putCharacter(EntityThing character, Vector2 pos)
    {
        character.gameObject.transform.position = pos;
        character.location = pos;
       // character.prefab.transform.position = pos;
        mapArray[(int)pos.x, (int)pos.y] = character;
    }

    // private variables
    private Transform mapHolder;


    // buld the map
    private void MapSetup()
    {
        // init map object
        mapHolder = new GameObject("Map").transform;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                // generating random ground tile
                GameObject toInstantiate = groundTiles[Random.Range(0, groundTiles.Length)];

                // applying position
                GameObject instance = Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity) as GameObject;

                // adding into parent
                instance.transform.SetParent(mapHolder);
            }
        }

    }

	// generates the scene
    public void SetupScene()
    {
        // generating the map
        MapSetup();

    }
}
