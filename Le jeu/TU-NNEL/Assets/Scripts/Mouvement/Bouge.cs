using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
public class Bouge : MonoBehaviourPun
{
    Camera myCam;
    NavMeshAgent sbire;
    public LayerMask ground;
    RaycastHit hit;

    void Start()
    {
        myCam = Camera.main;
        sbire = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                photonView.RPC("moveSbire", RpcTarget.All);
            }
        }
        if (Vector3.Distance(hit.point, sbire.transform.position) < 2)
        {
            photonView.RPC("stopSbire", RpcTarget.All);
        }
    }
    [PunRPC]
    void moveSbire()
    {
        
        if(sbire != null) sbire.SetDestination(hit.point);
    }
    [PunRPC]
    void stopSbire()
    {
        
        if(sbire != null)
        {
            sbire.isStopped = true;
            sbire.ResetPath();
        }
    }
}
