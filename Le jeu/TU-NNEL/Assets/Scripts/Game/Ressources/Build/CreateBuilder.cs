using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CreateBuilder : MonoBehaviourPun
{
    [SerializeField] public GameObject builder;
    public void create()
    {
        this.photonView.RPC("createBuilder", RpcTarget.All);
    }
    [PunRPC]
    void createBuilder()
    {
        GameObject batisseur = Instantiate(builder, new Vector3(NewGeneration.coordBase[CreateAndJoinRooms.player -1].Item1+3, 1.1f, NewGeneration.coordBase[CreateAndJoinRooms.player -1].Item3-3), Quaternion.identity);
        batisseur.AddComponent<PhotonView>();
        PhotonNetwork.AllocateViewID(batisseur.GetPhotonView());
        batisseur.tag = "mine";
        Builder newBatisseur = new Builder(batisseur);
        ChangeState.builded = true;
        Ressources.pierre.suppRessource(5);
        Stone.update = false;
    }

}
