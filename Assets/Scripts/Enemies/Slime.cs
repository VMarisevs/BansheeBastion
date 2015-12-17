
using UnityEngine;

public class Slime : AEnemy {

 //   private Rigidbody2D rigidbody2D;

    public Slime()
    {
        //health = 20 * Level;
        health = 20;
        armour = 0;
        damage = 2;
        speed = 2;
        air = true;
        attack_range = 1;
    }
   
}
