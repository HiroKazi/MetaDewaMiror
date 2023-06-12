using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class WelcomingMessageAndUsername : MonoBehaviour
{
    #region Public Fields

    public Text WelcomingMessage;
    public Text UsernameText;

    #endregion

    #region Private Constants

    const string glyphs = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    const string playerNamePrefKey = "PlayerName";


    #endregion

    #region MonoBehavior Callbacks
    void Start()
    {
        int usernameAmount = Random.Range(4, 8);
        string defaultName = string.Empty;

        for (int i = 0; i < usernameAmount; i++)
        {
            UsernameText.text += glyphs[Random.Range(0, glyphs.Length)];
        }

        if(UsernameText != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                UsernameText.text = defaultName;
            }
        }

        WelcomingMessage.text = "Welcome, " + defaultName;

        PhotonNetwork.NickName = defaultName;
    }
    #endregion

    #region Public Method

    public void SetPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("The Username is empty");
            return;
        }
        PhotonNetwork.NickName = value;

        PlayerPrefs.SetString(playerNamePrefKey, value);
    }

    #endregion
}
