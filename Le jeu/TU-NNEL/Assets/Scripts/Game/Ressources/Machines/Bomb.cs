using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Quand il a enfin le coeur d'energie, le joueur a le choix entre fabriquer une bombe qui fait �normement de d�gats ou construire un super-m�ca

/**
*<summary>Fait beaucoup de d�gats d'un coup dans un rayon suffisament grand pour bien faire mal � l'adversaire.</summary>
*/
public class Bomb : Unitees
{
    private (uint, Ressources) fabrication = (1, Ressources.coeurEnergie);
    public (uint, Ressources) Fabrication => fabrication;
    public Bomb() : base("Bomb", 1000, 10000)
    {
    }
}
