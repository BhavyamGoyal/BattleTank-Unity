using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPlayer
{
    public ModelPlayer(Controls controls)
    {
        this.controls = controls;
    }
    public bool gameStarted;
    public PlayerNumber playerNumber;
    public bool freez = false;
    public float lastShot=0;
    public float firePower=10;
    public float fireInterval = 1;
    public float speed=15;
    public float rotationSpeed = 40;
    public float boost=1;
    public float health = 100;
    public int score = 0;
    public BulletTypes bulletType = BulletTypes.explossiveBullet;
    public void TakeDamage(float damage)
    {
       health -= damage;

    }
    public bool IsAlive()
    {
        return health > 0;
    }
    public Controls controls;
}
