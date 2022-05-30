using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//<summary>
//I'm not sur this will be usefull but for now I need it 
//This class will save all stat about the palyer : ressources, batiments..
//</summary>
public class Player : MonoBehaviour
{
    private List<Ressources> ressources; //List of ressources of the player, please don't add a ressources if it is already in the list
    private static List<Batiments> batiments; //List of buildings of the player, same
    private int popMax;
    public List<Ressources> Ressources => ressources;
    public static List<Batiments> Batiments => batiments;
    public int PopMax => popMax;
    public Player()
    {
        this.ressources = new List<Ressources>();
        Player.batiments = new List<Batiments>();
        this.popMax = 10;
    }
    
}
