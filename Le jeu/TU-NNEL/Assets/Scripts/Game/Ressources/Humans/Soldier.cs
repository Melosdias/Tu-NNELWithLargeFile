using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*<summary> Le soldat est l'unit�e militaire de base, il a une arme � feu et ne peut attaquer que sur des distances courtes � moyennes.</summary>
*/
public class Soldier : Unitees
{
    public Soldier() : base("Soldier", 100, 20)
    {
    }
}
