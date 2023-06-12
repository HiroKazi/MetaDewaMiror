using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using RootMotion.FinalIK;


namespace Mirror.Examples.Operator
{
    public class Player : NetworkBehaviour
    {
        #region Variables
        internal static readonly HashSet<string> playerNames = new HashSet<string>();
        internal static readonly List<string> playerNamesList = new List<string>();

        [SerializeField, SyncVar]
        internal string playerName;
        [SyncVar]
        public string gender;
        public GameObject male;
        public GameObject female;

        public Transform kanan;
        public Transform kiri;
        public Transform kepala;
        

        public SkinnedMeshRenderer skinnedMeshRendererMale;
        public SkinnedMeshRenderer skinnedMeshRendererFemale;
        public Material Femaleskin;
        public Material MaleSkin;

        [SyncVar]
        public string skin;
        bool FirstMaterial = true;
        bool SecondMaterial = false;

        private OVRHand OvrKanan, OvrKiri;
        private OVRCameraRig camRig;

        private Transform targetKanan, targetKiri, targetKepala;
        #endregion

        private void Awake()
        {
            camRig = GameObject.Find("OVRCameraRig").GetComponent<OVRCameraRig>();
            OvrKanan = camRig.rightHandAnchor.GetComponentInChildren<OVRHand>();
            OvrKiri = camRig.leftHandAnchor.GetComponentInChildren<OVRHand>();
            targetKanan = camRig.rightHandAnchor.Find("RightHandTarget");
            targetKiri = camRig.leftHandAnchor.Find("LeftHandTarget");
            targetKepala = camRig.centerEyeAnchor.Find("HeadTarget");
           
            
        }
        private void Start()
        {
        //  skinnedMeshRendererMale.material = MaleSkin;
        //  skinnedMeshRendererFemale.material = Femaleskin;
        }
        private void Update()
        {
            if (isLocalPlayer)
            {
                kepala.transform.position = targetKepala.position - new Vector3(0, 0, 0);
                kepala.transform.rotation = targetKepala.rotation;

                kanan.transform.position = targetKanan.position;
                kanan.transform.rotation = targetKanan.rotation;

                kiri.transform.position = targetKiri.position;
                kiri.transform.rotation = targetKiri.rotation;
                /*
                if (OvrKanan.IsTracked)
                {
                    kanan.transform.position = targetKanan.position;
                    kanan.transform.rotation = targetKanan.rotation;
                }

                if (OvrKiri.IsTracked)
                {
                    kiri.transform.position = targetKiri.position;
                    kiri.transform.rotation = targetKiri.rotation;
                }*/
            }
        }

        #region Methods
        // RuntimeInitializeOnLoadMethod -> fast playmode without domain reload
        [UnityEngine.RuntimeInitializeOnLoadMethod]
        static void ResetStatics()
        {
            playerNames.Clear();
        }

        public override void OnStartServer()
        {
            playerName = (string)connectionToClient.authenticationData;
        }

        public override void OnStartLocalPlayer()
        {
            ChatUI.localPlayerName = playerName;
            Debug.Log("{playerName}");
        }
        
        public override void OnStartAuthority()
        {
            NetworkIdentity networkIdentity = GetComponent<NetworkIdentity>();
            networkIdentity.AssignClientAuthority(connectionToClient);
            base.OnStartAuthority();
        }

        [ClientRpc]
        public void RpcSetGenderMale()
        {
            SetToMale();
        }
        [ClientRpc]
        public void RpcSetGenderFemale()
        {
            SetToFemale();
        }
        public void SetToMale()
        {
            male.gameObject.SetActive(true);
            female.gameObject.SetActive(false);
        }
        public void SetToFemale()
        {
            male.gameObject.SetActive(false);
            female.gameObject.SetActive(true);
        }
        
        [ClientRpc]
        public void RpcChangeSkinMale()
        {
            SkinCngMale();
        }
        [ClientRpc]
        public void RpcChangeSkinFemale()
        {
            SkinCngFemale();
        }
        public void SkinCngMale()
        {
            if (FirstMaterial)
            {
                skinnedMeshRendererMale.material = MaleSkin;
                SecondMaterial = true;
                FirstMaterial = false;
                Debug.Log("Set Stone Male");
            }
            else if (SecondMaterial)
            {
                skinnedMeshRendererMale.material = Femaleskin;
                FirstMaterial = true;
                SecondMaterial = false;
                Debug.Log("Set Wood Male");
            }
        }
        public void SkinCngFemale()
        {
            if (FirstMaterial)
            {
                skinnedMeshRendererFemale.material = MaleSkin;
                SecondMaterial = true;
                FirstMaterial = false;
                Debug.Log("Set Stone Female");
            }
            else if (SecondMaterial)
            {
                skinnedMeshRendererFemale.material = Femaleskin;
                FirstMaterial = true;
                SecondMaterial = false;
                Debug.Log("Set Wood Female");
            }
        }

        #endregion
    }
}
