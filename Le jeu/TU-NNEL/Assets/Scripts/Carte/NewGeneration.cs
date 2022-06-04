using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class NewGeneration : MonoBehaviourPun
{
    #region Emmeline's part
    public static bool create; 
    public static int seed; 
    public static int i;
    public static int j;
    
    public static List<(int,int, int)> coordBase = new List<(int, int, int)>();
    public static List<(int,(int,int, int))> coordBaseVithId = new List<(int, (int,int, int))>(); //On a vu plus opti, mais changer coordBase impliquait trop de changement pour un truc dont je suis pas sûre >.>
    System.Random rnd = new System.Random(seed); 
    public static GameObject[,] sky = new GameObject[100, 100];
    
    #endregion 
    public int size = 100;

    [SerializeField]  private int playerNum = 2;
    [SerializeField]  private GameObject Mur;
    [SerializeField]  private GameObject Base;
    [SerializeField] private GameObject Cube;
    [SerializeField] private GameObject GreatMine;
    
    
    
    public string Type()
    {
        
        int NombreAuPif = rnd.Next(0,100);
        if (NombreAuPif < 15) return "fer";
        if(NombreAuPif < 41) return "pierre";
        else return "boue";
    }
    public void GenererLaCarte()
    {
        List<(GameObject, string)> map = new List<(GameObject, string)>();
        
        for (int i = 0; i < size; i++) for (int j = 0; j < size; j++)
        {
            string a = Type(); 
            GameObject Bloc = (GameObject)Instantiate(Resources.Load(a), transform);
            Bloc.transform.position = new Vector3(3*i, 0,3*j);
            Bloc.layer = 7;
            PhotonView view = Bloc.AddComponent<PhotonView>() as PhotonView; //Normalement ça ajoute un photonView à ce bloc (et c'est cool)
            Bloc.tag = "free";
            map.Add((Bloc, a));

            a = "pierre";
            GameObject go = (GameObject)Instantiate(Resources.Load(a), transform);
            go.transform.position = new Vector3(3*i, 4,3*j);
            go.layer = 6;
            sky[i,j] = go;
        }
    }
    

    private  Cell[,] ConstructMat() 
    {
        
        int rep = 0;
        int s = size/4;

        System.Random rnd = new System.Random(seed*3);            
        int a = 1;
        Cell[,] genM = new Cell[size, size]; // initialise la matrice avec des caillous partouts
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                genM[i,j] = new Cell(true,false,false) ;
            }
        }
        int x = 0;
        int y = 0;
        while (a <= playerNum)                               //s'occupe du spawn des joueurs ( de 1 à 4
        {
            if (a == 1)
            {
                x = rnd.Next(2, size / 2 - 1);
                y = rnd.Next(2, size / 2 - 1);
            }
            if (a == 2)
            {
                x = rnd.Next(size / 2 - 1 , size - 2);
                y = rnd.Next(size / 2 + 1, size - 2);
            }
            if (a == 3)
            {
                x = rnd.Next(2, size / 2 - 1);
                y = rnd.Next(size / 2 + 1, size - 2);
            }
            if (a == 4)
            {
                x = rnd.Next( size / 2 - 1, size - 2);
                y = rnd.Next(2, size / 2 - 1);
            }
            genM[y,x].isSpawn = true;
            genM[y,x].isRock = false;
            genM[y + 1,x].isRock = false;
            genM[y - 1,x].isRock = false;
            genM[y,x + 1].isRock = false;
            genM[y,x - 1].isRock = false;
            genM[y + 1,x + 1].isRock = false;
            genM[y + 1,x - 1].isRock = false;
            genM[y - 1,x + 1].isRock = false;
            genM[y - 1,x - 1].isRock = false;
            a = a + 1;
        }
        return genM; 
    }
    private Cell[,] ConstructMatRuin(Cell[,]ori)
    {
        int count = playerNum / 2;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (ori[i, j].isSpawn)
                {
                    if (count == 0 )
                    {
                        break;
                    }
                    int posx = rnd.Next(j+3,size - 2);
                    int posy = rnd.Next(i+3, size - 2);
                    ori[posy, posx].isRuin = true;
                    ori[posy,posx].isRock = false;
                    ori[posy + 1,posx].isRock = false;
                    ori[posy - 1,posx].isRock = false;
                    ori[posy,posx + 1].isRock = false;
                    ori[posy,posx - 1].isRock = false;
                    ori[posy + 1,posx + 1].isRock = false;
                    ori[posy + 1,posx - 1].isRock = false;
                    ori[posy - 1,posx + 1].isRock = false;
                    ori[posy - 1,posx - 1].isRock = false;
                    count--;
                }
            }
        }
        return ori;
    }
    private void ConstructMap() 
    {
        List<string> Ruines = new List<string>() {"Cube", "Mine"};
        Cell[,] genM = ConstructMatRuin(ConstructMat());
        string type;
        for (int y = 0; y < size; y++)               
        {
            for (int x = 0; x < size; x++)
            {

                if (genM[y,x].isSpawn)
                {
                    //Base.layer = 9;
                    if(photonView.IsMine) 
                    {
                        Debug.Log("photonView is mine");
                        //GameObject go = PhotonNetwork.Instantiate(Base.name, new Vector3(3*y, 1.1f,3*x), Quaternion.identity);
                        Debug.Log($"coordBase.Count {coordBase.Count}");
                        //if(coordBase.Count == 1) go.tag = "player1";
                        //else go.tag = "player2";
                    }
                    coordBase.Add((3*y, 1, 3*x));              
                    coordBaseVithId.Add((coordBase.Count-1, coordBase[coordBase.Count-1]));
                    /*(i,j) = (x,y);
                    photonView.RPC("UpdateCoordBase", RpcTarget.All);*/
                    
                }
                if (genM[y,x].isRock && !genM[y,x].isSpawn)
                {
                    Mur.layer = 9;
                    if(!photonView.IsMine)  continue ;
                    PhotonNetwork.Instantiate(Mur.name, new Vector3(3*y, 1.1f,3*x), Quaternion.identity);
                    Mur.tag = "Intact";
                    Mur.layer = 9;
                }
                
                
                if (genM[y,x].isRuin)
                {
                    int a = rnd.Next(0,500);
                    Debug.Log(a);
                    if (a % 2 == 0)
                    {
                        type = "Cube";
                    }
                    else
                    {
                        type = "Mine";
                    }
                    if (type == "Cube")
                    {
                        if(!photonView.IsMine)  continue ;
                        Cube.layer = 9;
                        PhotonNetwork.Instantiate(Cube.name, new Vector3(3*y, 1.3f,3*x),  Quaternion.Euler(new Vector3(Cube.transform.eulerAngles.x, Cube.transform.eulerAngles.y+90, Cube.transform.eulerAngles.z+90)));
                    }

                    if (type == "Mine")
                    {
                        if(!photonView.IsMine)  continue ;
                        GreatMine.layer = 9;
                        PhotonNetwork.Instantiate(GreatMine.name, new Vector3(3*y, 2,3*x), Quaternion.Euler(new Vector3(GreatMine.transform.eulerAngles.x, GreatMine.transform.eulerAngles.y+90, GreatMine.transform.eulerAngles.z+90)));
                    }
                }
            }
        }  
    }
    void Start()
    {
        create = false;
    }


     void Update()
    {
        //seed = Wait.seed;
        if(!(seed == 0) && !create)
        {
            Debug.Log($"create : {create}");
            Debug.Log($"coordBase.Count {coordBase.Count}");
            Wait.CoordCam = coordBase;
            GenererLaCarte();
            ConstructMap();
            create = true;
            foreach ((int,int, int) coord in coordBase)
            {
                Debug.Log($"Coordonées de la base : x : {coord.Item1}, y : {coord.Item2}, z: {coord.Item3}");
            }
            //Essai de fog of war
        }
    }
    [PunRPC]
    void tagBase(GameObject go)
    {
        Debug.Log("tagBase");
        if(coordBase.Count == 1) go.tag = "player1";
        else go.tag = "player2";
        //Wait.CoordCam.Add((3*j, 1, 3*i));
    }
}