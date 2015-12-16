using UnityEngine;
using System.Collections;

public abstract class EntityThing : MonoBehaviour {

    public Vector2 location;
    public GameObject prefab;
    public int health;
    public int armour;

    public EntityThing()
    {
        location = new Vector2(-1, -1);
    }



}
