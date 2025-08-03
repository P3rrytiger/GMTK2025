using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class Animation{
    public List<Sprite> sprites;
    public int frame;
    public float time;
    public bool active;
    public SpriteRenderer obj;
    public float fps;

    public Animation(SpriteRenderer obj, float fps)
    {
        frame = 0;
        time = 0;
        active = false;
        this.obj = obj;
        this.fps = fps;
    }

    public void update()
    {
        if (active)
        {
            time += Time.deltaTime*fps;
            if (time >= 1)
            {
                time -= 1;
                frame += 1;

                if (frame >= sprites.Count)
                {
                    frame = 0;
                }

                obj.sprite = sprites[frame];
            }

            

        }
    }

    public void setAnimation(List<Sprite> newAnimation)
    {
        if (sprites != newAnimation)
        {
            sprites = newAnimation;
            frame = 0;
            time = 1;
            active = true;
        }
    }
}
public class animationControler : MonoBehaviour
{
    public Animation animation;
    public float fps;
    // Start is called before the first frame update
    void Start()
    {
        animation = new Animation(gameObject.GetComponent<SpriteRenderer>(), fps);
    }

    // Update is called once per frame
    void Update()
    {
        animation.update();
    }

    public void updateAnimation()
    {
        animation.update();
    }

    public void setAnimation(List<Sprite> newAnimation)
    {
        animation.setAnimation(newAnimation);
    }
}
