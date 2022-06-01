using UnityEngine;
using System.Runtime;
using System;

public class ControlleurDeCam : MonoBehaviour
{
    #region Emmeline's part
    public static bool init;
    //Coord area
    public static int north;
    public static int south;
    public static int east;
    public static int west;
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
            int idPlayer = CreateAndJoinRooms.player - 1;
            Debug.Log($"CreateAndJoinRooms.player -1 ; {CreateAndJoinRooms.player -1}");
            Debug.Log($"Wait.CoordCam.Count : {Wait.CoordCam.Count}");
            Debug.Log($"NewGeneration.coordBase.Count : {NewGeneration.coordBase.Count}");
            position.x = NewGeneration.coordBase[idPlayer].Item1;
            position.y = 15;
            position.z =  NewGeneration.coordBase[idPlayer].Item3 -5;
            init = true;
            //Calcul de l'aire de base
            north = NewGeneration.coordBase[idPlayer].Item3 +3;
            Debug.Log($"North : {north}");
            south = NewGeneration.coordBase[idPlayer].Item3 -3;
            Debug.Log($"South : {south}");
            east = NewGeneration.coordBase[idPlayer].Item1 +3;
            Debug.Log($"East : {east}");
            west = NewGeneration.coordBase[idPlayer].Item1 -3;
            Debug.Log($"West : {west}");
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

        //Si On veut bloquer la cam, on garde ces lignes là
        if(east<west) position.x = Mathf.Clamp(position.x, east, west);
        else position.x = Mathf.Clamp(position.x,  west, east); //Imagine avoir max < min.. Imagine >.>
        if (south < north ) position.z = Mathf.Clamp(position.z, south, north);
        else position.z = Mathf.Clamp(position.z, north, south);
        position.y = Mathf.Clamp(position.y, 3.2f, limite);
        
        //Sinon on décommente celle là ;)
        /*position.x = Mathf.Clamp(position.x, 0f, s);

        position.z = Mathf.Clamp(position.z, 0f, s);

        position.y = Mathf.Clamp(position.y, 3.2f, limite);  //mathf.clamp sert a fixer une limite a la position x, y ou z.
        */
        transform.position = position;
    }
}