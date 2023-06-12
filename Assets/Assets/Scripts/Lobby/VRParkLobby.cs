using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;
using TMPro;

namespace MPI.VRpark.Lobby
{
    public class VRParkLobby : MonoBehaviourPunCallbacks
    {

        private static string ROOM_NAME = "MetaDewata";

        [SerializeField] private PUNConnection connectionPanel;
        [SerializeField] private VRParkWaitingRoom waitingRoom;

        // Start is called before the first frame update
        void Start()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.SerializationRate = 30;

            connectionPanel.OnSubmit += Connect;
            waitingRoom.OnStartGame += LoadGameLevel;

            if (PhotonNetwork.IsConnected)
                PhotonNetwork.Disconnect();
            
        }


        private void Connect(string userID)
        {
            PhotonNetwork.AuthValues = new AuthenticationValues();
            PhotonNetwork.AuthValues.UserId = userID;

            PhotonNetwork.ConnectUsingSettings();
            connectionPanel.ShowErrorMessage("Connecting....");
        }

    #region CONNECT & LOBBY CALLBACK
        public override void OnConnectedToMaster()
        {
            connectionPanel.ShowErrorMessage("Joining Lobby...");
            bool _result = PhotonNetwork.JoinLobby();

            if (!_result)
            {
                connectionPanel.ShowErrorMessage("Could not joinLobby", 2.0f);
            }

        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            connectionPanel.gameObject.SetActive(true);
            waitingRoom.gameObject.SetActive(false);
            connectionPanel.ShowErrorMessage($"Disconnected : {cause}", 2.0f);
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
            rOptions.MaxPlayers = 10;
            rOptions.PublishUserId = true;

            PhotonNetwork.JoinOrCreateRoom(ROOM_NAME, rOptions, TypedLobby.Default);

            connectionPanel.ShowErrorMessage($"Joining Room {ROOM_NAME}");
        }

        public override void OnLeftLobby()
        {
            connectionPanel.ShowErrorMessage($"Left Lobby", 2.0f);
        }
    #endregion

    #region ROOM_STUFF
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            connectionPanel.ShowErrorMessage($"Failed to create room", 2.0f);
        }

        public override void OnJoinedRoom()
        {
            connectionPanel.ShowErrorMessage($"Joined Room : {PhotonNetwork.CurrentRoom.Name}");

            StartCoroutine(GoToWaitingRoomAfterSeconds(2.0f));
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            connectionPanel.ShowErrorMessage(message, 2.0f);
        }

        public override void OnLeftRoom()
        {
            connectionPanel.ShowErrorMessage("Left room");
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            waitingRoom.UpdatePlayerNames();
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            waitingRoom.UpdatePlayerNames();
        }

        public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
        {
            if (propertiesThatChanged["GoToGame"].ToString() == "true")
            {
                waitingRoom.ShowErrorMessage("Starting Game");
            }
        }
        #endregion


        IEnumerator GoToWaitingRoomAfterSeconds(float time)
        {
            yield return new WaitForSeconds(time);

            waitingRoom.gameObject.SetActive(true);
            connectionPanel.gameObject.SetActive(false);

            waitingRoom.UpdatePlayerNames();
        }

        void LoadGameLevel()
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "GoToGame", "true" } });
            StartCoroutine(GotoControlRoomAfterSecond(2));
        }

        IEnumerator GotoControlRoomAfterSecond(float time)
        {
            yield return new WaitForSeconds(time);
            PhotonNetwork.LoadLevel("01-02-LobGamel");
        }
    }
}