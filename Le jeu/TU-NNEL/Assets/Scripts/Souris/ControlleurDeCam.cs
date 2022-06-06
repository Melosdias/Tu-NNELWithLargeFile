using UnityEngine;
using System.Runtime;
using System;
using Photon.Pun;

public class ControlleurDeCam : MonoBehaviour
{
    #region Emmeline's part
    public static bool init;
    public static int idPlayer;
    [SerializeField]  private GameObject Base;
    private GameObject go;
    public Renderer myRenderer;
    public static bool reactivateBase;
    //public static 
    #endregion


    [SerializeField] private float vitesse = 10f;           //vitesse de la caméra. (conseil: ne pas trop augmenter sinon unity crash etoutetout)
    [SerializeField] private float limite;           //Hauteur limite à laquelle peut accéder la caméra.
    [SerializeField] private float vitesseDuScroll = 2000f; //Vitesse de zoom
    [SerializeField] public NewGeneration script;

    void Start()
    {
        reactivateBase = false;
    }



    void Update()
    {
        Vector3 position = transform.position;

        //Determiner l'emplacement de base du joueur
        
        if(!init && NewGeneration.coordBase.Count > CreateAndJoinRooms.player -1 && CreateAndJoinRooms.player -1 >= 0)
        {
            idPlayer = CreateAndJoinRooms.player - 1;
            Debug.Log($"CreateAndJoinRooms.player -1 ; {CreateAndJoinRooms.player -1}");
            Debug.Log($"Wait.CoordCam.Count : {Wait.CoordCam.Count}");
            Debug.Log($"NewGeneration.coordBase.Count : {NewGeneration.coordBase.Count}");
            position.x = NewGeneration.coordBase[idPlayer].Item1;
            position.y = 15;
            position.z =  NewGeneration.coordBase[idPlayer].Item3 -5;
            init = true;
            
            go = PhotonNetwork.Instantiate(Base.name, new Vector3(NewGeneration.coordBase[idPlayer].Item1,NewGeneration.coordBase[idPlayer].Item2,NewGeneration.coordBase[idPlayer].Item3), Quaternion.identity);
            go.tag = "mine";
            go.layer = 9;
            PhotonView goView = PhotonView.Get(go);
            goView.RPC("hideToOther", RpcTarget.OthersBuffered);//Les autres joueurs ne peuvent pas voir la base (sino elle dépasse du fow >.>)
            
            #region ouverturedufogofwar
            //Quand on a placé la base du joueur, il peut aprrécier que son fog of war soit ouvert au dessus de sa base
            NewGeneration.sky[(int)position.x/3,(int)NewGeneration.coordBase[idPlayer].Item3/3].SetActive(false);
            ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3,(int)NewGeneration.coordBase[idPlayer].Item3/3].transform.position);
            if(position.x/3 - 1 > 0) 
            {
                NewGeneration.sky[(int)position.x/3-1,(int)NewGeneration.coordBase[idPlayer].Item3/3].SetActive(false);
                ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3-1,(int)NewGeneration.coordBase[idPlayer].Item3/3].transform.position);
                if(NewGeneration.coordBase[idPlayer].Item3/3 - 1 > 0) 
                {
                    NewGeneration.sky[(int)position.x/3-1,(int)NewGeneration.coordBase[idPlayer].Item3/3-1].SetActive(false);
                    ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3-1,(int)NewGeneration.coordBase[idPlayer].Item3/3-1].transform.position);
                    if(NewGeneration.coordBase[idPlayer].Item3/3 - 2 > 0) 
                    {
                        NewGeneration.sky[(int)position.x/3-1,(int)NewGeneration.coordBase[idPlayer].Item3/3-2].SetActive(false);
                        //ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3-1,(int)NewGeneration.coordBase[idPlayer].Item3/3-2].transform.position);

                    }
                }
                
                if(NewGeneration.coordBase[idPlayer].Item3/3 + 1 < NewGeneration.sky.Length) 
                {
                    NewGeneration.sky[(int)position.x/3-1,(int)NewGeneration.coordBase[idPlayer].Item3/3+1].SetActive(false);
                    ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3-1,(int)NewGeneration.coordBase[idPlayer].Item3/3+1].transform.position);
                    if(NewGeneration.coordBase[idPlayer].Item3/3 + 2 < NewGeneration.sky.Length) 
                    {
                        NewGeneration.sky[(int)position.x/3-1,(int)NewGeneration.coordBase[idPlayer].Item3/3+2].SetActive(false);
                        //ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3-1,(int)NewGeneration.coordBase[idPlayer].Item3/3+2].transform.position);
                    }
                }
                
            }
            if(position.x/3-2>0) 
            {
                NewGeneration.sky[(int)position.x/3-2,(int)NewGeneration.coordBase[idPlayer].Item3/3].SetActive(false);
                //ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3-2,(int)NewGeneration.coordBase[idPlayer].Item3/3].transform.position);
                if(NewGeneration.coordBase[idPlayer].Item3/3 - 1 > 0) 
                {
                    NewGeneration.sky[(int)position.x/3-2,(int)NewGeneration.coordBase[idPlayer].Item3/3-1].SetActive(false);
                    //ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3-2,(int)NewGeneration.coordBase[idPlayer].Item3/3-1].transform.position);
                    if(NewGeneration.coordBase[idPlayer].Item3/3 - 2 > 0) 
                    {
                        NewGeneration.sky[(int)position.x/3-2,(int)NewGeneration.coordBase[idPlayer].Item3/3-2].SetActive(false);
                        //ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3-2,(int)NewGeneration.coordBase[idPlayer].Item3/3-2].transform.position);
                    }
                }
                
                if(NewGeneration.coordBase[idPlayer].Item3/3 + 1 < NewGeneration.sky.Length) 
                {
                    NewGeneration.sky[(int)position.x/3-2,(int)NewGeneration.coordBase[idPlayer].Item3/3+1].SetActive(false);
                    //ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3-2,(int)NewGeneration.coordBase[idPlayer].Item3/3+1].transform.position);
                    if(NewGeneration.coordBase[idPlayer].Item3/3 + 2 < NewGeneration.sky.Length) 
                    {
                        NewGeneration.sky[(int)position.x/3-2,(int)NewGeneration.coordBase[idPlayer].Item3/3+2].SetActive(false);
                        //ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3-2,(int)NewGeneration.coordBase[idPlayer].Item3/3+2].transform.position);
                    }
                }
                
            }
            if(position.x/3+1<NewGeneration.sky.Length) 
            {
                NewGeneration.sky[(int)position.x/3+1,(int)NewGeneration.coordBase[idPlayer].Item3/3].SetActive(false);
                ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3+1,(int)NewGeneration.coordBase[idPlayer].Item3/3].transform.position);
                if(NewGeneration.coordBase[idPlayer].Item3/3 - 1 > 0) 
                {
                    NewGeneration.sky[(int)position.x/3+1,(int)NewGeneration.coordBase[idPlayer].Item3/3-1].SetActive(false);
                    ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3+1,(int)NewGeneration.coordBase[idPlayer].Item3/3-1].transform.position);
                    if(NewGeneration.coordBase[idPlayer].Item3/3 - 2 > 0) 
                    {
                        NewGeneration.sky[(int)position.x/3+1,(int)NewGeneration.coordBase[idPlayer].Item3/3-2].SetActive(false);
                        //ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3+1,(int)NewGeneration.coordBase[idPlayer].Item3/3-2].transform.position);
                    }
                }
                
                if(NewGeneration.coordBase[idPlayer].Item3/3 + 1 < NewGeneration.sky.Length) 
                {
                    NewGeneration.sky[(int)position.x/3+1,(int)NewGeneration.coordBase[idPlayer].Item3/3+1].SetActive(false);
                    ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3+1,(int)NewGeneration.coordBase[idPlayer].Item3/3+1].transform.position);
                    if(NewGeneration.coordBase[idPlayer].Item3/3 + 2 < NewGeneration.sky.Length) 
                    {
                        NewGeneration.sky[(int)position.x/3+1,(int)NewGeneration.coordBase[idPlayer].Item3/3+2].SetActive(false);
                        //ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3+1,(int)NewGeneration.coordBase[idPlayer].Item3/3+2].transform.position);
                    }
                }
                
            }
            if(position.x/3+2<NewGeneration.sky.Length) 
            {
                NewGeneration.sky[(int)position.x/3+2,(int)NewGeneration.coordBase[idPlayer].Item3/3].SetActive(false);
                //ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3+2,(int)NewGeneration.coordBase[idPlayer].Item3/3].transform.position);
                if(NewGeneration.coordBase[idPlayer].Item3/3 - 1 > 0) 
                {
                    NewGeneration.sky[(int)position.x/3+2,(int)NewGeneration.coordBase[idPlayer].Item3/3-1].SetActive(false);
                    //ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3+2,(int)NewGeneration.coordBase[idPlayer].Item3/3-1].transform.position);
                    if(NewGeneration.coordBase[idPlayer].Item3/3 - 2 > 0) 
                    {
                        NewGeneration.sky[(int)position.x/3+2,(int)NewGeneration.coordBase[idPlayer].Item3/3-2].SetActive(false);
                        //ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3+2,(int)NewGeneration.coordBase[idPlayer].Item3/3-2].transform.position);
                    }
                }
                
                if(NewGeneration.coordBase[idPlayer].Item3/3 + 1 < NewGeneration.sky.Length) 
                {
                    NewGeneration.sky[(int)position.x/3+2,(int)NewGeneration.coordBase[idPlayer].Item3/3+1].SetActive(false);
                    //ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3+2,(int)NewGeneration.coordBase[idPlayer].Item3/3+1].transform.position);
                    if(NewGeneration.coordBase[idPlayer].Item3/3 + 2 < NewGeneration.sky.Length) 
                    {
                        NewGeneration.sky[(int)position.x/3+2,(int)NewGeneration.coordBase[idPlayer].Item3/3+2].SetActive(false);
                        //ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3+2,(int)NewGeneration.coordBase[idPlayer].Item3/3+2].transform.position);
                    }
                }
                
            }
            if(NewGeneration.coordBase[idPlayer].Item3/3 - 1 > 0) 
            {
                NewGeneration.sky[(int)position.x/3,(int)NewGeneration.coordBase[idPlayer].Item3/3-1].SetActive(false);
                ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3,(int)NewGeneration.coordBase[idPlayer].Item3/3-1].transform.position);
                if(NewGeneration.coordBase[idPlayer].Item3/3 - 2 > 0) 
                {
                    NewGeneration.sky[(int)position.x/3,(int)NewGeneration.coordBase[idPlayer].Item3/3-2].SetActive(false);
                    //ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3,(int)NewGeneration.coordBase[idPlayer].Item3/3-2].transform.position);
                }
            }
            if(NewGeneration.coordBase[idPlayer].Item3/3 + 1 < NewGeneration.sky.Length) 
            {
                NewGeneration.sky[(int)position.x/3,(int)NewGeneration.coordBase[idPlayer].Item3/3+1].SetActive(false);
                ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3,(int)NewGeneration.coordBase[idPlayer].Item3/3+1].transform.position);
                if(NewGeneration.coordBase[idPlayer].Item3/3 + 2 < NewGeneration.sky.Length) 
                {
                    NewGeneration.sky[(int)position.x/3,(int)NewGeneration.coordBase[idPlayer].Item3/3+2].SetActive(false);
                    //ChangeState.emptyWall.Add(NewGeneration.sky[(int)position.x/3,(int)NewGeneration.coordBase[idPlayer].Item3/3+2].transform.position);
                }
            }
            
            #endregion
        
        }
        if(reactivateBase)
        {
            //Quand un joueur a découvert la base de son adversaire, ils peuvent tout les deux voir la base de l'autre
            
            PhotonView goView = PhotonView.Get(go);
            goView.RPC("unhideToOther", RpcTarget.All);
            reactivateBase = false;
            ChangeState.baseConnected = true;
        }

        
        if(this.name != "Base(Clone)")
        {
            float s = 3 * (float) script.size;
    
        
            float zoom = Input.GetAxis("Mouse ScrollWheel");

            if (Input.GetKey("z"))
            {
                position.z += vitesse * Time.deltaTime;
            }
            if (Input.GetKey("s"))
            {
                position.z += -vitesse * Time.deltaTime;
            }
            if (Input.GetKey("q"))
            {
                position.x += -vitesse * Time.deltaTime;
            }
            if (Input.GetKey("d"))
            {
                position.x += vitesse * Time.deltaTime;
            }

            position.y -= zoom * vitesseDuScroll * Time.deltaTime;

            
            
            position.x = Mathf.Clamp(position.x, 0f, s);

            position.z = Mathf.Clamp(position.z, 0f, s);

            position.y = Mathf.Clamp(position.y, 3.2f, limite);  //mathf.clamp sert a fixer une limite a la position x, y ou z.
            
            transform.position = position;
        }
    }

    #region PunRPCs
    [PunRPC]

    void hideToOther()
    {
        Debug.Log("hideToOther");
        gameObject.SetActive(false);
    }

    [PunRPC]
    void unhideToOther()
    {
        Debug.Log("unhideToOther");
        gameObject.SetActive(true);
        GameObject[] list =  FindObjectsOfType(typeof(GameObject), true) as GameObject[];
        int compteur = 0;
        foreach (var go in list)
        {
            if(go.name == "Base(Clone)") 
            {
                go.SetActive(true);
                compteur++;
            }
            if (compteur == 2) break;
        }
        if(idPlayer == 0)
        {
            Debug.Log("OpenTheFow calls");
            if(NewGeneration.coordBase[1].Item1 - 3 > 0)
            {
                ChangeState.openTheFow(new Vector3(NewGeneration.coordBase[1].Item1 - 3, NewGeneration.coordBase[1].Item2, NewGeneration.coordBase[1].Item3));
                if(NewGeneration.coordBase[1].Item3 - 3 > 0) ChangeState.openTheFow(new Vector3(NewGeneration.coordBase[1].Item1 - 3, NewGeneration.coordBase[1].Item2, NewGeneration.coordBase[1].Item3 - 3));
                if(NewGeneration.coordBase[1].Item3 + 3 < NewGeneration.sky.Length) ChangeState.openTheFow(new Vector3(NewGeneration.coordBase[1].Item1 - 3, NewGeneration.coordBase[1].Item2, NewGeneration.coordBase[1].Item3 + 3));
            }
            if(NewGeneration.coordBase[1].Item1 + 3 < NewGeneration.sky.Length) 
            {
                ChangeState.openTheFow(new Vector3(NewGeneration.coordBase[1].Item1 + 3, NewGeneration.coordBase[1].Item2, NewGeneration.coordBase[1].Item3));
                if(NewGeneration.coordBase[1].Item3 - 3 > 0) ChangeState.openTheFow(new Vector3(NewGeneration.coordBase[1].Item1 + 3, NewGeneration.coordBase[1].Item2, NewGeneration.coordBase[1].Item3 - 3));
                if(NewGeneration.coordBase[1].Item3 + 3 < NewGeneration.sky.Length) ChangeState.openTheFow(new Vector3(NewGeneration.coordBase[1].Item1 + 3, NewGeneration.coordBase[1].Item2, NewGeneration.coordBase[1].Item3 + 3));
            }
            if(NewGeneration.coordBase[1].Item3 - 3 > 0) ChangeState.openTheFow(new Vector3(NewGeneration.coordBase[1].Item1, NewGeneration.coordBase[1].Item2, NewGeneration.coordBase[1].Item3 - 3));
            if(NewGeneration.coordBase[1].Item3 + 3 < NewGeneration.sky.Length) ChangeState.openTheFow(new Vector3(NewGeneration.coordBase[1].Item1, NewGeneration.coordBase[1].Item2, NewGeneration.coordBase[1].Item3 + 3));
        }
        else
        {
            Debug.Log("OpenTheFow calls");
            if(NewGeneration.coordBase[0].Item1 - 3 > 0)
            {
                ChangeState.openTheFow(new Vector3(NewGeneration.coordBase[0].Item1 - 3, NewGeneration.coordBase[0].Item2, NewGeneration.coordBase[0].Item3));
                if(NewGeneration.coordBase[0].Item3 - 3 > 0) ChangeState.openTheFow(new Vector3(NewGeneration.coordBase[0].Item1 - 3, NewGeneration.coordBase[0].Item2, NewGeneration.coordBase[0].Item3 - 3));
                if(NewGeneration.coordBase[0].Item3 + 3 < NewGeneration.sky.Length) ChangeState.openTheFow(new Vector3(NewGeneration.coordBase[0].Item1 - 3, NewGeneration.coordBase[0].Item2, NewGeneration.coordBase[1].Item3 + 3));
            }
            if(NewGeneration.coordBase[0].Item1 + 3 < NewGeneration.sky.Length) 
            {
                ChangeState.openTheFow(new Vector3(NewGeneration.coordBase[0].Item1 + 3, NewGeneration.coordBase[0].Item2, NewGeneration.coordBase[0].Item3));
                if(NewGeneration.coordBase[0].Item3 - 3 > 0) ChangeState.openTheFow(new Vector3(NewGeneration.coordBase[0].Item1 + 3, NewGeneration.coordBase[0].Item2, NewGeneration.coordBase[0].Item3 - 3));
                if(NewGeneration.coordBase[0].Item3 + 3 < NewGeneration.sky.Length) ChangeState.openTheFow(new Vector3(NewGeneration.coordBase[0].Item1 + 3, NewGeneration.coordBase[0].Item2, NewGeneration.coordBase[0].Item3 + 3));
            }
            if(NewGeneration.coordBase[0].Item3 - 3 > 0) ChangeState.openTheFow(new Vector3(NewGeneration.coordBase[0].Item1, NewGeneration.coordBase[0].Item2, NewGeneration.coordBase[0].Item3 - 3));
            if(NewGeneration.coordBase[0].Item3 + 3 < NewGeneration.sky.Length) ChangeState.openTheFow(new Vector3(NewGeneration.coordBase[0].Item1, NewGeneration.coordBase[0].Item2, NewGeneration.coordBase[0].Item3 + 3));
        }
    }
    #endregion
}