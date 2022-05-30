using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Runtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;
    private static int playerConnect = 0;
    private static int maxiPlayer = 2;
    private static string nameScene = "Prejeu";
    public static bool tryCreate = false;
    private static bool tryJoin = false;
    public static int rnd;


    public static int error = 0; 
    /*If there is no problem to be cnnected, error= 0
    If the player try to create a room which already exist, error = 1
    If the player try to join a room that don't exist, error = 2
    If the player try to join a full room, error = 3
    */

    public static int player
    {
        get => playerConnect;
      
        set { playerConnect = value; }
    }
    public static string Name
    {
        get => nameScene;
        set { nameScene = value; }
    }
    public static bool TryCreate
    {
        get => tryCreate;
        set { tryCreate = value; }
    }

    public static bool TryJoin
    {
        get => tryJoin;
        set { tryJoin = value; }
    }
    public static int Maxi 
    {
        get => maxiPlayer;
        set { maxiPlayer = value; }
    }

    public void CreateRoom()
    {        
        Debug.Log($"CreateRoom, error : {error}");
        PhotonNetwork.CreateRoom(createInput.text, new RoomOptions {MaxPlayers = (byte)maxiPlayer}, null); //Gives the power to create a room with only two players
        tryCreate = true;
    }

    
    public void JoinRoom()
    {
        Debug.Log("Join room");
        tryJoin = true;
        player++;
        Debug.Log($"Player : {player}");
        Debug.Log($"MaxiPlayer : {maxiPlayer}");
        PhotonNetwork.JoinRoom(joinInput.text); //Gives the power to join the room automatically after create it
    }
    
    public override void OnJoinedRoom()
    {
        playerConnect = PhotonNetwork.CurrentRoom.PlayerCount;
        Debug.Log($"Number of players in the current room :{PhotonNetwork.CurrentRoom.PlayerCount}");
        Debug.Log($"MaxPlayers {PhotonNetwork.CurrentRoom.MaxPlayers}");
        Debug.Log($"Name of the current scene :{SceneManager.GetActiveScene().name}");
        PhotonNetwork.LoadLevel("Game");//Gives the power to start to wait 
        nameScene = SceneManager.GetActiveScene().name;
    }

    public override void OnCreateRoomFailed(short returnCode, string message )
    {
        base.OnCreateRoomFailed(returnCode, message);
        error = 1; 
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        
        if (returnCode == 0x7FFF - 2) //If the player want to join a full room

        {
            error = 3; 
            Debug.Log($"OnJoinRoomFailed, error : {error}");
        }
        else 
        {
            error = 2; //Otherwise, the player wanted to join a room that dos not exist
            Debug.Log($"OnJoinRoomFailed, error : {error}");
        }
    }
}