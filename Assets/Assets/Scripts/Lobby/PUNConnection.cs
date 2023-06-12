using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;
using TMPro;

namespace MPI.VRpark.Lobby
{
    public class PUNConnection : MonoBehaviour
    {
        [Header("Connection")]
        [SerializeField] private GameObject connectionPanel;

        [Space()]
        [SerializeField] private VRButton userIDButton;
        [SerializeField] private TMP_Text userIDText;

        [SerializeField] private VRButton submitButton;

        [Header("Error Message")]
        [SerializeField] private TMP_Text errorMessageLabel;

        [Header("Tools")]
        [SerializeField] private VRButton calibrateButton;

        private TouchScreenKeyboard keyboard;

        public Action<string> OnSubmit;

        void Awake()
        {
            userIDButton.OnClick += OpenDefaultKeyboard;
            submitButton.OnClick += Connect;
            calibrateButton.OnClick += delegate ()
            {
                SceneManager.LoadScene("CalibrateMetadewa");
            };


            if (SystemInfo.deviceUniqueIdentifier == "9a18e9fcbed91b2754e98690bb0c5896")
                userIDText.text = "OculusHitam";

            if (SystemInfo.deviceUniqueIdentifier == "fed9b9fa2ca74659923d88f42cf1e25a")
                userIDText.text = "OculusPutih";
        }

        void Update()
        {
            if (keyboard != null)
            {
                userIDText.text = keyboard.text;
                if (keyboard.status == TouchScreenKeyboard.Status.Done) keyboard = null;
            }
        }
        public void openCalibrateScene()
        {
            SceneManager.LoadScene("CalibrateMetadewa");
        }
        public void OpenDefaultKeyboard()
        {
            keyboard = TouchScreenKeyboard.Open(userIDText.text, TouchScreenKeyboardType.Default);
        }

        public void Connect()
        {
            if(keyboard == null){
                OnSubmit?.Invoke(userIDText.text);
            }
        }

        public void ShowErrorMessage(string errorMessage, float showTime = -1)
        {
            StartCoroutine(ShowErrorMessageForSeconds(errorMessage, showTime));
        }

        public void Show()
        {
            connectionPanel.SetActive(true);
            errorMessageLabel.text = "";
        }

        IEnumerator ShowErrorMessageForSeconds(string errorMessage, float showTime)
        {
            connectionPanel.SetActive(false);
            errorMessageLabel.text = errorMessage;

            if (showTime >= 0)
            {
                yield return new WaitForSeconds(showTime);
                Show();
            }
        }
    }
}
