using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class BackToLobby : MonoBehaviourPunCallbacks
{
    private PhotonView phView;

    private void Start()
    {
        phView = GetComponent<PhotonView>();
    }
    private void FixedUpdate()
    {
        if (PhotonNetwork.IsMasterClient == false)
        {
            return;
        }
    }

    
    public void LeaveRoom()
    {
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "ExitGame", "true" } });
    }
    public override void OnDisconnected(DisconnectCause cause)
    {

        SceneManager.LoadScene(0);
    }
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged["ExitGame"].ToString() == "true")
        {
            PhotonNetwork.Disconnect();
        }
    }
    IEnumerator GotoControlRoomAfterSecond(float time)
    {
        yield return new WaitForSeconds(time);
        PhotonNetwork.LoadLevel("Launcher");
        Debug.Log("GotoControlRoomAfterSecond");
    }
}