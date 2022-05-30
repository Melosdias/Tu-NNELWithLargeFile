using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cellules : MonoBehaviour
{
    //Attributs

    protected int taille = 3;
    protected int abscisse;
    protected int ordonn�e;
    protected bool creus�e = false;

    public bool Creus�e => this.creus�e;

    //Methodes

    public void Creuser()
    {
        creus�e = true;
    }
    
}
