using UnityEngine;
using System.Collections;

public class Slime : AEnemy {
    
    

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
