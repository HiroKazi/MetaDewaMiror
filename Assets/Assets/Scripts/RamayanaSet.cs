using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Oculus.Interaction;
using System;
public class RamayanaSet : MonoBehaviour , IPunObservable
{
    public GameObject []popUpRamayana;
    private PhotonView phView;
    
    void Start()
    {
        phView = GetComponent<PhotonView>();
    }

    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            for(int i = 0;i < popUpRamayana.Length; i++)
            {
                stream.SendNext(popUpRamayana[i].activeInHierarchy);
            }
        }
        else
        {
            for (int i = 0; i < popUpRamayana.Length; i++)
            {
                popUpRamayana[i].SetActive((bool)stream.ReceiveNext());

            }
        }
    }
}
