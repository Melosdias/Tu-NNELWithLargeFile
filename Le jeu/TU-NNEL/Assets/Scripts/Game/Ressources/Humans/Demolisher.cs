using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*<summary>Le démolisseur sert à faire exploser les murs. Il n'est pas censé attaquer mais il peut faire un peu de dégats pour se défendre (mais clairement, 
il fera pas très mal) et a peu de vie.</summary>
*<remarks>Ne sahcant pas comment les murs sont gérés pour l'instant, j'ai préféré ne pas implémenter la fonction qui permet de faire exploser les murs</remarks>
*/
public class Demolisher : Unitees
{
    public Demolisher() : base("Demolisher", 50, 5)
    {
    }
}
