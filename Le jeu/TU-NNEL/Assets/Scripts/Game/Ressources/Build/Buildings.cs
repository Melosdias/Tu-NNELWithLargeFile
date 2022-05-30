using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class Buildings : MonoBehaviourPun
{
    [SerializeField] private GameObject Batiment;
    [SerializeField] private GameObject transparentBatiment;
    private PhotonView view;
    private Vector3 position;
    private Quaternion quaternion;
    
    public float waitTime = 10;
    #region Reinforcement
    public void buildTransparentReinforcement()
    {
        if(Ressources.pierre.Quant >= 5)
        {
            
            position = new Vector3(ChangeState.goCoord.x, 2.2f,ChangeState.goCoord.z);
            GameObject go = ChangeState.go;
           
            //Cherchons où doit se positionner le renforcement :)
            LayerMask layerMask = LayerMask.GetMask("Wall");
            Collider[] collider = Physics.OverlapSphere(go.transform.position, 3, layerMask);
            Debug.Log($"collider.Length {collider.Length}");
            if (collider.Length >= 9)
            {
                Debug.Log("Can't build on this wall");
            }
            else
            {
                    bool top = true;
                    bool bot = true;
                    bool right = true;
                    bool left = true;
                    //Debug.Log($"go.transform.position {go.transform.position.x}, {go.transform.position.z}");
                    foreach (Collider colli in collider)
                    {
                        //Debug.Log($"colli.transform.position {colli.transform.position.x}, {colli.transform.position.z}");
                        if (colli.transform.position.x == go.transform.position.x + 3 && colli.transform.position.z == go.transform.position.z)
                        {
                            //Debug.Log($"right");
                            right = false;
                            continue;
                        }
                        if (colli.transform.position.x == go.transform.position.x - 3 && colli.transform.position.z == go.transform.position.z)
                        {
                            //Debug.Log($"left");
                            left = false;
                            continue;
                        }
                        if (colli.transform.position.x == go.transform.position.x && colli.transform.position.z == go.transform.position.z - 3)
                        {
                            //Debug.Log($"bot");
                            bot = false;
                            continue;
                        }
                        if (colli.transform.position.x == go.transform.position.x && colli.transform.position.z == go.transform.position.z + 3)
                        {
                            //Debug.Log($"right");
                            top = false;
                            continue;
                        }
                    }
                    bool builded = false;
                    if (right)
                    {
                        Debug.Log("Right is empty");
                        position = new Vector3(go.transform.position.x+3,0.5f, go.transform.position.z);
                        quaternion = Quaternion.identity;
                        builded = true;
                    }
                    else 
                    {
                        if (left)
                        {
                            Debug.Log("Left is empty");
                            position = new Vector3(go.transform.position.x-3,0.5f, go.transform.position.z);
                            quaternion = Quaternion.Euler(new Vector3(Batiment.transform.eulerAngles.x, Batiment.transform.eulerAngles.y+180, Batiment.transform.eulerAngles.z));
                            builded = true;
                        }
                        else
                        {
                            if(bot)
                            {
                                Debug.Log("Bot is empty");
                                position = new Vector3(go.transform.position.x,0.5f, go.transform.position.z-3);
                                quaternion = Quaternion.Euler(new Vector3(Batiment.transform.eulerAngles.x, Batiment.transform.eulerAngles.y+90, Batiment.transform.eulerAngles.z));
                                builded = true;
                            }
                            if (top)
                            {
                                Debug.Log("Top is empty");
                                position = new Vector3(go.transform.position.x,0.5f, go.transform.position.z+3);
                                quaternion = Quaternion.Euler(new Vector3(Batiment.transform.eulerAngles.x, Batiment.transform.eulerAngles.y-90, Batiment.transform.eulerAngles.z));
                                builded = true;
                            }
                        }
                    }
                    if(builded)
                    {
                        Ressources.pierre.suppRessource(5);
                        Stone.update = false;
                        Batiment.layer = 9;
                        GameObject reinforcmeent = PhotonNetwork.Instantiate(transparentBatiment.name, position , quaternion);
                        view = PhotonView.Get(reinforcmeent);
                        Debug.Log($"view : {view.ViewID}");
                        ChangeState.builded = true;
                        Debug.Log($"Reinforcement.coord {Batiment.transform.position}");
                        
                        Invoke("buildReinforcement", waitTime);
                    }
                }
                
        }
        else 
        {
            Debug.Log("Not enough stone");
            ChangeState.builded = true;
        }
    }
    public void buildReinforcement()
    {
        view.RPC("deleteBat", RpcTarget.All);
        Batiment.layer = 9;
        GameObject go = PhotonNetwork.Instantiate(Batiment.name, position, quaternion);
        Debug.Log($"Pop actuelle : {Ressources.population.Quant}");
        //Debug.Log($"Nb batiment {Batiments.ListBat.Count}");
    }
    #endregion
    
    #region House
    public void buildTransparentHouse()
    {
        if(Ressources.pierre.Quant >= 5)
        {
            Ressources.pierre.suppRessource(5);
            Stone.update = false;
            position = new Vector3(ChangeState.goCoord.x, 2.2f,ChangeState.goCoord.z);
            ChangeState.go.tag = "notFree";
            Batiment.layer = 9;
            GameObject go = PhotonNetwork.Instantiate(transparentBatiment.name, position , Quaternion.Euler(new Vector3(Batiment.transform.eulerAngles.x, Batiment.transform.eulerAngles.y+90, Batiment.transform.eulerAngles.z+90)));
            view = PhotonView.Get(go);
            Debug.Log($"view : {view.ViewID}");
            ChangeState.builded = true;
            Debug.Log($"House.coord {Batiment.transform.position}");
            Invoke("buildHouse", waitTime);
        }
        else 
        {
            Debug.Log("Not enough stone");
            ChangeState.builded = true;
        }
    }
    public void buildHouse()
    {
        view.RPC("deleteBat", RpcTarget.All);
        Batiment.layer = 9;
        GameObject go = PhotonNetwork.Instantiate(Batiment.name, position, Quaternion.Euler(new Vector3(Batiment.transform.eulerAngles.x, Batiment.transform.eulerAngles.y+90, Batiment.transform.eulerAngles.z+90)));
        Batiments.build("House");
        Ressources.population.addRessource(10);
        Pop.update = false;
        Debug.Log($"Pop actuelle : {Ressources.population.Quant}");
        //Debug.Log($"Nb batiment {Batiments.ListBat.Count}");
    }
    #endregion
    #region Quarry
    public void buildTransparentQuarry()
    {
        if(Ressources.pierre.Quant >= 10)
        {
            Ressources.pierre.suppRessource(10);
            Stone.update = false;
            position = new Vector3(ChangeState.goCoord.x, 2.2f,ChangeState.goCoord.z);
            ChangeState.go.tag = "notFree";
            Batiment.layer = 9;
            GameObject go = PhotonNetwork.Instantiate(transparentBatiment.name, position,Quaternion.identity);
            view = PhotonView.Get(go);
            ChangeState.builded = true;
            Debug.Log($"House.coord {Batiment.transform.position}");
            Invoke("buildQuarry", waitTime);
        }
        else 
        {
            Debug.Log("Not enough stone");
            ChangeState.builded = true;
        }
    }
    public void buildQuarry()
    {
        view.RPC("deleteBat", RpcTarget.All);
        Batiment.layer = 9;
        GameObject go = PhotonNetwork.Instantiate(Batiment.name, position, Quaternion.identity);
        Batiments.build("Quarry");
    }
    #endregion
    #region Mine
    public void buildTransparentMine()
    {
        if(Ressources.pierre.Quant >= 15)
        {
            Ressources.pierre.suppRessource(5);
            Stone.update = false;
            position = new Vector3(ChangeState.goCoord.x, 2.2f,ChangeState.goCoord.z);
            ChangeState.go.tag = "notFree";
            Batiment.layer = 9;
            GameObject go = PhotonNetwork.Instantiate(transparentBatiment.name, position, Quaternion.identity);
            view = PhotonView.Get(go);
            ChangeState.builded = true;
            Debug.Log($"House.coord {Batiment.transform.position}");
            Invoke("buildMine", waitTime);
        }
        else 
        {
            Debug.Log("Not enough stone");
            ChangeState.builded = true;
        }
    }
    public void buildMine()
    {
        view.RPC("deleteBat", RpcTarget.All);
        Batiment.layer = 9;
        GameObject go = PhotonNetwork.Instantiate(Batiment.name, position, Quaternion.identity);
        Batiments.build("Mine");
    }
    #endregion
    #region Labo
    public void buildTransparentLabo()
    {
        if(Ressources.pierre.Quant>=15 && Ressources.metal.Quant>=5)
        {
            Ressources.pierre.suppRessource(15);
            Stone.update = false;
            Ressources.metal.suppRessource(5);
            Metal.update = false;
            position = new Vector3(ChangeState.goCoord.x, 2.2f,ChangeState.goCoord.z);
            ChangeState.go.tag = "notFree";
            Batiment.layer = 9;
            GameObject go = PhotonNetwork.Instantiate(transparentBatiment.name, position, Quaternion.identity);
            view = PhotonView.Get(go);
            ChangeState.builded = true;
            Debug.Log($"Labo.coord {Batiment.transform.position}");
            Invoke("buildHLabo", waitTime);
        }
        else
        {
            if (Ressources.pierre.Quant<15)
            {
                Debug.Log("Not enough stone");
            }
            else
            {
                Debug.Log("Not enough iron");
            }
        }
    }
    public void buildLabo()
    {
        view.RPC("deleteBat", RpcTarget.All);
        Batiment.layer = 9;
        GameObject go = PhotonNetwork.Instantiate(Batiment.name, position, Quaternion.identity);
        Batiments.build("Labo");
        
    }
    #endregion
    #region Barracks
    public void buildTransparentBarracks()
    {
        if(Ressources.pierre.Quant >= 10)
        {
            Ressources.pierre.suppRessource(10);
            Stone.update = false;
            position = new Vector3(ChangeState.goCoord.x, 1.1f,ChangeState.goCoord.z);
            Batiment.layer = 9;
            ChangeState.go.tag = "notFree";
            GameObject go = PhotonNetwork.Instantiate(transparentBatiment.name, position , Quaternion.identity);
            view = PhotonView.Get(go);
            Debug.Log($"viewId : {view.ViewID}");
            ChangeState.builded = true;
            Debug.Log($"Barracks.coord {Batiment.transform.position}");
            Invoke("buildBarracks", waitTime);
        }
        else
        {
            Debug.Log("Not enough stone");
        }
    }
    public void buildBarracks()
    {
        
        view.RPC("deleteBat", RpcTarget.All);
        Batiment.layer = 9;
        GameObject go = PhotonNetwork.Instantiate(Batiment.name, position, Quaternion.identity);
        Batiments.build("Barracks");
        
        
    }
    #endregion
    #region RobotBay
    public void buildTransparentRobotBay()
    {
        if(Ressources.pierre.Quant>=15 && Ressources.metal.Quant>=10)
        {
            Ressources.pierre.suppRessource(15);
            Stone.update = false;
            Ressources.metal.suppRessource(10);
            Metal.update = false;
            position = new Vector3(ChangeState.goCoord.x, 1.1f,ChangeState.goCoord.z);
            ChangeState.go.tag = "notFree";
            Batiment.layer = 9;
            GameObject go = PhotonNetwork.Instantiate(transparentBatiment.name, position, Quaternion.identity);
            view = PhotonView.Get(go);
            ChangeState.builded = true;
            Debug.Log($"RobotBay.coord {Batiment.transform.position}");
            Invoke("buildRobotBay", waitTime);
        }
        else
        {
            if (Ressources.pierre.Quant<15)
            {
                Debug.Log("Not enough stone");
            }
            else
            {
                Debug.Log("Not enough iron");
            }
        }
    }
    public void buildRobotBay()
    {
        view.RPC("deleteBat", RpcTarget.All);
        Batiment.layer = 9;
        GameObject go = PhotonNetwork.Instantiate(Batiment.name, position, Quaternion.identity);
        Batiments.build("RobotBay");
        
    }
    #endregion
    #region PunRPC
    [PunRPC]
    void deleteBat()
    {
        Debug.Log("deleteBat");
        Destroy(transparentBatiment);
        //transparentBatiment.SetActive(false);
    }
    #endregion
}
