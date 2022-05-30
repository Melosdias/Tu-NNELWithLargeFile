using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public Builder(GameObject go) : base("Builder", 20, 0)
    {
        this.go = go;
    }
    private void Start()
    {
        UnitSelection.Instance.mineurs.Enqueue(this.gameObject);
    }
    public void Work(RaycastHit r)
    {
        NavMeshAgent sbire = this.gameObject.GetComponent<NavMeshAgent>();
        sbire.SetDestination(r.point);
        
    }
    /*private void Start()
    {
        UnitSelection.Instance.mineurs.Enqueue(go);

    }*/
}
