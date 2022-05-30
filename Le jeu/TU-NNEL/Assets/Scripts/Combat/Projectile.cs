using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public GameObject cible;

    public bool ciblé;
    public string TypeDeCible;
    public float vitesse = 20;
    public bool Stop;   

    void Update()
    {
        if (cible)
        {
            if (cible == null)
            {
                Destroy(gameObject);
            }

            transform.position = Vector3.MoveTowards(transform.position, cible.transform.position, vitesse * Time.deltaTime);
            
            if (!Stop)
            {
                if (Vector3.Distance(transform.position, cible.transform.position)<0.5f)
                {
                    if (!Stop)
                    {
                        cible.GetComponent<Stats>().PV -= damage;
                        Stop = true;
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
