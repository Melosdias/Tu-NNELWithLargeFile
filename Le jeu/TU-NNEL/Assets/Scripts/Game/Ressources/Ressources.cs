using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

/**
*<summary> Cette classe gère les ressources du jeu. 
Dans le jeu on a la population (builder et soldats confondus, ils sont différencier dans la classe unitée), la pierre (ressource de base),
le métal (ressource plus rare) et le noyau d'ernergie (ressource de fin de jeu). 
Chaque ressource est caractérisée par son nopm, sa quantité actuelle (par rapport au joueur) et son obtention par seconde </summary>
*/
public  class Ressources : MonoBehaviour
{
    private string nameRessource; 
    private double quantActuelle;
    private double obtentionPerSec;
    private static List<Ressources> ListRessource;
    

    public Ressources(string name, double obtention, double quant)
    {
        this.nameRessource = name;
        this.quantActuelle = quant;
        this.obtentionPerSec = obtention;
        ListRessource = new List<Ressources>();
        
    }
    public string Name => nameRessource;
    public double Quant
    {
        get => this.quantActuelle;
        set {this.quantActuelle = value;}
    }
    public double Obtention => obtentionPerSec;
    public List<Ressources> LstRessource => ListRessource;
    
    //Toute les ressources disponibles sont crées ici
    //Bien entendu, les chiffres definis ici ne sont pas définitifs, il faudra les adapter pour que le jeu soit agréable
    
    public static Ressources population = new Ressources("population", 0.25, 1); 
    public static Ressources pierre = new Ressources("pierre", 1,10);
    
    public static Ressources metal = new Ressources("metal",  0.25,0);

    public static Ressources coeurEnergie = new Ressources("coeurEnergie", 0.0125,0);
    
    
    /**
    *<summary>Gère le minage de la ressource. Il s'écoule dix secondes entre le moment où le bloc de ressource a été séléctionné 
    et le moment où il change d'état.</summary>
    *<param name = "ressources">Correspond à la ressource minée.</param>
    */
    public async static void mine(Ressources ressources, GameObject larbin, RaycastHit minerai)
    {
        while (ressources.Quant < ressources.Quant + (ressources.Obtention *10))
        {
            if (Vector3.Distance(larbin.transform.position, minerai.point) < 4)
                break;
            ressources.Quant+= ressources.Obtention;
            await Task.Delay(500);
        }
    }

    /**
    *<summary>Gère la génération de ressources par les bâtiments. Tant qu'il y a des bâtiments, les ressources sont continuellement générées.</summary>
    *<param name = "nbBat">Correspond au nombre de bâtiment actuel</param>
    */
    public static void generation(string type, uint nbBat )
    {
        
        if(type == "pierre")
        {
            
            if (nbBat > 0)
            {
                Ressources.pierre.Quant+= (Ressources.pierre.Obtention*nbBat);
                Stone.update = false;
            }
        }
        if(type == "metal")
        {
            
            if (nbBat > 0)
            {
                Ressources.metal.Quant+= (Ressources.metal.Obtention*nbBat);
                Metal.update = false;
            }
        }   
        
    } 
    

    /**
    *<summary>Pour ajouter des ressources (suite à un évènement spécial par exemple)</summary>
    *<param name="nb">Le nombre de ressources à ajouter</param>
    */

    public  void addRessource(uint nb)
    {
        this.Quant+=nb;
    }
    
    /**
    *<summary>Pour supprimer des ressources (suite à un évènement spécial par exemple)</summary>
    *<param name="nb">Le nombre de ressources à ajouter</param>
    */
    public void suppRessource(uint nb)
    {
        this.Quant-=nb;
        if (this.Quant <0)
        {
            this.Quant = 0;
        }
    }
}

