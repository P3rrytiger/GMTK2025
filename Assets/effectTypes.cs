using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class effectTypes : MonoBehaviour
{
    public efect baseEfect;
    public weapon baseWeapon;

    //[SerializeReference, UseGameEventEditor]
    public  List<efect>  test  =   new();
    [SerializeReference]
    public efect test2 = new weapon();

    public efect test3 = new weapon();

}
