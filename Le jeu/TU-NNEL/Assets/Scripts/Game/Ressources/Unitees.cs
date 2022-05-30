using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*<summary> Cette classe gère les unitées du jeu. 
Si chaque unitées a rôle différent, elle sont toute regroupées dans la ressource population.
Dans le jeu, les unitées peuvent soit être des builders soit des militaires
Chaque unitées est définie par son nom, son nombre de pv, sle nombre de dommage qu'elle peut emettre en un coup. Ses spécialités sont dans les classes filles.
Les robots sont aussi gérés grâce à cette classe, mais ils ne rentrent pas dans la population !!</summary>
*/
public abstract class Unitees : MonoBehaviour
{
    private string nameUnit; 
    private double health;
    private double damage;
    public Unitees(string name, double health, double damage)
    {
        this.nameUnit = name;
        this.health = health;
        this.damage = damage;
    }
    public string Name => nameUnit;
    public double Health
    {
        get => this.health;
        set {this.health = value;}
    }
    public double Damage
    {
        get => this.damage;
        set {this.damage = value;}
    }
}