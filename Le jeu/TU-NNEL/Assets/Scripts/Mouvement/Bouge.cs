using UnityEngine;
using UnityEngine.AI;
public class Bouge : MonoBehaviour
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
                sbire.SetDestination(hit.point);
            }
        }
        if (Vector3.Distance(hit.point, sbire.transform.position) < 2)
        {
            sbire.isStopped = true;
            sbire.ResetPath();
        }
    }
}
