using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Projectile
{
    public GameObject player;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

   public virtual void hitSomething()
   {
        if (canDespawn)
        {
            Destroy(gameObject);
        }
   }
}
