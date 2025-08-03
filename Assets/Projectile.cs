using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 position;
    public Vector2 velocity;
    public Collider2D colider;
    public SpriteSet sprite;
    public int despawnTime;
    public float time;
    public bool canDespawn;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        position += velocity*Time.deltaTime;
        gameObject.transform.position = new Vector3(position.x, position.y, 0);
        time += Time.deltaTime;

        if (time >= despawnTime&& canDespawn)
        {
            Destroy(gameObject);
        }

        otherThings();
    }

    public virtual void otherThings()
    {

    }
}
