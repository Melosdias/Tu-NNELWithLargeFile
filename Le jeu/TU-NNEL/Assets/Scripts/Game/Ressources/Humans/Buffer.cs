using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*<summary>Le buffer est une troupe qui bouste les troupes alli�es. Alors je ne sais pas trop dans quel sens (booste l'attaque, la vie ou la vitesse voir les trois).
Il peut attaquer sur une distance courte � moyenne.</summary>
<remarks>Il faut donc impl�menter la fonction qui permet de booster les troupes aux alentours :)</remarks>
*/
public class Buffer : Unitees
{
    public Buffer() : base("Buffer", 130, 80)
    {
    }
}
