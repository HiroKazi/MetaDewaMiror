using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using System;

public class NameTagPlayer : MonoBehaviourPun
{
    [SerializeField] private TMP_Text nameText;

    void Start()
    {
        if (photonView.IsMine)
        {
            return;
        }
        SetName();
    }
    private void SetName()
    {
        nameText.text = photonView.Owner.NickName;
    }
}
