using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{

    // public variables
    public static int xCol = 16;
    public static int yRow = 8;
    
    // public object arrays
    public GameObject[] groundTiles;

    public GameObject allowedTile;

    public static EntityThing[,] mapArray = new EntityThing[xCol, yRow];

    public static GameObject[,] mapAvailable = new GameObject[xCol, yRow];

    // private variables
    private Transform groundHolder;


    // singleton

    public static MapManager instance { get; private set; }

    void Awake()
    {
        // First we check if there are any other instances conflicting
        if (instance != null && instance != this)
        {
            // If that is the case, we destroy other instances
            Destroy(gameObject);
        }

        // Here we save our singleton instance
        instance = this;

        // Furthermore we make sure that we don't destroy between scenes (this is optional)
        DontDestroyOnLoad(gameObject);
    }


    public bool putCharacter(EntityThing character, Vector3 newPos, Vector3 oldPos)
    {
        
        if (mapArray[(int)newPos.x, (int)newPos.y] == null)
        {
            //DisplayCharactersBehaviour();
            
            mapArray[(int)newPos.x, (int)newPos.y] = character;

            if (oldPos != Vector3.zero)
                mapArray[(int)oldPos.x, (int)oldPos.y] = null;

            character.gameObject.GetComponent<Rigidbody2D>().MovePosition(newPos);

            DisplayAvailableBox();

            return true;
        }
        return false;
    }

    private void DisplayCharactersBehaviour()
    {
        string row = "";

        for (int i = yRow - 1; i >= 0; i--)
        {
            for (int j = 0; j < xCol; j++)
            {
                if (mapArray[j, i] == null)
                {
                    row += " .";
                //    // applying position
                //    GameObject instance = Instantiate(allowedTile, new Vector2(j, i), Quaternion.identity) as GameObject;
                //
                //    // adding into parent
                //    instance.transform.SetParent(groundHolder);
                }                    
                else
                    row += " x";
            }
            row += "\n";
        }
        print(row);
    }

    private void DisplayAvailableBox()
    {
        for (int i = yRow - 1; i >= 0; i--)
        {
            for (int j = 7; j < xCol; j++)
            {
                if (mapArray[j, i] == null)
                {
                    if (mapAvailable[j, i] == null)
                    {
                        // applying position
                        GameObject instance = Instantiate(allowedTile, new Vector2(j, i), Quaternion.identity) as GameObject;
                        // adding into parent
                        instance.transform.SetParent(groundHolder);
                        // inserting into array
                        mapAvailable[j, i] = instance;
                    }
                        
                }
                else if (mapAvailable[j, i] != null)
                {
                  //  print("destroying available");
                  // GameObject instance = ;

                    UnityEngine.Object.Destroy(mapAvailable[j, i]);

                    mapAvailable[j, i] = null;

                  //  UnityEngine.Object.Destroy(instance);

                }
                  
            }
           
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
    }
    
    // generates the scene
    public void SetupScene()
    {
        // generating the map
        MapSetup();
  

    }
}
