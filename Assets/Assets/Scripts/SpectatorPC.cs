using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpectatorPC : MonoBehaviourPunCallbacks
{
    private static string ROOM_NAME = "MetaDewata";

    
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.SerializationRate = 30;
        Connect("Computer");
    }

    
    void Update()
    {
        
    }
    private void Connect(string userID)
    {
        PhotonNetwork.AuthValues = new AuthenticationValues();
        PhotonNetwork.AuthValues.UserId = userID;

        PhotonNetwork.ConnectUsingSettings();
        
    }
    public override void OnConnectedToMaster()
    {
        
        bool _result = PhotonNetwork.JoinLobby();

    }

    

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo rInfo in roomList)
        {
            Debug.Log(rInfo.Name);
        }

    }


    public override void OnJoinedLobby()
    {
        RoomOptions rOptions = new RoomOptions();
        rOptions.MaxPlayers = 4;
        rOptions.PublishUserId = true;

        PhotonNetwork.JoinOrCreateRoom(ROOM_NAME, rOptions, TypedLobby.Default);

        
    }


   
}
