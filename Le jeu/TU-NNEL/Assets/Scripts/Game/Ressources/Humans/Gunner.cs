using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*<summary>Tant qu'il n'a pas déposé son arme, il ne peut pas attquer et a peu de vie. 
Par contre, il fait très mal une fois qu'il l'a psoé et a beaucoup de vie mais ne peut plus bouger. A une portée moyenne à longue.</summary>
*/
public class Gunner : Unitees
{
    private bool active;
    public bool Active => active;
    public Gunner() : base("Gunner", 0, 0)
    {
        if (active) //S'il a posé son arme
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
