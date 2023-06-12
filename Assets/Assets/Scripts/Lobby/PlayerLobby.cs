using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MPI.VRpark.Lobby
{
    public class PlayerLobby : MonoBehaviour
    {
        private static PlayerLobby _instance;

        public static PlayerLobby instance
        {
            get { return _instance; }
            private set { }
        }

        [Header("Hand Collider")]
        [SerializeField] private Collider rightIndexFinger;
        [SerializeField] private Collider leftIndexFinger;

        public Collider RightIndexFinger { get { return rightIndexFinger; } }
        public Collider LeftIndexFinger { get { return leftIndexFinger; } }

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
        }
    }
}