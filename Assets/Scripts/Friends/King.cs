using UnityEngine;
using System.Collections;

public class King : AFriend {

	public King()
    {
        _health = 50;
        _armour = 0;
        damage = 2;
        speed = 2;
        air = false;
        attack_range = 1;
    }
}
