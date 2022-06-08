using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SalvageTheImpossibleCube : MonoBehaviourPun
{
    [SerializeField] GameObject CubeIntact;
    public static PhotonView viewIntact;
    [SerializeField] GameObject CubeDestroyed;
    [SerializeField] GameObject CanvasEvent;
    [SerializeField] int respierre = 5;
    [SerializeField] int resmetal = 15;
    [SerializeField] int enercore = 1;
    // Start is called before the first frame update
   public void Effect() 
   {
        viewIntact = TheCubeScript.view;
        Debug.Log($"viewIntact viewID : {viewIntact.ViewID}");
        Ressources.pierre.addRessource((uint)respierre);
        Ressources.metal.addRessource((uint)resmetal);
        Ressources.coeurEnergie.addRessource((uint)enercore);
        Stone.update = false;
        Metal.update = false;
        EnergyCore.update = false;
        CanvasEvent.SetActive(false);
        PhotonView view = CubeIntact.GetPhotonView();
        Debug.Log($"cubeIntact viewID : {view.ViewID}");
        PhotonNetwork.AllocateViewID(this.photonView);
        Debug.Log($"this viewID : {this.photonView.ViewID}");
        
        viewIntact.RPC("deleteCube", RpcTarget.All);
        viewIntact.RPC("buildExploredCube", RpcTarget.All);
   }
   [PunRPC]
    void deleteCube()
    {
        Debug.Log("deleteCube");
        Destroy(CubeIntact);
    }
    [PunRPC]
    void buildExploredCube()
    {
        Debug.Log("buildExploredCube");
        GameObject go = Instantiate(CubeDestroyed, TheCubeScript.position, Quaternion.Euler(new Vector3(CubeDestroyed.transform.eulerAngles.x, CubeDestroyed.transform.eulerAngles.y+90, CubeDestroyed.transform.eulerAngles.z+90)));        
        go.layer = 9;
        go.AddComponent<PhotonView>();
    }
}
