using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*<summary>Le d�molisseur sert � faire exploser les murs. Il n'est pas cens� attaquer mais il peut faire un peu de d�gats pour se d�fendre (mais clairement, 
il fera pas tr�s mal) et a peu de vie.</summary>
*<remarks>Ne sahcant pas comment les murs sont g�r�s pour l'instant, j'ai pr�f�r� ne pas impl�menter la fonction qui permet de faire exploser les murs</remarks>
*/
public class Demolisher : Unitees
{
    public Demolisher() : base("Demolisher", 50, 5, 0)
    {
    }
}
