using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Stats : MonoBehaviourPun
{
    public int PV;
    public int Degats;
    public int LenteurAttaque;
    public GameObject Cible;
    private int vitesse2 = 0;

    void Update()
    {
        if (PV <= 0)
        {
            //Destroy(gameObject);
            photonView.RPC("UpdateLive", RpcTarget.All);
        }
        if (Cible != null && vitesse2 == 0)
        {
            GameObject balle = (GameObject)Instantiate(Resources.Load("Bullet"), transform);
            balle.transform.position = this.transform.position;
            balle.GetComponent<Projectile>().cible = Cible;
            balle.GetComponent<Projectile>().TypeDeCible = Cible.tag;
        }
        vitesse2 = (vitesse2 + 1) % LenteurAttaque;
    }

    [PunRPC]
    void UpdateLive()
    {
        //Debug.Log("UpdateLive");
        gameObject.SetActive(false);
    }
}
