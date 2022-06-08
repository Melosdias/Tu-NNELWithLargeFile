using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
public class Bouge : MonoBehaviourPun
{
    Camera myCam;
    NavMeshAgent sbire;
    public LayerMask ground;
    RaycastHit hit;
    Animator m_Animator;

    void Start()
    {
        myCam = Camera.main;
        sbire = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
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
        
        if(sbire != null) 
        {
            sbire.SetDestination(hit.point);
            m_Animator.SetBool("isWalking", true); 
        }
    }
    [PunRPC]
    void stopSbire()
    {
        
        if(sbire != null)
        {
            sbire.isStopped = true;
            sbire.ResetPath();
            m_Animator.SetBool("isWalking", false);
        }
    }
}
