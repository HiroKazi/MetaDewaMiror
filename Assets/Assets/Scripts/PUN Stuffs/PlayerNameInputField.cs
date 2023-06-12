using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace Photon.Pun {
    [RequireComponent(typeof(InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {

        #region Private Constants

        const string playerNamePrefKey = "PlayerName";
        const string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";

        #endregion

        #region MonoBehavior Callbacks

        
        // Start is called before the first frame update
        void Start()
        {
            int usernameAmount = Random.Range(4, 8);
            string defaultName = string.Empty;
            InputField _inputField = this.GetComponent<InputField>();

            for (int i = 0; i < usernameAmount; i++)
            {
                _inputField.text += glyphs[Random.Range(0, glyphs.Length)];
            }
            
            if (_inputField!=null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    _inputField.text = defaultName;
                }
            }

            PhotonNetwork.NickName = defaultName;
        }
        #endregion

        #region Public Methods

        public void SetPlayerName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Player name is null or empty");
                return;
            }
            PhotonNetwork.NickName = value;

            PlayerPrefs.SetString(playerNamePrefKey, value);
        }
        #endregion
    }
}
