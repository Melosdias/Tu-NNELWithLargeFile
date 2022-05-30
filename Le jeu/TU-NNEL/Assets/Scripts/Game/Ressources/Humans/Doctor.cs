using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*<summary>Le docteur/medic n'attaque pas et a peu de vie. Cependant, il peut soigner les troupes alliées dans un certains rayon.</summary>
<remarks>Il faut donc implémenter la fonction qui permet de soigner les troupes aux alentours :)</remarks>
*/
public class Doctor : Unitees
{
    public Doctor() : base("Doctor", 80, 0)
    {
    }
}
