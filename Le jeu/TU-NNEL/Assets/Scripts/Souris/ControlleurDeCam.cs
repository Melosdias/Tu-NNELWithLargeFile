using UnityEngine;
using System.Runtime;
using System;
using Photon.Pun;

public class ControlleurDeCam : MonoBehaviour
{
    #region Emmeline's part
    public static bool init;
    public static int idPlayer;
    [SerializeField]  private GameObject Base;
    //public static 
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

        //Determiner l'emplacement de base du joueur
        
        if(!init && NewGeneration.coordBase.Count > CreateAndJoinRooms.player -1 && CreateAndJoinRooms.player -1 >= 0)
        {
            idPlayer = CreateAndJoinRooms.player - 1;
            Debug.Log($"CreateAndJoinRooms.player -1 ; {CreateAndJoinRooms.player -1}");
            Debug.Log($"Wait.CoordCam.Count : {Wait.CoordCam.Count}");
            Debug.Log($"NewGeneration.coordBase.Count : {NewGeneration.coordBase.Count}");
            position.x = NewGeneration.coordBase[idPlayer].Item1;
            position.y = 15;
            position.z =  NewGeneration.coordBase[idPlayer].Item3 -5;
            init = true;

            GameObject go = PhotonNetwork.Instantiate(Base.name, new Vector3(NewGeneration.coordBase[idPlayer].Item1,NewGeneration.coordBase[idPlayer].Item2,NewGeneration.coordBase[idPlayer].Item3), Quaternion.identity);
            go.tag = "mine";
            go.layer = 9;
        }

        //Determiner l'aire de mouvement de la caméra
        //On ne veut pas que le joueur déplace simplement sa caméra à la recherche de on advsersaire parce que c'est nul, mais faire un fog of war simple
        //implique d'avoir un système de lumière et cie. Donc, en attendant, on va bloquer la caméra : c'est plus simple et ça permet d'avoir au moins un truc 
        //si on ne fait pas la gestion de la lumière correctement
        //On va simplement "calculer" la zone que le joueur peuit voir en fonction de son emplacement de base et des murs cassés
        //Cette zone sera un rectangle 


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

        position.y = Mathf.Clamp(position.y, 3.2f, limite);  //mathf.clamp sert a fixer une limite a la position x, y ou z.
        
        transform.position = position;
    }
}