using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;



public class ChangeState : MonoBehaviourPun
{
    //[SerializeField] private GameObject Batiment;
    [SerializeField] private Camera theCamera;
    #region Menu
    [SerializeField] private GameObject houseMenu;
    [SerializeField] private GameObject baseMenu;
    [SerializeField] private GameObject barrackMenu;
    [SerializeField] private GameObject wallMenu;
    [SerializeField] private GameObject mineMetauxMenu;
    [SerializeField] private GameObject labMenu;
     public GameObject buildMenu;
    #endregion
    #region gameObject
    public static GameObject floor;
    public static GameObject wall;
    public static GameObject building;
    //[SerializeField] private GameObject Base;
   
    private GameObject larbin;

    //Images
    public GameObject cadenas;
    public GameObject stone;
    public GameObject stoneCost;


    #endregion
    #region bool
    private bool changing = false;
    
    private bool rightClick;
    public static bool builded = false;
    private bool buildWall;
    #endregion
    #region Vector3
    public static Vector3 floorCoord;
    
    public static Vector3 wallCoord;
    
    public static Vector3 buildingCoord;
    #endregion
    #region Material
    public Material intact;
    public Material intermediate;
    #endregion
    #region LayerMask
    public LayerMask layerWall;
    public LayerMask layerGround;
    #endregion
    #region UI
    public Button mine;
    #endregion
    
    public static List<Vector3> emptyWall =new List<Vector3>();
    public static bool baseConnected = false;
    public static (int, int) coord;
    public static PhotonView view;

