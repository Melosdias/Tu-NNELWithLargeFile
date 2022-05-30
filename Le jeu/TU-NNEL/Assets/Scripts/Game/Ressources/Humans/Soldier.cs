using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*<summary> Le soldat est l'unitée militaire de base, il a une arme à feu et ne peut attaquer que sur des distances courtes à moyennes.</summary>
*/
public class Soldier : Unitees
{
    public Soldier() : base("Soldier", 100, 20)
    {
    }
}
