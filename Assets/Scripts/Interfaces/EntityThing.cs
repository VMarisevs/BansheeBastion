using UnityEngine;
using System.Collections;

public abstract class EntityThing : MonoBehaviour {

    protected LayerMask blockingLayer;
    protected BoxCollider2D boxCollider2D;

    public string _name;
    public int _health;
    public int _maxHealth;
    public int _armour;
    public float _speed;
    public bool _action = true;

    private float healthBarLength;
    private float maxHealthBarLength;
    private float healthBarWidth = 20;
    private Vector2 screenPosition;

    public void Start()
    {
        //healthBarLength = _health*20;
        maxHealthBarLength = Screen.width / 300 * _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // centralize the camera against the character
        screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        screenPosition.y = Screen.height - screenPosition.y;
        // setup the position against character
        screenPosition.y -= 40f;
        screenPosition.x -= healthBarLength/2;
        AddjustCurrentHealth();
    }

    void OnGUI()
    {
        GUI.Box(new Rect(screenPosition.x, screenPosition.y, healthBarLength, healthBarWidth), _health + "/" + _maxHealth);
    }

    public void AddjustCurrentHealth()
    {
        if (_health < 0)
            _health = 0;

        if (_health > _maxHealth)
            _health = _maxHealth;

        if (_maxHealth < 1)
            _maxHealth = 1;

        healthBarLength = (maxHealthBarLength) * (_health / (float)_maxHealth);
    }

}
