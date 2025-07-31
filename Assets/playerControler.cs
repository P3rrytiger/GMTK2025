using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour    
{
    public inputs inputScript;
    public Vector2 velocity;
    public float friction;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        inputScript = GameObject.FindGameObjectWithTag("input Handlerer").GetComponent<inputs>();
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
    }
}
