using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/**
*<summary>Le buffer est une troupe qui bouste les troupes alliées. Alors je ne sais pas trop dans quel sens (booste l'attaque, la vie ou la vitesse voir les trois).
Il peut attaquer sur une distance courte à moyenne.</summary>
<remarks>Il faut donc implémenter la fonction qui permet de booster les troupes aux alentours :)</remarks>
*/
public class Buffer : Unitees
{
    List<GameObject> allies = new List<GameObject>();
    public Buffer() : base("Buffer", 130, 80, 0)
    {
    }
    

    public void Update()
    {
        if (Health<=0)
            photonView.RPC("UpdateLive", RpcTarget.All);
        foreach (GameObject ally in allies)
        {
            ally.GetComponent<Unitees>().Damage += 5;
            ally.GetComponent<Unitees>().Attackspeed -= ally.GetComponent<Unitees>().Attackspeed / 10;
        }
    }

    private void OnTriggerEnter(Collider sphereCollider)
    {
        if (sphereCollider.CompareTag("P1"))
        {
            allies.Add(sphereCollider.gameObject);
        }
    }
    private void OnTriggerExit(Collider sphereCollider)
    {
        if (sphereCollider.CompareTag("P1"))
        {
            if (allies.Contains(sphereCollider.gameObject))
            {
                allies.Remove(sphereCollider.gameObject);
            }
        }
    }
}