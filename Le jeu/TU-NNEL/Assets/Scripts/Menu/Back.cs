using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Back : MonoBehaviour
{
    
    public void back()
    {
        SceneManager.LoadScene("Menu");
        PhotonNetwork.Disconnect();
    }

}