    void Start()
    {
        buildMenu.SetActive(false);
        houseMenu.SetActive(false);
        baseMenu.SetActive(false);
        barrackMenu.SetActive(false);
        mineMetauxMenu.SetActive(false);
        labMenu.SetActive(false);
        rightClick = false;
        mine.enabled = false;
        view = PhotonView.Get(this);
    }
    void Update()
    {
        
        #region LeftClick
        if(Input.GetMouseButtonDown(0) && !rightClick)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.Log("Left click");
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerWall))
            {
                Debug.Log($"hit.transform.parent {hit.transform.parent}");
                Debug.Log($"hit.transform.gameObject.name {hit.transform.gameObject.name}");
                Debug.Log($"Builder dispo : {UnitSelection.Instance.mineurs.Count}");
                if (UnitSelection.Instance.mineurs.Count>0)
                {
                    larbin = UnitSelection.Instance.mineurs.Dequeue();
                    if (larbin != null && larbin.GetComponent<Builder>() != null)
                    {
                        larbin.GetComponent<Builder>().Work(hit, intermediate, changing);
                        Debug.Log("fait");
                    }
                    else
                    {
                        Debug.Log("pas fait");
                    }
                    UnitSelection.Instance.mineurs.Enqueue(larbin);
                }
                
            }
            
        }
        #endregion

        #region constructionEnCours
        if(builded)
        {
            rightClick = false;
            //buildMenu.SetActive(false);
            buildMenu.SetActive(false);
            houseMenu.SetActive(false);
            baseMenu.SetActive(false);
            barrackMenu.SetActive(false);
            wallMenu.SetActive(false);
            mineMetauxMenu.SetActive(false);
            labMenu.SetActive(false);
            builded = false;
        }
        #endregion

        #region rightCLick
        if(Input.GetMouseButtonDown(1) )
        {
            Debug.Log("Right click");
            Vector3 mousePos = Input.mousePosition;
            #region deuxièmeClickDroit
            if(rightClick)
            {
                rightClick = false;
                buildMenu.SetActive(false);
                houseMenu.SetActive(false);
                barrackMenu.SetActive(false);
                baseMenu.SetActive(false);
                wallMenu.SetActive(false);
                labMenu.SetActive(false);
                mineMetauxMenu.SetActive(false);
            }
            #endregion

            #region premierClickDroit
            else
            {
                Debug.Log("else");
                Ray ray = theCamera.ScreenPointToRay(mousePos);
                RaycastHit hit;
                rightClick = true;
               
               #region clickDroitSurLayerWall
                if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerWall))
                {
                    Debug.Log($"On a cliqué sur : {hit.transform.gameObject.name}");
                    if (hit.transform.gameObject.name == "obstruction(Clone)")
                    {
                        Debug.Log("Is a wall");
                        if (hit.transform.gameObject.tag != "reinforced")
                        {
                            wall = hit.transform.gameObject;
                            wallMenu.SetActive(true);
                            wallCoord = wall.transform.position;
                        }
                    }
                    else
                    {
                        Debug.Log("Not a wall");
                    }
                    Debug.Log($"idPlayer {ControlleurDeCam.idPlayer}");
                    Debug.Log($"tag : {hit.transform.gameObject.tag}");
                    if(hit.transform.gameObject.tag == "mine" || (hit.transform.parent != null && hit.transform.parent.tag == "mine"))
                    {
                        if (hit.transform.gameObject.name == "maisonuntied(Clone)") //On a cliqué sur une maison
                        {

                            houseMenu.SetActive(true);
                            building = hit.transform.gameObject;
                            
                        }
                        if(hit.transform.parent != null &&  (hit.transform.parent.name == "CasernePrefab(Clone)")
                        || hit.transform.gameObject.name == "CasernePrefab(Clone)") //On a cliqué sur la caserne
                        {
                            barrackMenu.SetActive(true);
                            building = hit.transform.gameObject;
                        }
                        if (hit.transform.parent != null && (hit.transform.parent.name == "Base(Clone)") || hit.transform.gameObject.name == "Base(Clone)") //On a cliqué sur la base
                        {
                            baseMenu.SetActive(true);
                            building = hit.transform.gameObject;
                        }
                        if(hit.transform.parent != null &&  (hit.transform.parent.name == "mineMetauxPrefab(Clone)")
                        || hit.transform.gameObject.name == "mineMetauxPrefab(Clone)") 
                        {
                            mineMetauxMenu.SetActive(true);
                            building = hit.transform.gameObject;
                        }
                        if(hit.transform.parent != null &&  (hit.transform.parent.name == "CentreDeRecherchePrefab(Clone)")
                        || hit.transform.gameObject.name == "CentreDeRecherchePrefab(Clone)") 
                        {
                            labMenu.SetActive(true);
                            building = hit.transform.gameObject;
                        }
                    }
                }
                #endregion

                #region ClickDroitSurLayerSol
                else 
                {
                    mine.enabled = false;
                    if (!(Physics.Raycast(ray, out RaycastHit raycastHit1, float.MaxValue, layerWall)) 
                    && Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerGround))
                    {
                        rightClick = true;
                        Debug.Log("if sol");
                        #region freeFloor
                        if(raycastHit.transform.tag == "free")
                        {
                            transform.position = raycastHit.point;
                            buildMenu.SetActive(true);
                            floor = raycastHit.transform.gameObject;
                            Debug.Log($"go.coord {((int)floor.transform.position.x)},{(int)floor.transform.position.z}");
                            buildMenu.transform.position = new Vector3(raycastHit.point.x, 3, raycastHit.point.z);
                            floorCoord = raycastHit.point;
                            //Je veux sauvegarder le sol sur lequel on a cliqué
                            if(floor.transform.position.x != floorCoord.x || floor.transform.position.z != floorCoord.z)
                            {
                                Debug.Log("go.transform.position.x != goCoord.x || go.transform.position.z != goCoord.z");
                                Collider[] collider = Physics.OverlapSphere(floor.transform.position, 3, layerGround);
                                Debug.Log($"Collider.Count {collider.Length}");
                                foreach(Collider colli in collider)
                                {
                                    Debug.Log($"colli : {colli.transform.position.x}, {colli.transform.position.z}");
                                    if((int)floorCoord.x % 3 == 0)
                                    {
                                        Debug.Log("goCoord.x % 3 == 0");
                                        if(colli.transform.position.x != (int)floorCoord.x) continue;
                                        if((int)floorCoord.z%3 == 0)
                                        {
                                            Debug.Log("goCoord.z%3 == 0");
                                            if (colli.transform.position.z != (int)floorCoord.z) continue;
                                            else 
                                            {
                                                floor = colli.transform.gameObject;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if ((int)floorCoord.z < colli.transform.position.z)
                                            {
                                                Debug.Log("goCoord.z < colli.transform.position.z");
                                                if((int)floorCoord.z > colli.transform.position.z - 3)
                                                {
                                                    Debug.Log("goCoord.z > colli.transform.position.z - 3");
                                                    floor = colli.transform.gameObject;
                                                    break;
                                                }
                                                else continue;
                                            }
                                        }
                                    }
                                    
                                    if((int)floorCoord.z % 3  == 0)
                                    {
                                        Debug.Log("goCoord.z % 3  == 0");
                                        if(colli.transform.position.z != (int)floorCoord.z) continue;
                                        else
                                        {
                                            if ((int)floorCoord.x < colli.transform.position.x)
                                            {
                                                Debug.Log("goCoord.x < colli.transform.position.x");
                                                if((int)floorCoord.x > colli.transform.position.x - 3)
                                                //goCoord.z = colli.z & colli.x -3 < goCoord.x < colli.x
                                                {
                                                    Debug.Log("if(goCoord.x > colli.transform.position.x - 3)");
                                                    floor = colli.transform.gameObject;
                                                    break;
                                                }
                                                else continue;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if ((int)floorCoord.x < colli.transform.position.x)
                                        {
                                            Debug.Log("goCoord.x < colli.transform.position.x");
                                            if((int)floorCoord.x > colli.transform.position.x - 3)
                                            //goCoord.z = colli.z & colli.x -3 < goCoord.x < colli.x
                                            {
                                                Debug.Log("goCoord.x > colli.transform.position.x - 3");
                                                if ((int)floorCoord.z < colli.transform.position.z)
                                                {
                                                    Debug.Log("goCoord.z < colli.transform.position.z");
                                                    if((int)floorCoord.z > colli.transform.position.z - 3)
                                                    {
                                                        Debug.Log("goCoord.z > colli.transform.position.z - 3");
                                                        floor = colli.transform.gameObject;
                                                        break;
                                                    }
                                                    else continue;
                                                }
                                            }
                                            else continue;
                                        }
                                        if((int)floorCoord.x > colli.transform.position.x)
                                        {
                                            Debug.Log("goCoord.x > colli.transform.position.x");
                                            if ((int)floorCoord.x < colli.transform.position.x + 3)
                                            {
                                                Debug.Log("goCoord.x < colli.transform.position.x + 3");
                                                if ((int)floorCoord.z < colli.transform.position.z)
                                                {
                                                    Debug.Log("goCoord.z < colli.transform.position.z");
                                                    if((int)floorCoord.z > colli.transform.position.z - 3)
                                                    {
                                                        Debug.Log("goCoord.z > colli.transform.position.z - 3");
                                                        floor = colli.transform.gameObject;
                                                        break;
                                                    }
                                                    else continue;
                                                }
                                            }
                                            else continue;
                                        }
                                    }
                                    
                                }
                                
                            }
                            Debug.Log("raycastHit.transform.name" + raycastHit.transform.name);
                            if(floor.name == "fer(Clone)") 
                            {
                                mine.enabled = true;
                                stone.SetActive(true);
                                stoneCost.SetActive(true);
                                cadenas.SetActive(false);
                            }
                            else 
                            {
                                mine.enabled = false;
                                cadenas.SetActive(true);
                                stone.SetActive(false);
                                stoneCost.SetActive(false);
                            }
                            Debug.Log($"Go.name : {floor.name}");
                            Debug.Log($"go.coord {floor.transform.position.x},{floor.transform.position.z}");
                            Debug.Log($"goCoord : {floorCoord.x},{floorCoord.z}");

                        }
                        else rightClick = false;
                    }
                    else 
                    {
                        rightClick = false;
                    }
                }
                
            }
            #endregion
            
        }
        #endregion
            #endregion
        #endregion    
    }

    
                        
    
    bool inTheCollider(Collider[] collider, float x, float z)
    {
        foreach (Collider colli in collider)
        {
            if (colli.transform.position.x == x && colli.transform.position.z == z)
            {
                return true;
            }
        }
        return false;
    }
    void transition()
    {
        Debug.Log("transition");
        PhotonView goView = PhotonView.Get(wall);
        goView.RPC("changeMesh", RpcTarget.All); 
        Invoke("destroyWall",5);
        
    }
    void destroyWall()
    {
        Debug.Log("destroyWall");
        if(wall.activeInHierarchy)
        {
            PhotonView goView = PhotonView.Get(wall);
            goView.RPC("delete", RpcTarget.All);
            changing = false;
            UnitSelection.Instance.mineurs.Enqueue(larbin);
            #region openTheFow
            NewGeneration.sky[(int)wall.transform.position.x/3,(int)wall.transform.position.z/3].SetActive(false);
            if(wall.transform.position.x/3 - 1 > 0) 
            {
                NewGeneration.sky[(int)wall.transform.position.x/3-1,(int)wall.transform.position.z/3].SetActive(false);
                if(wall.transform.position.z/3 - 1 > 0) NewGeneration.sky[(int)wall.transform.position.x/3-1,(int)wall.transform.position.z/3-1].SetActive(false);
                if(wall.transform.position.z/3 + 1 < NewGeneration.sky.Length) NewGeneration.sky[(int)wall.transform.position.x/3-1,(int)wall.transform.position.z/3+1].SetActive(false);
            }
            if(wall.transform.position.x/3+1<NewGeneration.sky.Length) 
            {
                NewGeneration.sky[(int)wall.transform.position.x/3+1,(int)wall.transform.position.z/3].SetActive(false);
                if(wall.transform.position.z/3 - 1 > 0) NewGeneration.sky[(int)wall.transform.position.x/3+1,(int)wall.transform.position.z/3-1].SetActive(false);
                if(wall.transform.position.z/3 + 1 < NewGeneration.sky.Length) NewGeneration.sky[(int)wall.transform.position.x/3+1,(int)wall.transform.position.z/3+1].SetActive(false);
            }
            if(wall.transform.position.z/3 - 1 > 0) NewGeneration.sky[(int)wall.transform.position.x/3,(int)wall.transform.position.z/3-1].SetActive(false);
            if(wall.transform.position.z/3 + 1 < NewGeneration.sky.Length) NewGeneration.sky[(int)wall.transform.position.x/3,(int)wall.transform.position.z/3+1].SetActive(false);
            #endregion
            Debug.Log($"destroyWall, wall.trasnform.position.x {wall.transform.position.x}, wall.z : {wall.transform.position.z}");
            
            openTheFow(wall.transform.position);
        }
        
        
    }

    public static void openTheFow(Vector3 position)
    //Quand le joueur casse un mur, il faut ouvrir son fog of war. 
    //Si le mur cassé a un voisin désactivé, il faut désactiver la celule du plafond correspondante et ainsi de suite
    {
        Debug.Log($"openTheFow, position.x {position.x}, position.z {position.z}");
        LayerMask layer = LayerMask.GetMask("Wall");
        Collider[] collider = Physics.OverlapSphere(position, 3, layer);
        Debug.Log($"emptyWall");
        NewGeneration.sky[(int)position.x/3, (int)position.z/3].SetActive(false);
        if(position.x/3 - 1 > 0) 
        {
            NewGeneration.sky[(int)position.x/3-1,(int)position.z/3].SetActive(false);
            if(position.z/3 - 1 > 0) NewGeneration.sky[(int)position.x/3-1,(int)position.z/3-1].SetActive(false);
            if(position.z/3 + 1 < NewGeneration.sky.Length) NewGeneration.sky[(int)position.x/3-1,(int)position.z/3+1].SetActive(false);
        }
        if(position.x/3+1<NewGeneration.sky.Length) 
        {
            NewGeneration.sky[(int)position.x/3+1,(int)position.z/3].SetActive(false);
            if(position.z/3 - 1 > 0) NewGeneration.sky[(int)position.x/3+1,(int)position.z/3-1].SetActive(false);
            if(position.z/3 + 1 < NewGeneration.sky.Length) NewGeneration.sky[(int)position.x/3+1,(int)position.z/3+1].SetActive(false);
        }
        if(position.z/3 - 1 > 0) NewGeneration.sky[(int)position.x/3,(int)position.z/3-1].SetActive(false);
        if(position.z/3 + 1 < NewGeneration.sky.Length) NewGeneration.sky[(int)position.x/3,(int)position.z/3+1].SetActive(false);
        if(baseConnected) 
        {
            Debug.Log($"view!=null {view!=null}");
            if(view != null) 
            {
                ChangeState.coord = ((int)position.x/3, (int)position.z/3);
                Debug.Log($"ChangeState.coord {ChangeState.coord}");
                view.RPC("openForEveryone", RpcTarget.All, ChangeState.coord.Item1, ChangeState.coord.Item2);
            }
        }
        Debug.Log($"emptyWall.Contains(position) {emptyWall.Contains(position)}");
        if(!emptyWall.Contains(position)) emptyWall.Add(position);
        float xWall = position.x;
        float zWall = position.z;
        float yWall = position.y;
        List<Vector3> freeWall = new List<Vector3>();
        freeWall.Add(new Vector3(xWall,4, zWall - 3));
        freeWall.Add(new Vector3(xWall,4, zWall + 3));
        freeWall.Add(new Vector3(xWall + 3,4, zWall));
        freeWall.Add(new Vector3(xWall - 3,4, zWall));
        if(ControlleurDeCam.idPlayer == 0)
        {
            if((position.x + 6 == NewGeneration.coordBase[1].Item1 &&
            (position.z == NewGeneration.coordBase[1].Item3
            || position.z + 3 == NewGeneration.coordBase[1].Item3
            || position.z - 3 == NewGeneration.coordBase[1].Item3))
            
            || (position.x == NewGeneration.coordBase[1].Item1 && 
            (position.z + 6 == NewGeneration.coordBase[1].Item3
            || position.z - 6 == NewGeneration.coordBase[1].Item3))
            
            || (position.x - 6 == NewGeneration.coordBase[1].Item1 && 
            (position.z == NewGeneration.coordBase[1].Item3
            || position.z + 3 == NewGeneration.coordBase[1].Item3
            || position.z - 3 == NewGeneration.coordBase[1].Item3))

            || (position.x + 3 == NewGeneration.coordBase[1].Item1 &&
            (position.z + 6 == NewGeneration.coordBase[1].Item3
            || position.z - 6 == NewGeneration.coordBase[1].Item3))
            
            || (position.x - 3 == NewGeneration.coordBase[1].Item1 &&
            (position.z + 6 == NewGeneration.coordBase[1].Item3
            || position.z - 6 == NewGeneration.coordBase[1].Item3)))
            {
                Debug.Log($"base pas loin");
                ControlleurDeCam.reactivateBase = true;
            }    
        }  
        else 
        {
            if((position.x + 6 == NewGeneration.coordBase[0].Item1 &&
            (position.z == NewGeneration.coordBase[0].Item3
            || position.z + 3 == NewGeneration.coordBase[0].Item3
            || position.z - 3 == NewGeneration.coordBase[0].Item3))
            
            || (position.x == NewGeneration.coordBase[0].Item1 && 
            (position.z + 6 == NewGeneration.coordBase[0].Item3
            || position.z - 6 == NewGeneration.coordBase[0].Item3))
            
            || (position.x - 6 == NewGeneration.coordBase[0].Item1 && 
            (position.z == NewGeneration.coordBase[0].Item3
            || position.z + 3 == NewGeneration.coordBase[0].Item3
            || position.z - 3 == NewGeneration.coordBase[0].Item3))

            || (position.x + 3 == NewGeneration.coordBase[0].Item1 &&
            (position.z + 6 == NewGeneration.coordBase[0].Item3
            || position.z - 6 == NewGeneration.coordBase[0].Item3))
            
            || (position.x - 3 == NewGeneration.coordBase[0].Item1 &&
            (position.z + 6 == NewGeneration.coordBase[0].Item3
            || position.z - 6 == NewGeneration.coordBase[0].Item3)))
            {
                Debug.Log($"base pas loin");
                ControlleurDeCam.reactivateBase = true;
            }  
        }
        
        Debug.Log($"colli.Count {collider.Length}");
        foreach (Collider colli in collider)
        {
            if(freeWall.Count == 0) break;
        
            if(colli.transform.position.x == position.x)
            {
                if(colli.transform.position.z == position.z + 3)
                {
                    if(colli.name == "obstruction(Clone)")
                    {
                        for(int i = 0; i < freeWall.Count; i++)
                        {
                            if(freeWall[i].z == position.z + 3) 
                            {
                                freeWall.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    if(colli.name == "Base(Clone)" ||(colli.transform.parent != null && colli.transform.parent.name == "Base(Clone)"))
                    {
                        Debug.Log("colli.name == base(Clone) ||(colli.transform.parent != null && colli.transform.parent.name == Base(Clone)");
                        Debug.Log($"colli.x {colli.transform.position.x}, colli.z {colli.transform.position.z}");
                        ControlleurDeCam.reactivateBase = true;
                    }
                
                    continue;
                }
                if(colli.transform.position.z == position.z - 3)
                {
                    if(colli.name == "obstruction(Clone)")
                    {
                        for(int i = 0; i < freeWall.Count; i++)
                        {
                            if(freeWall[i].z == position.z - 3) 
                            {
                                freeWall.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    continue;
                }
                continue;
            }
            if(colli.transform.position.z == position.z)
            {
                if(colli.transform.position.x == position.x + 3)
                {
                    if(colli.name == "obstruction(Clone)")
                    {
                        for(int i = 0; i < freeWall.Count; i++)
                        {
                            if(freeWall[i].x == position.x + 3) 
                            {
                                freeWall.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    continue;
                }
                if(colli.transform.position.x == position.x - 3)
                {
                    if(colli.name == "obstruction(Clone)")
                    {
                        for(int i = 0; i < freeWall.Count; i++)
                        {
                            if(freeWall[i].x == position.x - 3) 
                            {
                                freeWall.RemoveAt(i);
                                break;
                            }
                        }
                    }
                }
            }
        }
        int a = 0;
        while(a < freeWall.Count)
        {
            Vector3 vec = freeWall[a];
            Debug.Log($"vec.x {vec.x}, vec.y {vec.y}, vec.z {vec.z}");
            if(emptyWall.Contains(vec)) freeWall.Remove(vec);
            else a++;
        }
        foreach(Vector3 emptyWall in freeWall) 
        {
            openTheFow(emptyWall);
        }
    }

    
    [PunRPC]
    protected virtual void changeMesh()
    {
        Debug.Log("changeMesh");
        gameObject.GetComponent<MeshRenderer>().material = intermediate;
    }
    [PunRPC]

    void delete()
    {
        Debug.Log("delete");
        emptyWall.Add(gameObject.transform.position);
        gameObject.SetActive(false);
        
    }
    [PunRPC]
    void openForEveryone(int coord1, int coord2)
    {
        Debug.Log("openForEveryone");
        NewGeneration.sky[coord1, coord2].SetActive(false);
        if(coord1 - 1 > 0) 
        {
            NewGeneration.sky[(int)coord1 - 1,(int)coord2].SetActive(false);
            if(coord2 - 1 > 0) NewGeneration.sky[(int)coord1 - 1,(int)coord2 - 1].SetActive(false);
            if(coord2 + 1 < NewGeneration.sky.Length) NewGeneration.sky[(int)coord1 - 1,(int)coord2 + 1].SetActive(false);
        }
        if(coord1 + 1 < NewGeneration.sky.Length) 
        {
            NewGeneration.sky[(int)wall.transform.position.x/3+1,(int)coord2].SetActive(false);
            if(coord2 - 1 > 0) NewGeneration.sky[(int)coord1 + 1,(int)coord2 - 1].SetActive(false);
            if(coord2 + 1 < NewGeneration.sky.Length) NewGeneration.sky[(int)coord1 + 1,(int)coord2 + 1].SetActive(false);
        }
        if(coord2 - 1 > 0) NewGeneration.sky[(int)coord1,(int)coord2 - 1].SetActive(false);
        if(coord2 + 1 < NewGeneration.sky.Length) NewGeneration.sky[(int)coord1,(int)coord2 + 1].SetActive(false);
    }

}