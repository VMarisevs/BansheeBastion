using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour {

    // public variables
    public int columns = 16;
    public int rows = 8;
    // public object arrays
    public GameObject[] groundTiles;


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
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

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
