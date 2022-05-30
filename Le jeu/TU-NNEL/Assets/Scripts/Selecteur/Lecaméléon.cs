using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lecaméléon : MonoBehaviour       //Sert à changer la couleur d'un truc selectionné en vert (vous pouvez changer la couleur si vous kiffez pas le vert)
{
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }

    private void OnDestroy()              //Sert à reswitch la couleur d'un truc selectionné en blanc.
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
