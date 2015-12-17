using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour {

    // public variables
    public static int xCol = 16;
    public static int yRow = 8;
    
    // public object arrays
    public GameObject[] groundTiles;

    public static EntityThing[,] mapArray = new EntityThing[xCol, yRow];
    //public static int[,] pathArray = new int[xCol, yRow];

    public static char[,] pathArray = new char[xCol, yRow];


    // private variables
    private Transform groundHolder;



    public static void putCharacter(EntityThing character, Vector3 pos)
    {

        //  if (mapArray[(int)pos.x, (int)pos.y] == null)
        print("x=" + pos.x + " y=" + pos.y);
        
        if (pathArray[(int)pos.x, (int)pos.y] == 0)
        {
            //displayPathMap();
            // remove the char from it's position

           // pathArray[(int)character.transform.position.x, (int)character.transform.position.y] = 0;
          //  mapArray[character.x, character.y] = null;

            // put him into new position
           // pathArray[(int)pos.x, (int)pos.y] = 1;
            // mapArray[(int)pos.x, (int)pos.y] = character;
            //clearPathFromPath();

        }
        
      
    }




    // buld the map
    private void MapSetup()
    {
        // init map object
        groundHolder = new GameObject("Ground").transform;

        for (int y = 0; y < yRow; y++)
        {
            for (int x = 0; x < xCol; x++)
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
        /*
        for (int i = 0; i < pathArray.GetLength(0); i++)
        {
            for (int j=0; j < pathArray.GetLength(1); j++)
            {
                pathArray[i,j] = '.';
            }
        }*/
        pathArray = new char[,] { //y 0   1   2   3   4   5   6   7 
                                    {'.','.','.','.','.','.','.','.'}, // 0 x
                                    {'.','.','.','.','.','.','.','.'}, // 1
                                    {'.','.','.','.','.','.','.','.'}, // 2
                                    {'.','.','.','.','.','.','.','.'}, // 3
                                    {'.','.','.','.','.','.','.','.'}, // 4 
                                    //{'.','.','.','.','.','.','.','.'}, // 5
                                    {'#','#','#','#','#','#','.','#'}, // 5
                                    {'.','.','.','.','.','.','.','.'}, // 6
                                    {'.','.','.','.','.','.','.','.'}, // 7
                                    {'.','.','.','.','.','.','.','.'}, // 8
                                    {'.','.','.','.','.','.','.','.'}, // 9
                                    {'.','.','.','.','D','.','.','.'}, // 10
                                    {'.','.','.','.','.','.','.','.'}, // 11
                                    {'.','.','.','.','.','.','.','.'}, // 12
                                    {'.','.','.','.','.','.','.','.'}, // 13
                                    {'.','.','.','.','.','.','.','.'}, // 14
                                    {'.','.','.','.','.','.','.','.'}, // 15
                                                                                                 
        };                                                                                    
    }                                                                                             

    /*
    private static void clearPathFromPath()
    {
        for (int y = 0; y < yRow; y++)
        {
            for (int x = 0; x < xCol; x++)
            {
                if (pathArray[x, y] == 2 || pathArray[x, y] == 3)
                {
                    pathArray[x, y] = 0;
                }
            }
        }
    }*/
	
    public static void displayPathMap()
    {
        string path = "";
        //for (int y = 0; y < yRow; y++)
        for (int y = yRow-1; y >= 0; y--)
        {
            for (int x = 0; x < xCol; x++)
            {
                if (pathArray[x, y] == '.')
                    path += " O";// + pathArray[x, y];
                else
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
  

    }
}
