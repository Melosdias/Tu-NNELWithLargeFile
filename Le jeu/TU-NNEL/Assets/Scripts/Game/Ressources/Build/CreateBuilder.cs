using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CreateBuilder : MonoBehaviourPun
{
    [SerializeField] public GameObject builder;
    public void create()
    {
        GameObject batisseur = PhotonNetwork.Instantiate(builder.name, new Vector3(NewGeneration.coordBase[CreateAndJoinRooms.player -1].Item1+3, 1.1f, NewGeneration.coordBase[CreateAndJoinRooms.player -1].Item3-3), Quaternion.identity);
        Builder newBatisseur = new Builder(batisseur);
        batisseur.AddComponent<PhotonView>();
        batisseur.tag = "mine";
        ChangeState.builded = true;
        Ressources.pierre.suppRessource(5);
        Stone.update = false;
    }
}
