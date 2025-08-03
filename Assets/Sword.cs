using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    //public Vector3 debug;
    public bool isBoneXD;
    public float timer;
    public float nps;
    public GameObject note;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        //debug = Input.mousePosition;
        Vector2 diference = new Vector2(player.transform.position.x, player.transform.position.y) - position;
        //diference *= 1 * Time.deltaTime;


        /*velocity = diference;

        position += velocity * Time.deltaTime*10;
        gameObject.transform.position = new Vector3(position.x, position.y, 0);*/

        gameObject.transform.position = player.transform.position;

        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        Camera camcam = cam.GetComponent<Camera>();

        //gameObject.transform.eulerAngles = new Vector3(0, 0, angle(new Vector2(Input.mousePosition.x, Input.mousePosition.y)));
        gameObject.transform.eulerAngles = new Vector3(0, 0, angle( 

            (Input.mousePosition - (new Vector3(Screen.width, Screen.height, 0)/2))   

            - (camcam.WorldToScreenPoint(player.transform.position) - camcam.WorldToScreenPoint(cam.transform.position) )
            
            ));

        if (isBoneXD)
        {
            timer += Time.deltaTime * nps;
            if (timer >= 1)
            {
                timer -= 1;




                Projectile theNoteThatWasJustFired = Instantiate(note, gameObject.transform.position, gameObject.transform.rotation).GetComponent<Projectile>();

                theNoteThatWasJustFired.velocity = new Vector2(Mathf.Cos(gameObject.transform.eulerAngles.z*Mathf.PI/180), Mathf.Sin(gameObject.transform.eulerAngles.z * Mathf.PI / 180))*speed;
                theNoteThatWasJustFired.despawnTime = 1;
                theNoteThatWasJustFired.canDespawn = true;
                //theNoteThatWasJustFired.velocity += player.GetComponent<playerControler>().velocity;
            }
        }
    }

    public float angle(Vector2 vector)
    {
        return (Mathf.Atan2(vector.y, vector.x)/Mathf.PI)*180;
    }

    public void Start()
    {
        if (isBoneXD)
        {
            gameObject.GetComponentInChildren<animationControler>().setAnimation(gameObject.GetComponent<SwordSprits>().swing);
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void hitSomething()
    {
        
    }
}
