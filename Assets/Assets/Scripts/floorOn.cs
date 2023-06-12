using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class floorOn : MonoBehaviour ,IPunObservable
{
    public GameObject []floor;
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
           for(int i = 0; i < floor.Length; i++)
            {
                stream.SendNext(floor[i].activeInHierarchy);
            }
        }
        else
        {
            for(int i = 0;i < floor.Length; i++)
            {
                floor[i].SetActive((bool)stream.ReceiveNext());
            }
        }
    }
}
