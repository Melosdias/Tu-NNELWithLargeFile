using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloneMap : MonoBehaviour
{
    public int size = 100;
    [SerializeField]  private int playerNum = 2;
    public void GenererLaCarte()
    {
        for (int i = 0; i < size; i++) for (int j = 0; j < size; j++)
        {
            string a = "pierre"; 
            GameObject Bloc = (GameObject)Instantiate(Resources.Load(a), transform);
            Bloc.transform.position = new Vector3(3*i, 10,3*j);
            Bloc.layer = 6;
            
        }
    }
    void Start()
    {
        
    }

     void Update()
    {
        GenererLaCarte();
        
    }
    
}
