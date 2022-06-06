using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Wait : MonoBehaviourPun
{
    public static int seed;
    public static List<(int,int, int)> CoordCam = new List<(int, int, int)>();
    [SerializeField] public GameObject UI;
    [SerializeField] public Text nameScene;
    [SerializeField] public GameObject Generate;

    void Start()
    {
        Debug.Log($"Is master client {PhotonNetwork.IsMasterClient}");
        UI.SetActive(false);
        nameScene.text += PhotonNetwork.CurrentRoom.Name;
        if(PhotonNetwork.IsMasterClient) 
        {
            Debug.Log("Create");
            photonView.RPC("SetSeed", RpcTarget.AllBuffered, Random.Range(1000,99999999));
            photonView.RPC("CoordBase", RpcTarget.AllBuffered);
        }
        //else photonView.RPC("CoordBase", RpcTarget.AllBuffered);
        
    }
    void Update()
    {

        if (CreateAndJoinRooms.player == 2)
        {
            //if(!PhotonNetwork.IsMasterClient) photonView.RPC("CoordBase", RpcTarget.All);
            photonView.RPC("UpdateTagMsg", RpcTarget.All);
            
            foreach (var coord in CoordCam)
            {
                Debug.Log($"x : {coord.Item1}, z : {coord.Item3}");
            }
        }
    }
 
    public void UpdateTag( )
    {
        
        Debug.Log("UpdateTag");

        if (!photonView.IsMine)
            {
                return ;
            }

        photonView.RPC("UpdateTagMsg", RpcTarget.All);
    }
    [PunRPC]

    protected virtual void SetSeed(int SeedValue)
    {

        Debug.Log("PunRPC SetSeed");
        seed = SeedValue;
        NewGeneration.seed = seed;
        Debug.Log($"Seed : {seed}");
    }
    [PunRPC]
    void UpdateTagMsg()
    {

        Debug.Log("UpdateTagMsg");
        gameObject.SetActive(false);
        UI.SetActive(true);
        Generate.SetActive(true);
    }
    [PunRPC]
    void CoordBase()
    {

        Debug.Log("PunRPC CoordBase");
        Wait.CoordCam = NewGeneration.coordBase;
        Debug.Log($"CoordCam.Count{CoordCam.Count}");
    }
    
}

