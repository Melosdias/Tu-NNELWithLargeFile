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
    }
}
