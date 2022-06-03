using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class DestroyBat : MonoBehaviourPun
{
    private GameObject save;
    public void destroyBat()
    {
        ChangeState.builded = true;
        save = ChangeState.go;
        Debug.Log($"Name : {save.name}");
        PhotonView view = PhotonView.Get(save);
        PhotonNetwork.Destroy(view);
        //view.RPC("suppBat",RpcTarget.All);
        
        Debug.Log($"activeInHierarchy {save.activeInHierarchy}");
        Debug.Log($"activeSelf {save.activeSelf}");
        if(save.name == "CasernePrefab(Clone)")
        {
            Batiments.destroy("Barracks");
            Ressources.pierre.addRessource(10);
            Stone.update = false;
        }
        if(save.name == "maisonuntied(Clone)")
        {
            Batiments.destroy("House");
            Ressources.pierre.addRessource(5);
            Stone.update = false;
            Ressources.population.suppRessource(10);
            Pop.update = false;
        }
        if(save.name == "mineMetauxPrefab(Clone)")
        {
            Batiments.destroy("Mine");
            Ressources.pierre.addRessource(15);
            Stone.update = false;
        }
        if(save.name == "CentreDeRecherchePrefab(Clone)")
        {
            Batiments.destroy("Labo");
            Ressources.pierre.addRessource(15);
            Ressources.metal.addRessource(5);
            Stone.update = false;
            Metal.update = false;
        }
    }
}
