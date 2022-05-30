using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*<summary>Sert � contrsuire une annexe de la base principale. Ne fait aucun d�gats et assez facile � d�truire.</summary>
*/
public class MiniBase : Unitees
{
    private (uint, Ressources) fabrication = (20, Ressources.metal);

    public (uint, Ressources) Fabrication => fabrication;


    public MiniBase() : base("MiniBase", 100, 0)
    {
    }
}
