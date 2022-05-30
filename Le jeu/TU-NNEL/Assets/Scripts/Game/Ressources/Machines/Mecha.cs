using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*<summary>Gros robot qui fait beaucoup de dégats et a beauocup de vie. 
Coute cher à fabriquer, se déplace très lentement et a besoin d'un humain pour attaquer et bouger.</summary>
*/
public class Mecha : Unitees
{
    private Unitees human;
    private (uint, Ressources) fabrication = (15, Ressources.metal);
    public Unitees Human => human;
    public (uint, Ressources) Fabrication => fabrication;
    public Mecha() : base("Mecha", 500, 0)
    {
        if (human != null)
        {
            if (human is Soldier || human is Hand2hand || human is Demolisher || human is Gunner || human is Buffer || human is Doctor)
                this.Damage = 100;
            else throw new System.Exception("Only human in mecha");
        }
        else
        {
            this.Damage = 0;
        }
    }
}
