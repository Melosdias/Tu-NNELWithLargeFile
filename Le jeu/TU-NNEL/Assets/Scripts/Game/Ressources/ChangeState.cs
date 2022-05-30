using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;


public class ChangeState : MonoBehaviourPun
{
    //[SerializeField] private GameObject Batiment;
    [SerializeField] private Camera theCamera;
    #region BatMenu
    [SerializeField] private GameObject houseMenu;
    [SerializeField] private GameObject baseMenu;
    [SerializeField] private GameObject barrackMenu;
    [SerializeField] private GameObject wallMenu;
    #endregion
    public Material intact;
    public Material intermediate;
    public static GameObject go;
    public static Vector3 goCoord;
    private bool changing = false;
    public GameObject buildMenu;
    private bool rightClick;
    public static bool builded = false;
    public LayerMask minerai;
    public LayerMask wall;

    private GameObject larbin;
    private bool buildWall;



    void Start()
    {
        buildMenu.SetActive(false);
        houseMenu.SetActive(false);
        baseMenu.SetActive(false);
        barrackMenu.SetActive(false);
        rightClick = false;
        minerai = LayerMask.GetMask("minerai");
        wall = LayerMask.GetMask("Wall");
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !rightClick)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.Log("Left click");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, wall))
            {
                Debug.Log($"hit.transform.parent {hit.transform.parent}");
                Debug.Log($"hit.transform.gameObject.name {hit.transform.gameObject.name}");
                Debug.Log($"Builder dispo : {UnitSelection.Instance.mineurs.Count}");
                if (UnitSelection.Instance.mineurs.Count>0)
                {
                    larbin = UnitSelection.Instance.mineurs.Dequeue();
                    if (larbin != null && larbin.GetComponent<Builder>() != null)
                    {
                        larbin.GetComponent<Builder>().Work(hit);
                        Debug.Log("fait");
                    }
                    else
                    {
                        Debug.Log("pas fait");
                    }
                    if (hit.transform.name == "pierre") //Si on mine de la pierre
                    {
                        if (hit.transform.gameObject.tag == "Intact")
                        {
                            hit.transform.gameObject.tag = "Intermediate";
                            //Debug.Log($"Tag {Minerais.tag}");
                            Ressources.mine(Ressources.pierre, larbin, hit);

                        }
                        else
                        {
                            Ressources.mine(Ressources.pierre, larbin, hit);
                        }
                        //Debug.Log($"Destroyed");
                        PhotonNetwork.Destroy(hit.transform.gameObject);
                    }
                    if (hit.transform.name == "metal") //Si on mine du métal
                    {
                        if (hit.transform.gameObject.tag == "Intact")
                        {
                            hit.transform.gameObject.tag = "Intermediate";
                            //Debug.Log($"Tag {Minerais.tag}");
                            Ressources.mine(Ressources.metal, larbin, hit);
                            hit.transform.gameObject.GetComponent<MeshRenderer>().material = intermediate;
                        }
                        else
                        {
                            Ressources.mine(Ressources.metal, larbin, hit);
                            //Debug.Log($"Destroyed");
                            PhotonNetwork.Destroy(hit.transform.gameObject);
                        }
                    }
                    if (hit.transform.name == "coeurEnergie") //Si on mine un coeur d'energie
                    {
                        if (hit.transform.gameObject.tag == "Intact")
                        {
                            hit.transform.gameObject.tag = "Intermediate";
                            //Debug.Log($"Tag {Minerais.tag}");
                            Ressources.mine(Ressources.coeurEnergie, larbin, hit);
                            hit.transform.gameObject.GetComponent<MeshRenderer>().material = intermediate;
                        }
                        else
                        {
                            Ressources.mine(Ressources.coeurEnergie, larbin, hit);
                            //Debug.Log($"Destroyed");
                            PhotonNetwork.Destroy(hit.transform.gameObject);
                        }
                    }
                    if (hit.transform.name == "obstruction(Clone)") //Si c'est juste un mur à casser
                    {
                        if (!changing && hit.transform.tag == "Intact")
                        {

                            go = hit.transform.gameObject;
                            LayerMask layerMask = LayerMask.GetMask("Wall");
                            Collider[] collider = Physics.OverlapSphere(go.transform.position, 3, layerMask);
                            Debug.Log($"collider.Length {collider.Length}");
                            if (collider.Length >= 9)
                            {
                                Debug.Log("Can't hit this wall");
                            }
                            else
                            {

                                if (collider.Length < 5)
                                {
                                    changing = true;
                                    Invoke("transition", 1);
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
                                        if (colli.transform.position.x == go.transform.position.x && colli.transform.position.z == go.transform.position.z + 3)
                                        {
                                            //Debug.Log($"bot");
                                            bot = false;
                                            continue;
                                        }
                                        if (colli.transform.position.x == go.transform.position.x && colli.transform.position.z == go.transform.position.z - 3)
                                        {
                                            //Debug.Log($"right");
                                            top = false;
                                            continue;
                                        }
                                    }
                                    if (top || bot || left || right)
                                    {
                                        changing = true;
                                        Debug.Log("wallhit");
                                        Invoke("transition", 1);
                                    }
                                    else
                                    {
                                        Debug.Log("Can't hit this wall");
                                    }
                                }
                            }
                        }
                    }
                    UnitSelection.Instance.mineurs.Enqueue(larbin);
                }
                
            }
            
        }
        if(builded)
        {
            rightClick = false;
            //buildMenu.SetActive(false);
            buildMenu.SetActive(false);
            houseMenu.SetActive(false);
            baseMenu.SetActive(false);
            barrackMenu.SetActive(false);
            wallMenu.SetActive(false);
            builded = false;
        }
        if(Input.GetMouseButtonDown(1) )
        {
            Debug.Log("Right click");
            Vector3 mousePos = Input.mousePosition;
            //Debug.Log($"Mpuse pos {mousePos}");
            if(rightClick)
            {
                rightClick = false;
                buildMenu.SetActive(false);
                houseMenu.SetActive(false);
                barrackMenu.SetActive(false);
                baseMenu.SetActive(false);
                wallMenu.SetActive(false);
            }
            else
            {
                //Debug.Log("else");
                LayerMask layerWall = LayerMask.GetMask("Wall");
                LayerMask layerGround = LayerMask.GetMask("Sol");
                Ray ray = theCamera.ScreenPointToRay(mousePos);
                RaycastHit hit;
                rightClick = true;
                if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerWall))
                {
                    Debug.Log($"On a cliqué sur : {hit.transform.gameObject.name}");
                    if (hit.transform.gameObject.name == "obstruction(Clone)")
                    {
                        Debug.Log("Is a wall");
                        if (hit.transform.gameObject.tag != "reinforced")
                        {
                            go = hit.transform.gameObject;
                            wallMenu.SetActive(true);
                            goCoord = go.transform.position;
                        }
                    }
                    else
                    {
                        Debug.Log("Not a wall");
                    }
                    if (hit.transform.gameObject.name == "maisonuntied(Clone)")
                    {
                        houseMenu.SetActive(true);
                        go = hit.transform.gameObject;
                        
                    }
                    if(hit.transform.parent != null &&  (hit.transform.parent.name == "CasernePrefab(Clone)")
                    || hit.transform.gameObject.name == "CasernePrefab(Clone)")
                    {
                        barrackMenu.SetActive(true);
                        go = hit.transform.gameObject;
                    }
                    if (hit.transform.parent != null && (hit.transform.parent.name == "Base(Clone)") || hit.transform.gameObject.name == "Base(Clone)")
                    {
                        baseMenu.SetActive(true);
                        go = hit.transform.gameObject;
                    }
                    
                }
                else 
                {
                    if (!(Physics.Raycast(ray, out RaycastHit raycastHit1, float.MaxValue, layerWall)) 
                    && Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerGround))
                    {
                        Debug.Log("if");
                        transform.position = raycastHit.point;
                        buildMenu.SetActive(true);
                        buildMenu.transform.position = new Vector3(raycastHit.point.x, 3, raycastHit.point.z);
                        goCoord = raycastHit.point;
                    }
                    else 
                    {
                        rightClick = false;
                    }
                }
                
            }
        }
        
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
        PhotonView goView = PhotonView.Get(go);

        Debug.Log($"go.name {go.name}");

        Debug.Log($"go.tag before {go.tag}");
        goView.RPC("changeMesh", RpcTarget.All); 
        go.tag = "Intermediate";
        Invoke("destroyWall",5);
        Debug.Log($"go.tag after {go.tag}");
    }
    void destroyWall()
    {
        Debug.Log("destroyWall");
        PhotonView goView = PhotonView.Get(go);
        goView.RPC("delete", RpcTarget.All);
        Debug.Log($"go.activeSelf : {go.activeSelf}");
        changing = false;
        UnitSelection.Instance.mineurs.Enqueue(larbin);

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
        gameObject.SetActive(false);
    }


}