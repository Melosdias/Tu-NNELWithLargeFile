using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class TheCubeScript : MonoBehaviourPun
{
    [SerializeField] public GameObject aled1; //Canvas
    public static Vector3 position;
    public static PhotonView view;
    public static bool tigerred = false;


    void OnTriggerEnter(Collider other)
    {
        if (!tigerred)
        {
            GameObject Bloc = Object.Instantiate(aled1);
            aled1.SetActive(true);
            ChangeState.builded = true;
            position = this.transform.position;
            view = PhotonView.Get(this);
            tigerred = true;
        }
    }
}
