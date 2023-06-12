using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using Photon.Realtime;

namespace Photon.Pun.Demo.PunBasics
{

    public class GameController : MonoBehaviourPunCallbacks
    {
        public GameObject Character;

        //static public GameController Instance;

        //private GameObject instance;

        //[Tooltip("The prefab to use for representing the player")]
        [SerializeField]
        //private GameObject playerPrefab;


        // Start is called before the first frame update
        void Start()
        {
            Instantiate(Character, new Vector3(0, 1, 0), Quaternion.identity);

            //Instance = this;

            // in case we started this demo with the wrong scene being active, simply load the menu scene
            //if (!PhotonNetwork.IsConnected)
            //{
            //SceneManager.LoadScene("PunBasics-Launcher");

            //return;
            //}

            //if (playerPrefab == null)
            //{ // #Tip Never assume public properties of Components are filled up properly, always check and inform the developer of it.

            //Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            //}
            //else
            //{

            //PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(2.7f, 1.79f, -9f), Quaternion.identity, 0);
        }
    }

}
//}