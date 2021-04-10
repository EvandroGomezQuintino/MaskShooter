using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameNetwork : MonoBehaviourPunCallbacks
{

    public string PlayerName;

    //Testing the connection
    public Text chatlog;

    // -------------------------------------    Starting Connection    --------------------------------------- //
    private void Awake()
    {
        // Accessing the server
        chatlog.text += "\n Connecting to the Server";

        PlayerName = PhotonNetwork.LocalPlayer.NickName;
        PhotonNetwork.ConnectUsingSettings();

        Resources.LoadAll("Assets/Prefabs/Resources");



    }


    // -------------------------------------    Checking Connection    --------------------------------------- //

    public override void OnConnectedToMaster()
    {


        chatlog.text += "Connected!";

        if (PhotonNetwork.InLobby == false)
        {
            chatlog.text += "\n Entering Lobby";
            PhotonNetwork.JoinLobby();
        }


    }

    // -------------------------------------    Accessing Lobby    --------------------------------------- //

    public override void OnJoinedLobby()
    {
        chatlog.text += "\n Entered Lobby!";
        chatlog.text += "\n Entering Room LOTR";

        PhotonNetwork.JoinRoom("LOTR");

    }

    // -------------------------------------    Creating Room    --------------------------------------- //

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        chatlog.text += "\n Error entering the room" + message + "+ | codigo" + returnCode;

        if (returnCode == ErrorCode.GameDoesNotExist)
        {
            chatlog.text += "\n Creating Room";

            // Settings for our room
            RoomOptions roomSettings = new RoomOptions { MaxPlayers = 2 };
            // Creating room
            PhotonNetwork.CreateRoom("LOTR", roomSettings, null);
            PhotonNetwork.LocalPlayer.NickName = "PLAYER1";
            PlayerName = PhotonNetwork.LocalPlayer.NickName;
   

        }


    }

    // -------------------------------------    Accessing Room --------------------------------------- //

    public override void OnJoinedRoom()
    {
        chatlog.text += PlayerName + " entered Room!";

        if (PhotonNetwork.LocalPlayer.IsMasterClient == false)
        {


            PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-25.0f,25.0f),1, Random.Range(-25.0f, 25.0f)),Quaternion.identity);
        }

    }
    // -------------------------------------    New Player  --------------------------------------- //
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        newPlayer.NickName = "PLAYER2";
        chatlog.text = newPlayer.NickName + " entered the room";
    }


    // -------------------------------------    Player Leaving Room  --------------------------------------- //
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        chatlog.text += "Player " + otherPlayer.NickName + " left the room.";
    }

    // -------------------------------------    Host leave room  --------------------------------------- //

    public override void OnLeftRoom()
    {
        chatlog.text = "Host left the room!";
    }

    // -------------------------------------    Retuning Connection Error  --------------------------------------- //

    public override void OnErrorInfo(ErrorInfo errorInfo)
    {
        chatlog.text = "Error to connect: " + errorInfo.Info;
    }





}
