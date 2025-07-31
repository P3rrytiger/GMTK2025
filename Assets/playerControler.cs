using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour    
{
    public inputs inputScript;
    public animationControler animationControler;
    public playerAnimations animationSet;
    public Vector2 velocity;
    public float friction;
    public float speed;
    public GameObject spriteObject;
    public string direction;
    
    // Start is called before the first frame update
    void Start()
    {
        direction = "down";
        inputScript = GameObject.FindGameObjectWithTag("input Handlerer").GetComponent<inputs>();
        animationControler = gameObject.GetComponentInChildren<animationControler>();
        animationSet = gameObject.GetComponentInChildren<playerAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity.x += inputScript.inputX.value;
        velocity.y += inputScript.inputY.value;

        velocity *= friction;

        Vector3 move = new Vector3(velocity.x, velocity.y, 0);
        move *= Time.deltaTime;
        move *= speed;

        gameObject.transform.position += move;

        getAnimation();
    }

    public void getAnimation()
    {
        if(inputScript.inputY.value != 0)
        {
            if(inputScript.inputY.value == 1)
            {
                animationControler.setAnimation(animationSet.walkUp);
                direction = "up";
            } else
            {
                animationControler.setAnimation(animationSet.walkDown);
                direction = "down";
            }
        } else if(inputScript.inputX.value != 0)
        {
            if (inputScript.inputX.value == 1)
            {
                animationControler.setAnimation(animationSet.walkRight);
                direction = "right";
            }
            else
            {
                animationControler.setAnimation(animationSet.walkLeft);
                direction = "left";
            }
        }
        else
        {
            //animationControler.setAnimation(animationSet.idle);
            switch (direction)
            {
                case "down":
                    animationControler.setAnimation(animationSet.idleDown);
                    break;
                case "up":
                    animationControler.setAnimation(animationSet.idleUp);
                    break;
                case "left":
                    animationControler.setAnimation(animationSet.idleLeft);
                    break;
                case "right":
                    animationControler.setAnimation(animationSet.idleRight);
                    break;
            }
        }
    }
}
