using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*<summary>Version am�lior�e du m�ca de base. Fait beaucoup de d�gats, se d�place un peu plus vite mais a toujours besoin d'un humain.</summary>
*/
public class SuperMecha : Unitees
{
    private Unitees human;
    private (uint, Ressources) fabrication = (1, Ressources.coeurEnergie);
    public Unitees Human => human;
    public (uint, Ressources) Fabrication => fabrication;
    public SuperMecha() : base("Mecha", 10000, 0)
    {
        if (human != null)
        {
            if (human is Soldier || human is Hand2hand || human is Demolisher || human is Gunner || human is Buffer || human is Doctor)
                this.Damage = 750;
            else throw new System.Exception("Only human in supermecha");
        }
        else
        {
            this.Damage = 0;
        }
    }
}
