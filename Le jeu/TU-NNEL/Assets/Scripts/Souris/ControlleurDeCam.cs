using UnityEngine;
using System.Runtime;
using System;

public class ControlleurDeCam : MonoBehaviour
{
    #region Emmeline's part
    public static bool init;
    #endregion


    [SerializeField] private float vitesse = 10f;           //vitesse de la caméra. (conseil: ne pas trop augmenter sinon unity crash etoutetout)
    [SerializeField] private float limite;           //Hauteur limite à laquelle peut accéder la caméra.
    [SerializeField] private float vitesseDuScroll = 2000f; //Vitesse de zoom
    [SerializeField] public NewGeneration script;

    void Start()
    {
        
    }



    void Update()
    {
        Vector3 position = transform.position;

        //Modif
        
        if(!init && NewGeneration.coordBase.Count > CreateAndJoinRooms.player -1 && CreateAndJoinRooms.player -1 >= 0)
        {
            Debug.Log($"CreateAndJoinRooms.player -1 ; {CreateAndJoinRooms.player -1}");
            Debug.Log($"Wait.CoordCam.Count : {Wait.CoordCam.Count}");
            Debug.Log($"NewGeneration.coordBase.Count : {NewGeneration.coordBase.Count}");
            position.x = NewGeneration.coordBase[CreateAndJoinRooms.player -1].Item1;
            position.y = 15;
            position.z =  NewGeneration.coordBase[CreateAndJoinRooms.player -1].Item3 -5;
            init = true;
        }
        //Fin des modif

        float s = 3 * (float) script.size;
    
        
        float zoom = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetKey("z"))
        {
            position.z += vitesse * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            position.z += -vitesse * Time.deltaTime;
        }
        if (Input.GetKey("q"))
        {
            position.x += -vitesse * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            position.x += vitesse * Time.deltaTime;
        }

        position.y -= zoom * vitesseDuScroll * Time.deltaTime;


        position.x = Mathf.Clamp(position.x, 0f, s);

        position.z = Mathf.Clamp(position.z, 0f, s);

        position.y = Mathf.Clamp(position.y, 3.2f, limite);  //mathf.clamp sert � fixer une limite � la position x, y ou z.

        transform.position = position;
    }
}