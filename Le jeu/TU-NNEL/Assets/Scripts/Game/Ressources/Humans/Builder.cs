using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

/**
*<summary> Le builder construit les batiments. Il a tr�s peu de vie, ne peut pas attaquer mais est le seul a pouvoir miner des ressources 
(et peut-�tre construire les batiments) </summary>
<remarks>La fonction qui premet de g�rer l'�tat d'un bloc de ressources est dans la classe ChangeState</remarks>
<remarks>D�s le d�but du jeu, on a un builder</remarks>
*/
public class Builder : Unitees
{
    private GameObject go;
    public GameObject GoBuilder => go;
    public bool task = false;
    RaycastHit hit;
    public bool changing;
    public Material intermediate;
    public Builder(GameObject go) : base("Builder", 200, 0, 0)
    {
        this.go = go;
    }
    private void Start()
    {
        UnitSelection.Instance.mineurs.Enqueue(this.gameObject);
        this.Health = 200;
    }
    private void Update()
    {
        if (this.Health <= 0)
        {
            photonView.RPC("UpdateLive", RpcTarget.All);
        }
    }
    public void Work(RaycastHit h, Material inter, bool change)
    {
        NavMeshAgent sbire = this.gameObject.GetComponent<NavMeshAgent>();
        sbire.SetDestination(h.point);
        task = true;
        hit = h;
        changing = change;
        intermediate = inter;
    }

    /*private void Start()
    {
        UnitSelection.Instance.mineurs.Enqueue(go);

    }*/
}
