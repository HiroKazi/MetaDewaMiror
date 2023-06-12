using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

#pragma warning disable 649

   public class Launcher : MonoBehaviourPunCallbacks
   {
       [Tooltip("The maximum number of players per room. When a room is full, it cannot be joined by new players and therefore a new room will be made")]
       [SerializeField]
       private byte maxPlayersPerRoom = 2;
       #region Private Serializable Fields

        [Tooltip("The UI Panel to let users enter their username, then connect and play")]
        [SerializeField]
        private GameObject ControlPanel;

        [Tooltip("The UI Label to inform that the user is currently connecting to the server")]
        [SerializeField]
        private GameObject ProgressLabel;

        [SerializeField]
        private Text Counter;

        private float Count = 4.0f;
        private int length = 10;

        #endregion

       #region Private Fields
        bool isConnecting;
        

        /// <summary>
        /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
        /// </summary>
        string gameVersion = "1";

        #endregion

       #region MonoBehaviour Callbacks

       /// <summary>
       /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
       /// </summary>

       void Awake()
       {
           // #Critical
           // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
           PhotonNetwork.AutomaticallySyncScene = true;
       }

       /// <summary>
       /// MonoBehaviour method called on GameObject by Unity during initialization phase.
       /// </summary>


       #endregion

       #region Public Methods

        void Start()
        {
            Counter.text = ""+ Count;
            ProgressLabel.SetActive(false);
            ControlPanel.SetActive(true);
            StartCoroutine(LoginTimer());
            
        }

        void Update()
        {
            Counter.text = "" + Count;
        }

    public void Connect()
        {
            ProgressLabel.SetActive(true);
            ControlPanel.SetActive(false);


            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            } else
            {
               isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }
    #endregion

    #region Timer Related

        IEnumerator CountDown() {
            

            for (int i = 0; i < length; i++)
            {
                Count = Count - 1;
                yield return new WaitForSeconds(1);
            }

           
        }

        IEnumerator LoginTimer()
        {
        while (true)
        {
            for (int i = 0; i < length; i++)
            {
                Count -= 1;
                if (Count == 0)
                {
                    Counter.gameObject.SetActive(false);
                    Connect();   
                }
                yield return new WaitForSeconds(1);
            }
        }
    }

    #endregion

    #region MonoBehaviorPunCallbacks Callbacks

    public override void OnConnectedToMaster()
       {
           Debug.Log("Launcher: OnConnectedToMaster() was called by PUN");

           if (isConnecting)
           {
               PhotonNetwork.JoinRandomRoom();
               isConnecting = false;
           }
       }

       public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
       {
           Debug.LogWarningFormat("Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
           ProgressLabel.SetActive(false);
           ControlPanel.SetActive(true);

           isConnecting = false;
       }

       public override void OnJoinRandomFailed(short returnCode, string message)
       {
           Debug.Log("Launcher: OnJoinRandomFailed() was called by PUN No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

           PhotonNetwork.CreateRoom(null, new RoomOptions{ MaxPlayers = maxPlayersPerRoom });
       }

       public override void OnJoinedRoom()
       {
           Debug.Log("Launcher: OnJoinedRoom() was called by PUN. Now this client is inside a room.");

           if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
           {
               Debug.Log("We loaded the 'Room for 1'");


                PhotonNetwork.LoadLevel("SSMain");
            }
        }
        #endregion
    }