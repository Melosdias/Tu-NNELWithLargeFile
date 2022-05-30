using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ciblage : MonoBehaviour
{
    private void OnTriggerEnter(Collider SphereCollider)
    {
        if (SphereCollider.CompareTag("P1"))
        {
            this.transform.GetComponent<Stats>().Cible = SphereCollider.gameObject;
        }
    }
    private void OnTriggerExit(Collider SphereCollider)
    {
        if (SphereCollider.CompareTag("P1"))
        {
            this.transform.GetComponent<Stats>().Cible = null;
        }
    }
}
