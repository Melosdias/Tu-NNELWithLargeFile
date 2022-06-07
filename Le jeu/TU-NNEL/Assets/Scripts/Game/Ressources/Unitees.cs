using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/**
*<summary> Cette classe gère les unitées du jeu. 
Si chaque unitées a rôle différent, elle sont toute regroupées dans la ressource population.
Dans le jeu, les unitées peuvent soit être des builders soit des militaires
Chaque unitées est définie par son nom, son nombre de pv, sle nombre de dommage qu'elle peut emettre en un coup. Ses spécialités sont dans les classes filles.
Les robots sont aussi gérés grâce à cette classe, mais ils ne rentrent pas dans la population !!</summary>
*/
public abstract class Unitees : MonoBehaviourPun
{
    private string nameUnit;
    [SerializeField]
    private double health;
    private double damage;
    private double attackspeed;
    public Unitees(string name, double health, double damage, double attackspeed)
    {
        this.nameUnit = name;
        this.health = health;
        this.damage = damage;
        this.attackspeed = attackspeed;
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
    public double Attackspeed
    {
        get => this.attackspeed;
        set { this.attackspeed = value; }
    }
    public void Takedmg(double damage)
    {
        this.health -= damage;
    }
}