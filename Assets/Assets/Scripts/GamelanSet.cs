using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class GamelanSet : MonoBehaviour , IPunObservable
{
    public gamelanTrigger []gamelantrigger;
    private PhotonView phView;
    private bool[] isPlaying;
    private void Start()
    {
        for(int i = 0; i < gamelantrigger.Length; i++)
        {
            gamelantrigger[i].gamelanID = i;
            gamelantrigger[i].triggered = gamelanisTriggered;
            
        }
        isPlaying = new bool[gamelantrigger.Length];
        phView = GetComponent<PhotonView>();
    }

    public void gamelanisTriggered(int gamelan)
    {
        if (!phView.IsMine)
        {
            phView.TransferOwnership(PhotonNetwork.LocalPlayer);
        }
        isPlaying[gamelan] = true;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            
            for(int z = 0; z < gamelantrigger.Length; z++)
            {
                
                stream.SendNext(isPlaying[z]);
                isPlaying[z] = false;

            }
        }
        else
        {
            for (int z = 0; z < gamelantrigger.Length; z++)
            {
                bool isPlaying = (bool)stream.ReceiveNext();
                if (isPlaying)
                    gamelantrigger[z].source.Play();

            }
        }
    }
}
