using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using ExitGames.Client.Photon;
using System;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    void Start()
    {
        OVRManager.fixedFoveatedRenderingLevel = OVRManager.FixedFoveatedRenderingLevel.High; // it's the maximum foveation level
        OVRManager.useDynamicFixedFoveatedRendering = true;

        if (PhotonNetwork.CurrentRoom != null)
        {
            CreatePlayer();
        }
    }
    
    public void CreatePlayer()
    {
        PhotonNetwork.Instantiate("PlayerLocal", new Vector3(0, 0, -0), Quaternion.Euler(0, 0, 0));
        Debug.Log("Pilot_muncul");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.ReconnectAndRejoin();
    }
    
}
