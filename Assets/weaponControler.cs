using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SerializeReferenceEditor;
using System.Security.Cryptography;

[Serializable]
public class efect
{
    public string name;
    [SerializeReference, SR]
    public List<efect> childEfects = new List<efect>();

    public virtual void runtime(efect parent)
    {
        foreach(efect currentEfect in childEfects)
        {
            currentEfect.runtime(this);
        }

        
    }

    public virtual void efectSettup(efect parent)
    {
        //efect temp = new efect();
        //childEfects = parent.childEfects;
        foreach(efect parentEfect in parent.childEfects)
        {
            /*
            //
            Type type = parentEfect.GetType();

            //[SerializeReference]
            efect temp = new typeof(parentEfect)();

            temp = (type)temp;
            */
            //efect temp = new efect();



            childEfects.Add(parentEfect);
        }

        //return this;
    }

    public virtual efect settup(efect parent)
    {

        efect temp = new efect();
        temp.efectSettup(parent);

        return temp;
    }
    
}

[Serializable]
public class weapon : efect
{



    public GameObject weaponObjPrefab;
    public GameObject weaponObj;
    public float damage;

    //initializetion function
    //public weapon() { }
    public GameObject getWeapon()
    {
        if (weaponObj == null)
        {
            //GameObject.FindObjectOfType<Camera>().isActiveAndEnabled
            weaponObj = weaponControler.Instantiate(weaponObjPrefab);
        }

        return (weaponObj);
    }

    public void weaponSettup(weapon parent)
    {
        weaponObjPrefab = parent.weaponObjPrefab;
        weaponObj = parent.weaponObj;
        damage = parent.damage;
        efectSettup(parent);
    }
}

[Serializable]
public class trombone : weapon
{
    public float firingSpeed;

    public void tBoneSettup(trombone parent)
    {
        firingSpeed = parent.firingSpeed;
        weaponSettup(parent);
    }

    public override void runtime(efect parent)
    {
        getWeapon();

        weaponObj.GetComponentInChildren<animationControler>().animation.fps = weaponObj.GetComponent<Sword>().nps;
        weaponObj.GetComponent<Sword>().nps = firingSpeed;
        weaponObj.GetComponent<Sword>().damage = damage;
        

        base.runtime(this);
    }
}

[Serializable]
public class sword : weapon
{
    //public float damage;
    public List<swordSwing> thingsToDelete;
    public float timer;
    public float slashRate = 0;
    public float trailTime;
    public GameObject swingPrefab;
    public float baseDamage;

    public void swordSettup(sword parent)
    {
        slashRate = parent.slashRate;
        trailTime = parent.trailTime;
        swingPrefab = parent.swingPrefab;
        thingsToDelete = parent.thingsToDelete;
        weaponSettup(parent);
    }

    public override void runtime(efect parent)
    {
        getWeapon();

        if(thingsToDelete.Count > 0)
        {
            foreach(swordSwing thing in thingsToDelete)
            {
                childEfects.Remove(thing);
            }

            thingsToDelete.Clear();
        }

        

        weaponObj.GetComponent<Sword>().damage = damage * Time.deltaTime;

        timer += Time.deltaTime * slashRate;

        if (timer >= 1)
        {
            timer--;
            childEfects.Add(new swordSwing(trailTime, this, swingPrefab));
        }

        damage = baseDamage;

        base.runtime(this);
    }
}

[Serializable]
public class swordSwing : weapon
{
    public float lifetime;
    private float lifeLived = 0;
    public override void runtime(efect parent)
    {
        sword parentObj = (sword)parent;

        getWeapon();

        weaponObj.GetComponent<Weapon>().damage = (damage*(1-(lifeLived/lifetime)/2)) * Time.deltaTime;

        lifeLived += Time.deltaTime;

        if(lifeLived >= lifetime)
        {
            UnityEngine.Object.Destroy(weaponObj);

            parentObj.thingsToDelete.Add(this);
        }
    }

    public swordSwing(float lifetime, sword parentObj, GameObject obj)
    {
        this.lifetime = lifetime;
        damage = parentObj.damage;

        weaponObjPrefab = obj;

        getWeapon();

        weaponObj.transform.position = parentObj.weaponObj.transform.position;
        weaponObj.transform.rotation = parentObj.weaponObj.transform.rotation;
    }
}

[Serializable]
public class tBoneFieringSpeedUpbrade : efect
{
    public float npsLinearChange;
    public float npsExponentialChange;

    public void tBoneFieringSpeedUpbradeSettup(tBoneFieringSpeedUpbrade parent)
    {
        npsLinearChange = parent.npsLinearChange;
        npsExponentialChange = parent.npsExponentialChange;
    }
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

[Serializable]
public class swordDamageUpgrade : efect
{
    public float damageMultiplyer;
    public float damageAdder;

    public override void runtime(efect parent)
    {

        sword parentObj = (sword)parent;

        parentObj.damage += damageAdder;
        parentObj.damage *= damageMultiplyer;

        base.runtime(this);
    }
}

[Serializable]
public class efectCard
{
    public string name;
    public string desc;

    public string addto;

    [SerializeReference, SR]
    public efect efectToAdd;
    [SerializeReference, SR]
    public List<efect> requiredEfects;
    [SerializeReference, SR]
    public List<efect> withoutEfects;

}

//[Serializable]
public class weaponControler : MonoBehaviour
{
    public efect baseEfect;


    public List<efectCard> efectCards;

    public sword baseSword;
    public swordSwing baseSwordSwing;
    public trombone baseTrombone;
    public tBoneFieringSpeedUpbrade baseTBoneFieringSpeedUpbrade;




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

    public void addWeapon(efectCard card)
    {
        if (card.addto == "")
        {
            baseEfect.childEfects.Add(card.efectToAdd);
        }
        else
        {
            foreach(efect currentEfect in baseEfect.childEfects)
            {
                if(currentEfect.name == card.addto)
                {
                    currentEfect.childEfects.Add(card.efectToAdd);
                }
            }
        }
    }

    public efectCard newEfectCard()
    {
        efectCard current = efectCards[UnityEngine.Random.Range(0, efectCards.Count)];
        bool viable = true;

        foreach(efect requiredEfect in current.requiredEfects)
        {
            bool temp = false;

            foreach (efect testEfect in baseEfect.childEfects)
            {
                if(requiredEfect.name == testEfect.name)
                {
                    temp = true;
                }
            }

            if (!temp)
            {
                viable = false;
            }
        }

        foreach (efect withoutEfect in current.withoutEfects)
        {
            foreach (efect testEfect in baseEfect.childEfects)
            {
                if (withoutEfect.name == testEfect.name)
                {
                    viable = false;
                }
            }
        }

        if (viable)
        {
            return current;
        }
        else
        {
            return newEfectCard();
        }
    }

}
