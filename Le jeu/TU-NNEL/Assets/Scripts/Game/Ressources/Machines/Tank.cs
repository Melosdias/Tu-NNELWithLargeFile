using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*<summary>Gros véhicule faisant plein de dégats et diffcile à détruire.
Besoin de 5 humains à l'intérieur pour être fonctionnel et lent.</summary>
*/
public class Tank : Unitees
{
    private List<Unitees> human;
    private (uint, Ressources) fabrication = (20, Ressources.metal);

    public List<Unitees> Human => human;
    public (uint, Ressources) Fabrication => fabrication;


    public Tank() : base("Tank", 500, 0)
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
