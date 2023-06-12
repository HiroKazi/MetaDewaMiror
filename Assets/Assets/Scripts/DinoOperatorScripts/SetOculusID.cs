using Mirror;
using Mirror.Examples.Chat;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UI;
namespace Mirror.Examples.Operator
{


    public class SetOculusID : MonoBehaviour
    {
        [Header("UI Elements")]
       // [SerializeField] internal InputField usernameInput;
        [SerializeField] internal Button hostButton;
        [SerializeField] internal Button clientButton;
        [SerializeField] internal Text errorText; 

        public GameObject OculusID;
        public InputField OculusName;


        public string OculusInputName;
        private string PlayerID;


        

        public static SetOculusID instance;

        void Awake()
        {
            instance = this;
        }

        // Called by UI element UsernameInput.OnValueChanged
        public void ToggleButtons(string username)
        {
            hostButton.interactable = !string.IsNullOrWhiteSpace(username);
            clientButton.interactable = !string.IsNullOrWhiteSpace(username);
        }
        public void SetPlayerID()
        {

            OculusName.text = OculusInputName;
        }
    }
}