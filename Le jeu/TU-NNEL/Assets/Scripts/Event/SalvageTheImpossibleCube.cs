using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SalvageTheImpossibleCube : MonoBehaviourPun
{
    [SerializeField] GameObject CubeIntact;
    [SerializeField] GameObject CubeDestroyed;
    [SerializeField] GameObject CanvasEvent;
    [SerializeField] int respierre = 5;
    [SerializeField] int resmetal = 15;
    [SerializeField] int enercore = 1;
    // Start is called before the first frame update
   public void Effect() 
   {
        Ressources.pierre.addRessource((uint)respierre);
        Ressources.metal.addRessource((uint)resmetal);
        Ressources.coeurEnergie.addRessource((uint)enercore);
        Stone.update = false;
        Metal.update = false;
        EnergyCore.update = false;
        CanvasEvent.SetActive(false);
        PhotonView view = TheCubeScript.view;
        view.RPC("deleteCube", RpcTarget.All);
        PhotonNetwork.Instantiate(CubeDestroyed.name, TheCubeScript.position, Quaternion.Euler(new Vector3(CubeDestroyed.transform.eulerAngles.x, CubeDestroyed.transform.eulerAngles.y+90, CubeDestroyed.transform.eulerAngles.z+90)));        
   }
   [PunRPC]
    void deleteCube()
    {
        Debug.Log("deleteCube");
        Destroy(CubeIntact);
    }
}
