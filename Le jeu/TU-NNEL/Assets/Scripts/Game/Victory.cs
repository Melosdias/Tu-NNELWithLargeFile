using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Victory : MonoBehaviour
{
    [SerializeField] public GameObject victoryScreen;
    [SerializeField] public GameObject defeatScreen;

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1) 
        //Si l'autre joueur a été déconnecté
        {
            victoryScreen.SetActive(true);
        }
        if(NewGeneration.coordBase.Count == 1)
        //S'il n'y a plus qu'une base debout
        {
            if(NewGeneration.coordBaseVithId[0].Item2 == NewGeneration.coordBase[0]) 
            {
                if (ControlleurDeCam.idPlayer == 0) victoryScreen.SetActive(true);
                else defeatScreen.SetActive(true);
            }
            else
            {
                if (ControlleurDeCam.idPlayer == 1) victoryScreen.SetActive(true);
                else defeatScreen.SetActive(true);
            }
        }
    }
}
