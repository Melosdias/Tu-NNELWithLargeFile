using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class BackGame : MonoBehaviour
{
    public void LeftRoom()
    {
        PhotonNetwork.LoadLevel("Prejeu");
        //SceneManager.LoadScene("Prejeu");
        CreateAndJoinRooms.Name = "Prejeu";
    }
}
