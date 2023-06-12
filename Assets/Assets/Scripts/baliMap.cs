using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Oculus.Interaction;
using System;

public class baliMap : MonoBehaviour , IPunObservable
{
    public GameObject []popUp;
    private PhotonView phView;
    

    // Start is called before the first frame update
    void Start()
    {
        
        phView = GetComponent<PhotonView>();
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            for(int a = 0;a < popUp.Length; a++)
            {
                stream.SendNext(popUp[a].activeInHierarchy);
                
            }
        }
        else
        {
            for (int a = 0; a < popUp.Length; a++)
            {
                
                popUp[a].SetActive((bool)stream.ReceiveNext());
            }
        }
    }
    // Update is called once per frame
    public void mapBaliSelected(int index)
    {
        if (!phView.IsMine)
        {
            phView.TransferOwnership(PhotonNetwork.LocalPlayer);
            
        }
    }
}
