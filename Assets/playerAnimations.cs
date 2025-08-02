using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSet : MonoBehaviour
{
    public List<Sprite> defaultSprite;
}

public class playerAnimations : SpriteSet
{

    public List<Sprite> idleDown;
    public List<Sprite> idleUp;
    public List<Sprite> idleRight;
    public List<Sprite> idleLeft;
    public List<Sprite> walkDown;
    public List<Sprite> walkUp;
    public List<Sprite> walkLeft;
    public List<Sprite> walkRight;

}
