using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snake : Pluey
{
    public snakeAnimations sprites;
    public animationControler animationControler;
    public float timer;
    public bool justImplementedPluey;
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
            if (!justImplementedPluey)
            {
                justImplementedPluey = true;
                Instantiate(pluey, gameObject.transform.position, gameObject.transform.rotation);
            }
            animationControler.setAnimation(sprites.attack);

            if (timer >= 5)
            {
                timer = 0;
            }
        }
        else
        {
            animationControler.setAnimation(sprites.move);
            justImplementedPluey = false;
        }
    }
}
