using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SerializeReferenceEditor;

[Serializable]
public class efect
{
    [SerializeReference, SR]
    public List<efect> childEfects = new List<efect>();

    public virtual void runtime(efect parent)
    {
        foreach(efect currentEfect in childEfects)
        {
            currentEfect.runtime(this);
        }
    }
}

[Serializable]
public class weapon : efect
{



    public GameObject weaponObjPrefab;
    public GameObject weaponObj;

    //initializetion function
    //public weapon() { }
}

[Serializable]
public class trombone : weapon
{
    public float firingSpeed;

    public GameObject getWeapon()
    {
        if (weaponObj == null)
        {
            //GameObject.FindObjectOfType<Camera>().isActiveAndEnabled
            weaponObj = weaponControler.Instantiate(weaponObjPrefab);
        }

        return (weaponObj);
    }

    public override void runtime(efect parent)
    {
        getWeapon();

        weaponObj.GetComponent<Sword>().nps = firingSpeed;

        base.runtime(this);
    }
}

[Serializable]
public class tBoneFieringSpeedUpbrade : efect
{
    public override void runtime(efect parent)
    {
        trombone parentObj = (trombone)parent;

        parentObj.getWeapon().GetComponent<Sword>().nps += 1;

        parentObj.getWeapon().GetComponent<Sword>().nps *= 11;
        parentObj.getWeapon().GetComponent<Sword>().nps /= 10;

        //weaponObj


        foreach (efect currentEfect in childEfects)
        {
            currentEfect.runtime(this);
        }
    }
}


//[Serializable]
public class weaponControler : MonoBehaviour
{
    public efect baseEfect;
    
    
    


    // Start is called before the first frame update
    void Start()
    {
        /*allEfects.Add(new efect());
        allEfects.Add(new weapon());*/
    }

    // Update is called once per frame
    void Update()
    {
        baseEfect.runtime(baseEfect);
    }
}
