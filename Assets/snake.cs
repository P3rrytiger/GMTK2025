using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snake : Pluey
{
    public snakeAnimations sprites;
    public animationControler animationControler;
    public float timer;
    private bool justImplementedPluey;
    //private bool justBeganImplementingPluey;
    public GameObject pluey;
    public override void Start()
    {
        base.Start();

        animationControler = gameObject.GetComponentInChildren<animationControler>();

        animationControler.setAnimation(sprites.move);
    }
    public override void otherThings()
    {
        base.otherThings();

        timer += Time.deltaTime;

        if (timer >= 3)
        {

            if (justImplementedPluey)
            {
                gameObject.GetComponent<ParticleSystem>().Play();
            }
            
            animationControler.setAnimation(sprites.attack);
            justImplementedPluey = false;

            if (timer >= 5)
            {
                timer -= 5;
            }
        }
        else
        {
            animationControler.setAnimation(sprites.move);
            

            if (!justImplementedPluey)
            {
                justImplementedPluey = true;
                Instantiate(pluey, gameObject.transform.position, gameObject.transform.rotation);
            }
        }
    }
}
