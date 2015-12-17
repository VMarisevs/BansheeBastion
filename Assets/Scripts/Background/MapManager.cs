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
    public static int[,] pathArray = new int[8, 16];



    // private variables
    private Transform groundHolder;



    public static void putCharacter(EntityThing character, Vector3 pos)
    {

        //  if (mapArray[(int)pos.x, (int)pos.y] == null)
        if (pathArray[(int)pos.x, (int)pos.y] == 0)
        {
            displayPathMap();
            // remove the char from it's position

            pathArray[(int)character.transform.position.x, (int)character.transform.position.y] = 0;
          //  mapArray[character.x, character.y] = null;

            // put him into new position
            pathArray[(int)pos.x, (int)pos.y] = 1;
            // mapArray[(int)pos.x, (int)pos.y] = character;
            clearPathFromPath();

        }
      
    }




    // buld the map
    private void MapSetup()
    {
        // init map object
        groundHolder = new GameObject("Ground").transform;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                // generating random ground tile
                GameObject toInstantiate = groundTiles[Random.Range(0, groundTiles.Length)];

                // applying position
                GameObject instance = Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity) as GameObject;

                // adding into parent
                instance.transform.SetParent(groundHolder);
            }
        }

        clearPathArray();

    }

    private static void clearPathArray()
    {
        // init pathMap
        for (int i = 0; i < pathArray.GetLength(0); i++)
        {
            for (int j=0; j < pathArray.GetLength(1); j++)
            {
                pathArray[i,j] = 0;
            }
        }
    }

    private static void clearPathFromPath()
    {
        for (int i = 0; i < pathArray.GetLength(0); i++)
        {
            for (int j = 0; j < pathArray.GetLength(1); j++)
            {
                if (pathArray[i, j] == 2 || pathArray[i, j] == 3)
                {
                    pathArray[i, j] = 0;
                }
            }
        }
    }
	
    private static void displayPathMap()
    {
        string path = "";
        for (int x = 0; x < MapManager.pathArray.GetLength(0); x++)
        {
            for (int y = 0; y < MapManager.pathArray.GetLength(1); y++)
            {
                path += " " + pathArray[x, y];
            }
            path += "\n";
        }
        print(path);
    }
    // generates the scene
    public void SetupScene()
    {
        // generating the map
        MapSetup();
        clearPathArray();

    }
}
