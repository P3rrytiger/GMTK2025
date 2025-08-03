using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Projectile
{
    public GameObject player;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

   public virtual void hitSomething()
   {
        Destroy(gameObject);
   }
}
