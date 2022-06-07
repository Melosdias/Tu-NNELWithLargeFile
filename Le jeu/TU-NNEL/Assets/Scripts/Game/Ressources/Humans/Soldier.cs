using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/**
*<summary> Le soldat est l'unitée militaire de base, il a une arme à feu et ne peut attaquer que sur des distances courtes à moyennes.</summary>
*/
public class Soldier : Unitees
{
    public SphereCollider spherecollider;
    public List<GameObject> cibles = new List<GameObject>();
    public GameObject cible;
    public int vitesse2 = 1;
    
    public Soldier() : base("Soldier", 100, 20, 60)
    {
    }

    private void Update()
    {
        if (cible == null)
        {
            if (cibles.Count>0)
                cible = cibles[cibles.Count - 1];
        }
        if (Health <= 0)
        {
            //Destroy(gameObject);
            photonView.RPC("UpdateLive", RpcTarget.All);
        } 
        if (cible != null && vitesse2 == 0)
        {
            GameObject balle = (GameObject)Instantiate(Resources.Load("Bullet"), transform);
            balle.transform.position = this.transform.position;
            balle.GetComponent<Projectile>().cible = cible;
            balle.GetComponent<Projectile>().TypeDeCible = cible.tag;
            balle.GetComponent<Projectile>().damage = (int)Damage;
        }
        vitesse2 = (vitesse2 + 1) % (int)Attackspeed;
    }

    private void OnTriggerEnter(Collider sphereCollider)
    {
        if (sphereCollider.CompareTag("P1"))
        {
            cibles.Add(sphereCollider.gameObject);
        }
    }
    private void OnTriggerExit(Collider sphereCollider)
    {
        if (sphereCollider.CompareTag("P1"))
        {
            if (cibles.Contains(sphereCollider.gameObject))
            {
                cibles.Remove(sphereCollider.gameObject);
            }
            if (cible = sphereCollider.gameObject)
                cible = null;
        }
    }
}