using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
//Test
public class ConnectToServer : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //Gives the power to be connected to the server
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(); //Gives the power to create and join a room 
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Prejeu"); //Gives the power to go to the scene prejeu 
    }

}
