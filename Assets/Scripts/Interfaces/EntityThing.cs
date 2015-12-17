using UnityEngine;
using System.Collections;

public abstract class EntityThing : MonoBehaviour {

    protected LayerMask blockingLayer;
    protected BoxCollider2D boxCollider2D;

    public int health;
    public int armour;


}
