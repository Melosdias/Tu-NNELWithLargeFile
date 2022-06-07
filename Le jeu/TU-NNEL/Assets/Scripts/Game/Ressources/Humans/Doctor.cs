using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/**
*<summary>Le docteur/medic n'attaque pas et a peu de vie. Cependant, il peut soigner les troupes alliées dans un certains rayon.</summary>
<remarks>Il faut donc implémenter la fonction qui permet de soigner les troupes aux alentours :)</remarks>
*/
public class Doctor : Unitees
{
    List<GameObject> allies = new List<GameObject>();
    public Doctor() : base("Doctor", 80, 0, 0)
    {

    }
    public void Update()
    {
        if (Health <= 0)
            photonView.RPC("UpdateLive", RpcTarget.All);
        foreach (GameObject ally in allies)
        {
            ally.GetComponent<Unitees>().Health += 0.5;
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