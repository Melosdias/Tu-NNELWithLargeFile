using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*<summary>Gros v�hicule faisant plein de d�gats et diffcile � d�truire.
Besoin de 5 humains � l'int�rieur pour �tre fonctionnel et lent.</summary>
*/
public class Tank : Unitees
{
    private List<Unitees> human;
    private (uint, Ressources) fabrication = (20, Ressources.metal);

    public List<Unitees> Human => human;
    public (uint, Ressources) Fabrication => fabrication;


    public Tank() : base("Tank", 500, 0, 10)
    {
        if (human.Count == 5)
        {
            foreach (Unitees hum in human)
            {
                if (hum is Soldier || hum is Hand2hand || hum is Demolisher || hum is Gunner || hum is Buffer || hum is Doctor) continue;
                else throw new System.Exception("Only human in meca");
            }
            this.Damage = 150;
        }
        else
        {
            this.Damage = 0;
        }
    }
}
