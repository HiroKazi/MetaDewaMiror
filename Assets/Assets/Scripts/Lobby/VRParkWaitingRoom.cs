using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using TMPro;

namespace MPI.VRpark.Lobby
{
    public class VRParkWaitingRoom : MonoBehaviour
    {
        [Header("Main Panel")]
        [SerializeField] private GameObject mainPanel;

        [SerializeField] private TMP_Text waitingRoomTitle;

        [SerializeField] private TMP_Text playerNameTemplate;

        [SerializeField] private VRButton startGameBtn;

        [Header("Error Message")]
        [SerializeField] private TMP_Text errorMessageLabel;

        private List<TMP_Text> playerNames;

        public Action OnStartGame;

        private void Awake()
        {
            startGameBtn.OnClick += StartGame;
        }

        private void OnEnable()
        {
            waitingRoomTitle.text = $"Current Room : {PhotonNetwork.CurrentRoom.Name}";
            startGameBtn.gameObject.SetActive(PhotonNetwork.IsMasterClient);
            UpdatePlayerNames();
        }

        public void Show()
        {
            mainPanel.SetActive(true);
            errorMessageLabel.text = "";
        }

        public void ShowErrorMessage(string errorMessage, float showTime = -1)
        {
            StartCoroutine(ShowErrorMessageForSeconds(errorMessage, showTime));
        }

        IEnumerator ShowErrorMessageForSeconds(string errorMessage, float showTime)
        {
            mainPanel.SetActive(false);
            errorMessageLabel.text = errorMessage;

            if (showTime >= 0)
            {
                yield return new WaitForSeconds(showTime);
                Show();
            }
        }

        public void UpdatePlayerNames()
        {
            if (playerNames == null)
                playerNames = new List<TMP_Text>();

            for(int i=0; i< playerNames.Count; i++)
            {
                Destroy(playerNames[i].gameObject);
            }

            playerNames.Clear();

            foreach (Photon.Realtime.Player pl in PhotonNetwork.PlayerList)
            {
                GameObject newPlayerName = Instantiate(playerNameTemplate.gameObject);
                newPlayerName.SetActive(true);
                newPlayerName.transform.SetParent(playerNameTemplate.transform.parent, false);

                RectTransform rt = newPlayerName.GetComponent<RectTransform>();

                rt.localPosition += new Vector3(0, -rt.sizeDelta.y * playerNames.Count, 0);

                TMP_Text newPlayerNameText = newPlayerName.GetComponent<TMP_Text>();
                newPlayerNameText.text = pl.UserId;
                if (pl.IsMasterClient)
                    newPlayerNameText.text += " (Master)";

                playerNames.Add(newPlayerNameText);
            }
                
            startGameBtn.gameObject.SetActive(PhotonNetwork.IsMasterClient);
        }

        public void StartGame()
        {
            OnStartGame?.Invoke();
        }


        
    }
}
