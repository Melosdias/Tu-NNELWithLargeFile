using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


/**
*<summary> Gère les batiments du jeu. 
Dans le jeu, il y a deux types de batiments : ceux qui génèrent les ressources, et ceux qui génèrenet le unitées de combat.
Chaque batimeent est défini par son nom et le joueur a qui il appartient</summary>
*/
public abstract class Batiments : MonoBehaviourPun
{
    private string nameBat; 
    //private static Player player;
    private static List<Batiments> listBat;
    private static Dictionary<string, int> dicBat = new Dictionary<string, int>();

    public Batiments(string name) //, Player player
    {
        this.nameBat = name;
        //Batiments.player = player;
        listBat = new List<Batiments>();
        dicBat = new Dictionary<string, int>();
        
    }
    public string Name => nameBat;
    public static List<Batiments> ListBat => listBat;
    public static Dictionary<string, int> DicBat => dicBat;
    
    //public static Player Player => player;
    /**
    *<summary>Pour construire les bâtiments, il faut d'abord les ajouter dans la liste des batiments du joueur.</summary>
    *<param name = "batiment">Le batiment à construire.</param>
    */
    public static void build(string batiment)
    {
        Debug.Log($"batiment : {batiment}");
        //Player.Batiments.Add(batiment);
        if(dicBat.ContainsKey(batiment))
        {
            dicBat[batiment] ++;
        }
        else
        {
            dicBat.Add(batiment, 1);
        }
        /*listBat.Add(batiment);
        foreach (Batiments bat in listBat)
        {
            Debug.Log($"Name : {bat.Name}");
        }*/
        foreach (KeyValuePair<string, int> pair in dicBat)
        {
            Debug.Log($"{pair.Key} : {pair.Value}");
        }
    }


    /**
    *<summary>Pour détruire les bâtiments, il faut d'abord les supprimer de la liste des batiments du joueur.</summary>
    *<param name = "batiment">Le batiment à supprimer.</param>
    */
    public static void destroy(string batiment)
    {
        //Player.Batiments.Remove(batiment);
        /*listBat.Remove(batiment);*/
        dicBat[batiment] --;
    }
}


/**
*<summary> Crée la mine. 
Génère les pierres</summary>
*/
public class Quarry : Batiments
{
    private Ressources ressources;
    public Resources Pierre;
    public PhotonView viewId;
    public Quarry(PhotonView viewId) : base("Quarry")
    {
        this.ressources = Ressources.pierre;
        this.viewId = viewId;
    }
}


#region Ressource
/**
*<summary> Crée la mine. 
Génère le métal</summary>
*/
public class Mine : Batiments
{
    private Ressources ressources;
    public Resources Metal;
    public PhotonView viewId;
    public Mine() : base("Mine")
    {
        this.ressources = Ressources.metal;
        this.viewId = viewId;
    }
}

/**
*<summary> Crée les maisons. 
Génère la population</summary>
*/
public class House : Batiments
{
    private Ressources ressources;
    public Resources Population;
    public PhotonView viewId;
    public House(PhotonView viewId) : base("House")
    {
        this.ressources = Ressources.population;
        this.viewId = viewId;
    }
}
#endregion

/**
*<summary> Crée le laboratoire. 
Sert à gérer l'amélioration des unitées, et ne génère donc aucune ressource.</summary>
*/
public class Labo : Batiments
{
    public PhotonView viewId;
    public Labo(PhotonView viewId) : base("Labo")
    {
        this.viewId = viewId;
    }
}


#region Militaire
/**
*<summary> Crée la caserne. 
Sert à générer les unitées militaires humaines.</summary>
*/
public class Barracks : Batiments
{
    public PhotonView viewId;
    private List<String> uniteePossible = new List<String>(){"Soldier", "Hand2hand", "Demolisher", "Gunner", "Doctor","Buffer"};
    public List<String> Unitees => uniteePossible;
    public Barracks(PhotonView viewId) : base("Barracks")
    {
        this.viewId = viewId;
    }
}

/**
*<summary> Crée la baie robotique. 
Sert à générer les unitées militaires robotiques.</summary>
*/
public class RobotBay : Batiments
{
    public PhotonView viewId;
    private List<String> uniteePossible = new List<String>(){"Meca", "Tank", "MiniBase"};
    public List<String> Unitees => uniteePossible;
    public RobotBay(PhotonView viewId) : base("RobotBay")
    {
        this.viewId = viewId;
    }
}


/**
*<summary> Crée la base. 
Probablement inutile</summary>
*/
public class Base : Batiments
{
    public PhotonView viewId;
    public Base(PhotonView viewId) : base("Base")
    {
        this.viewId = viewId;
    }
}

#endregion
