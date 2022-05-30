using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*<summary>Tant qu'il n'a pas d�pos� son arme, il ne peut pas attquer et a peu de vie. 
Par contre, il fait tr�s mal une fois qu'il l'a pso� et a beaucoup de vie mais ne peut plus bouger. A une port�e moyenne � longue.</summary>
*/
public class Gunner : Unitees
{
    private bool active;
    public bool Active => active;
    public Gunner() : base("Gunner", 0, 0)
    {
        if (active) //S'il a pos� son arme
        {
            this.Damage = 150;
            this.Health = 300;
        }
        else
        {
            this.Damage = 0;
            this.Health = 80;
        }
    }
}
