using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pluey : Enimy
{
    public GameObject player;
    public float speed;
    // Start is called before the first frame update
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void otherThings()
    {
        float selfAngle = angle(toVector2(player.transform.position) - position);

        gameObject.transform.eulerAngles = new Vector3(0, 0, selfAngle);

        float selfRadiens = selfAngle * (Mathf.PI / 180);

        velocity = new Vector2(Mathf.Cos(selfRadiens), Mathf.Sin(selfRadiens))*speed;

        amISecretlyDead();
    }

    public float angle(Vector2 vector)
    {
        return (Mathf.Atan2(vector.y, vector.x) / Mathf.PI) * 180;
    }

    public Vector2 toVector2(Vector3 vec)
    {
        return new Vector2(vec.x, vec.y);
    }

    public void amISecretlyDead()
    {
        GameObject[] playerProjectiles = GameObject.FindGameObjectsWithTag("Player Projectile");
        
        Collider2D self = gameObject.GetComponent<Collider2D>();

        foreach(GameObject projectile in playerProjectiles)
        {
            Weapon curent = projectile.GetComponent<Weapon>();
            if (self.IsTouching(curent.colider))
            {
                health -= curent.damage;
                

                if(health <= 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    curent.hitSomething();
                }
            }
        }
    }
}
