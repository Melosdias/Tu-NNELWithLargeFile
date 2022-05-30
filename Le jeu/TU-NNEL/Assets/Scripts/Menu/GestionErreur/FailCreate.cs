using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Threading;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class FailCreate : MonoBehaviour
{
    public Text textError;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        Debug.Log($"Name of scene {CreateAndJoinRooms.Name}");
        
        textError = GameObject.Find("/Ecran/CreateFail/TextError").GetComponent<UnityEngine.UI.Text>();
        Debug.Log(textError.name);
        textError.text = "";
        //CreateFail.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
     void  Update()
    {
        CreateAndJoinRooms.Name = SceneManager.GetActiveScene().name;
        //Debug.Log($"Name of scene {CreateAndJoinRooms.Name}");

        /*if (CreateAndJoinRooms.TryCreate && CreateAndJoinRooms.Name == "Prejeu") //Player wants to create a room that already exists
        {
            //Thread.Sleep(200);
            textError.text = "Already used"+ '\n' +"Please enter another name";
        }
        if(!CreateAndJoinRooms.TryCreate && CreateAndJoinRooms.Name == "Prejeu" && CreateAndJoinRooms.TryJoin) //Player wants to join a room that is filled
        {
            if(CreateAndJoinRooms.player == CreateAndJoinRooms.Maxi)
            {
                textError.text = "Full game";
            }
            else
            {
                textError.text = "Don't exist";
            }
        }*/
        if (CreateAndJoinRooms.error == 1)
        {
            textError.text = "Already used"+ '\n' +"Please enter another name";
        }
        if (CreateAndJoinRooms.error == 2)
        {
            textError.text = "Don't exist";
        }
        if (CreateAndJoinRooms.error == 3)
        {
            textError.text = "Full game";
        }
    }
}
