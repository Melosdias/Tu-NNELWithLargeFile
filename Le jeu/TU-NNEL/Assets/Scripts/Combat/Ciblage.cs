using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ciblage : MonoBehaviour
{
    List<GameObject> cibles = new List<GameObject>();
    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider SphereCollider)
    {
        if (SphereCollider.CompareTag("P1"))
        {
            cibles.Add(SphereCollider.gameObject);
        }
    }
    private void OnTriggerExit(Collider SphereCollider)
    {
        if (SphereCollider.CompareTag("P1"))
        {
            if (cibles.Contains(SphereCollider.gameObject))
                cibles.Remove(SphereCollider.gameObject);
        }
    }
}
