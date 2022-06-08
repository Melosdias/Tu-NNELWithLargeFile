using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

/**
*<summary> Le builder construit les batiments. Il a tr�s peu de vie, ne peut pas attaquer mais est le seul a pouvoir miner des ressources 
(et peut-�tre construire les batiments) </summary>
<remarks>La fonction qui premet de g�rer l'�tat d'un bloc de ressources est dans la classe ChangeState</remarks>
<remarks>D�s le d�but du jeu, on a un builder</remarks>
*/
public class Builder : Unitees
{
    private GameObject go;
    public GameObject GoBuilder => go;
    public bool task = false;
    RaycastHit hit;
    public bool changing;
    public Material intermediate;
    Animator an;
    public Builder(GameObject go) : base("Builder", 200, 0, 0)
    {
        this.go = go;
    }
    private void Start()
    {
        UnitSelection.Instance.mineurs.Enqueue(this.gameObject);
        this.Health = 200;
        an = GetComponent<Animator>();
    }
    private void Update()
    {
        if (this.Health <= 0)
        {
            photonView.RPC("UpdateLive", RpcTarget.All);
        }
        if (task == true)
        {
            if (Vector3.Distance(hit.point, this.gameObject.GetComponent<NavMeshAgent>().transform.position)<10)
            {
                task = false;
                an.SetBool("isWalking", false);
                Break(hit, changing, intermediate);
                Debug.Log("arrivé");
            }
        }
    }
    public void Work(RaycastHit h, Material inter, bool change)
    {
        if(this.tag == "mine")
        {
            NavMeshAgent sbire = this.gameObject.GetComponent<NavMeshAgent>();
            sbire.SetDestination(h.point);
            an.SetBool("isWalking", true);
            task = true;
            hit = h;
            changing = change;
            intermediate = inter;
        }
    }
    public void Break(RaycastHit hit, bool changing, Material intermediate)
    {
        Debug.Log("Break");
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
                        UnitSelection.Instance.mineurs.Dequeue();
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
                            UnitSelection.Instance.mineurs.Dequeue();
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
    }
    public void transition()
    {
        Debug.Log("transition");
        PhotonView goView = PhotonView.Get(go);

        Debug.Log($"go.name {go.name}");

        Debug.Log($"go.tag before {go.tag}");
        goView.RPC("changeMesh", RpcTarget.All);
        //go.tag = "Intermediate";
        Invoke("destroyWall", 5);
        //Debug.Log("destroyed techniquement");
        Debug.Log($"go.tag after {go.tag}");
    }

    void destroyWall()
    {
        Debug.Log("destroyWall");
        if(go.activeInHierarchy)
        {
            PhotonView goView = PhotonView.Get(go);
            goView.RPC("delete", RpcTarget.All);
            changing = false;
            
            #region openTheFow
            NewGeneration.sky[(int)hit.transform.position.x/3,(int)hit.transform.position.z/3].SetActive(false);
            if(hit.transform.position.x/3 - 1 > 0) 
            {
                NewGeneration.sky[(int)hit.transform.position.x/3-1,(int)hit.transform.position.z/3].SetActive(false);
                if(hit.transform.position.z/3 - 1 > 0) NewGeneration.sky[(int)hit.transform.position.x/3-1,(int)hit.transform.position.z/3-1].SetActive(false);
                if(hit.transform.position.z/3 + 1 < NewGeneration.sky.Length) NewGeneration.sky[(int)hit.transform.position.x/3-1,(int)hit.transform.position.z/3+1].SetActive(false);
            }
            if(hit.transform.position.x/3+1<NewGeneration.sky.Length) 
            {
                NewGeneration.sky[(int)hit.transform.position.x/3+1,(int)hit.transform.position.z/3].SetActive(false);
                if(hit.transform.position.z/3 - 1 > 0) NewGeneration.sky[(int)hit.transform.position.x/3+1,(int)hit.transform.position.z/3-1].SetActive(false);
                if(hit.transform.position.z/3 + 1 < NewGeneration.sky.Length) NewGeneration.sky[(int)hit.transform.position.x/3+1,(int)hit.transform.position.z/3+1].SetActive(false);
            }
            if(hit.transform.position.z/3 - 1 > 0) NewGeneration.sky[(int)hit.transform.position.x/3,(int)hit.transform.position.z/3-1].SetActive(false);
            if(hit.transform.position.z/3 + 1 < NewGeneration.sky.Length) NewGeneration.sky[(int)hit.transform.position.x/3,(int)hit.transform.position.z/3+1].SetActive(false);
            #endregion
            Debug.Log($"destroyWall, wall.trasnform.position.x {hit.transform.position.x}, wall.z : {hit.transform.position.z}");
            
            ChangeState.openTheFow(hit.transform.position);
            UnitSelection.Instance.mineurs.Enqueue(this.gameObject);
        }
        
        
    }



    /*private void Start()
    {
        UnitSelection.Instance.mineurs.Enqueue(go);

    }*/
}
