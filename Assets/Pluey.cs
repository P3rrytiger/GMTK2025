using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pluey : Enimy
{
    public GameObject player;
    public float speed;
    public float justHit = 0;
    public GameObject dieEffect;
    public GameObject[] playerProjectiles;
    // Start is called before the first frame update
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void otherThings()
    {
        justHit -= Time.deltaTime * 10;

        if (justHit > 0)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
        else
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }

        float selfAngle = angle(toVector2(player.transform.position) - position);

        gameObject.transform.eulerAngles = new Vector3(0, 0, selfAngle);

        float selfRadiens = selfAngle * (Mathf.PI / 180);

        velocity =Vector2.Lerp(velocity, new Vector2(Mathf.Cos(selfRadiens), Mathf.Sin(selfRadiens))*speed, Time.deltaTime);

        amISecretlyDead();
    }

    public float angle(Vector2 vector)
    {
        return (Mathf.Atan2(vector.y, vector.x) / Mathf.PI) * 180;
    }

    public static Vector2 toVector2(Vector3 vec)
    {
        return new Vector2(vec.x, vec.y);
    }

    public void amISecretlyDead()
    {
        playerProjectiles = GameObject.FindGameObjectsWithTag("Player Projectile");
        Debug.Log(playerProjectiles);
        
        Collider2D self = gameObject.GetComponent<Collider2D>();

        foreach(GameObject projectile in playerProjectiles)
        {
            Weapon curent = projectile.GetComponent<Weapon>();
            if (self.IsTouching(curent.colider))
            {
                health -= curent.damage;
                justHit = 1;

                float selfAngle = angle(toVector2(player.transform.position) - position);

                gameObject.transform.eulerAngles = new Vector3(0, 0, selfAngle);

                float selfRadiens = selfAngle * (Mathf.PI / 180);

                velocity = Vector2.Lerp(velocity, new Vector2(Mathf.Cos(selfRadiens), Mathf.Sin(selfRadiens)) * speed*-1, (float)4/5);


                if (health <= 0)
                {
                    Instantiate(dieEffect, gameObject.transform.position, gameObject.transform.rotation);
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
