using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class TheCubeScript : MonoBehaviourPun
{
    [SerializeField] public GameObject canvas; //Canvas
    public static Vector3 position;
    public static PhotonView view;
    public static bool tigerred = false;


    void OnTriggerEnter(Collider other)
    {
        if (!tigerred)
        {   
            if(other.gameObject.CompareTag("mine"))
            {
                GameObject Bloc = Object.Instantiate(canvas);
                canvas.SetActive(true);
                ChangeState.builded = true;
                position = this.transform.position;
                view = PhotonView.Get(this);
            }
            tigerred = true;
        }
    }
}
