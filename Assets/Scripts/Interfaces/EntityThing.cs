using UnityEngine;
using System.Collections;

public abstract class EntityThing : MonoBehaviour {

    protected LayerMask blockingLayer;
    protected BoxCollider2D boxCollider2D;

    public int _health;
    public int _armour;

    

}
