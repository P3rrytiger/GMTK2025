using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class enimyType
{
    public GameObject obj;
    private Enimy script;
    public float frequency;
}

public class EnimySpawner : MonoBehaviour
{
    public List<enimyType> enimies;
    private float timer;
    public float spawnRate;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime*spawnRate;

        if(timer >= 1)
        {
            timer--;

            spawnEnimy();
        }
    }

    public void spawnEnimy()
    {


        enimyType enimy = enimies[Mathf.FloorToInt(UnityEngine.Random.Range(0, enimies.Count))];

        float totalFrequency = getTotalEnimyFrequency();

        if(UnityEngine.Random.Range(0, totalFrequency) <= enimy.frequency)
        {
            Instantiate(enimy.obj, player.transform.position + randomVector3(10), player.transform.rotation );
            Console.WriteLine("test2");
            Debug.Log("test");
        } else
        {
            spawnEnimy();
            Console.WriteLine("test");
        }

        
    }

    public float getTotalEnimyFrequency()
    {
        float output = 0;

        foreach(enimyType enimy in enimies)
        {
            output += enimy.frequency;
        }

        Console.WriteLine(output.ToString());
        Debug.Log(output);

        return output;
    }

    public Vector3 randomVector3(float magnetude)
    {
        float direction = UnityEngine.Random.Range(0, Mathf.PI*2);

        return new Vector3(Mathf.Cos(direction), Mathf.Sin(direction), 0) * magnetude;
    }
}
