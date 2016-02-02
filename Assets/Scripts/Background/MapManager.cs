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

    // private variables
    private Transform groundHolder;



    public static bool putCharacter(EntityThing character, Vector3 newPos, Vector3 oldPos)
    {
        
        if (mapArray[(int)newPos.x, (int)newPos.y] == null)
        {
            DisplayCharactersBehaviour();
            mapArray[(int)newPos.x, (int)newPos.y] = character;

            if (oldPos != null)
                mapArray[(int)oldPos.x, (int)oldPos.y] = null;

            character.gameObject.GetComponent<Rigidbody2D>().MovePosition(newPos);

            return true;
        }
        return false;
    }

    private static void DisplayCharactersBehaviour()
    {
        string row = "";

        for (int i = yRow - 1; i >= 0; i--)
        {
            for (int j = 0; j < xCol; j++)
            {
                if (mapArray[j, i] == null)
                    row += " .";
                else
                    row += " x";
            }
            row += "\n";
        }
        print(row);
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
    }
    
    // generates the scene
    public void SetupScene()
    {
        // generating the map
        MapSetup();
  

    }
}
