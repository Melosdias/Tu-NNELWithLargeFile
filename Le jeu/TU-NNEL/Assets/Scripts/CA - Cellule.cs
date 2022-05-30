using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cellules : MonoBehaviour
{
    //Attributs

    protected int taille = 3;
    protected int abscisse;
    protected int ordonnée;
    protected bool creusée = false;

    public bool Creusée => this.creusée;

    //Methodes

    public void Creuser()
    {
        creusée = true;
    }
    
}
