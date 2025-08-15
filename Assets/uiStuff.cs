using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class uiStuff : MonoBehaviour
{
    public UIDocument uIDocument;
    public GroupBox opt1;
    public GroupBox opt2;
    public weaponControler weaponControler;
    public float timer = 10;
    public bool timerActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timerActive && timer <= 0)
        {
            timerActive = false;
            settupOption(opt1);
            settupOption(opt2);

            uIDocument.rootVisualElement.visible = true;
            GameObject.FindGameObjectWithTag("Enimy Spawner").GetComponent<EnimySpawner>().spawnRate *= ((float)13)/10;
        }
    }

    public void OnEnable()
    {
        opt1 = uIDocument.rootVisualElement.Q("option1") as GroupBox;
        opt2 = uIDocument.rootVisualElement.Q("option2") as GroupBox;

        Debug.Log( opt1);

        settupOption(opt1);
        settupOption(opt2);

        //o1B.

        //o1B.RegisterCallback<ClickEvent>(button1WasJustClicked);
        //o2B.RegisterCallback<ClickEvent>(button2WasJustClicked);
        //o1B.text = "test";

    }

    public void OnDisable()
    {
        //o1B.UnregisterCallback<ClickEvent>(button1WasJustClicked);
    }

    public void buttonWasJustClicked(ClickEvent evt, efectCard card)
    {
        Debug.Log("yayyyyyy");
        /*GameObject.FindGameObjectWithTag("Weapon Handlerer").GetComponent<weaponControler>().addWeapon(1);
         */
        weaponControler.addWeapon(card);

        uIDocument.rootVisualElement.visible = false;

        timer = 30;
        timerActive = true;
    }
    /*public void button2WasJustClicked(ClickEvent evt)
    {
        Debug.Log("yayyyyyy");
        GameObject.FindGameObjectWithTag("Weapon Handlerer").GetComponent<weaponControler>().addWeapon(0);
        uIDocument.rootVisualElement.visible = false;
    }*/

    public void settupOption(GroupBox option)
    {
        Debug.Log(option);
        efectCard card = weaponControler.newEfectCard();
        TextElement tital = option.Q("tital") as TextElement;
        TextElement desc = option.Q("desc") as TextElement;
        Button button = option.Q("button") as Button;

        tital.text = card.name;
        desc.text = card.desc;

        button.RegisterCallback<ClickEvent, efectCard>(buttonWasJustClicked, card);
    }
}
