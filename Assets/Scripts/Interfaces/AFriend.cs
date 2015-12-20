using UnityEngine;
using System.Collections;

public class AFriend : EntityThing {
    public int damage;
    public int speed;
    public bool air;
    public int attack_range;

    public void hit(int damage)
    {

        _health -= damage;

        //print("I've been attack. by " + damage + " my health: " + _health);

        if (_health <= 0)
        {
           // print("I am dead!");
            LevelManager.friendsSpawned.Remove(this);
            Object.Destroy(this.gameObject);
        }

    }
}
