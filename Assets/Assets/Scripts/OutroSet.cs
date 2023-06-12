using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class OutroSet : MonoBehaviour , IPunObservable
{
    public GameObject[] bahanOutro;
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
            for (int i = 0; i < bahanOutro.Length; i++)
            {
                stream.SendNext(bahanOutro[i].activeInHierarchy);
            }
        }
        else
        {
            for (int i = 0; i < bahanOutro.Length; i++)
            {
                bahanOutro[i].SetActive((bool)stream.ReceiveNext());
            }
        }
    }
}
