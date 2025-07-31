using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]

public class InputClass
{
    public string name;
    public int value;
    public bool justPressed;
    public bool held;
    public bool active;
    
    public InputClass(string name)
    {
        this.name = name;
        value = 0;
        justPressed = false;
        held = false;
        active = false;
    }
}
public class inputs : MonoBehaviour
{

    public InputClass inputX;
    public InputClass inputY;
    public InputClass select;
    public InputClass deselect;
    public InputClass menue;
    public bool test;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputX.value = 0;
        inputY.value = 0;

        calculateInput(inputX, KeyCode.RightArrow, 1);
        calculateInput(inputX, KeyCode.LeftArrow, -1);
        calculateInput(inputY, KeyCode.UpArrow, 1);
        calculateInput(inputY, KeyCode.DownArrow, -1);
    }

    public void calculateInput(InputClass input, KeyCode button, int changeBy)
    {
        if (Input.GetKey(button))
        {
            if (input.justPressed)
            {
                input.justPressed = false;
                input.held = true;
            }

            if (!input.active)
            {
                input.active = true;
                input.justPressed = true;
            }

            input.value += changeBy;
        }   
    }
}
