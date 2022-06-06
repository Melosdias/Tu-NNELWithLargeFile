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
         
        bool connected = PhotonNetwork.ConnectUsingSettings(); //Gives the power to be connected to the server
        Debug.Log($"ConnectUsingSettings : {connected}");
    }
    public override void OnConnectedToMaster()
    {
        bool joined = PhotonNetwork.JoinLobby(); //Gives the power to create and join a room 
        Debug.Log($"JoinLobby {joined}");
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Prejeu"); //Gives the power to go to the scene prejeu 
    }

}
