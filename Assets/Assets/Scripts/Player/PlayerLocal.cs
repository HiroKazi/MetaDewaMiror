using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using RootMotion.FinalIK;

public class PlayerLocal : MonoBehaviour, IPunObservable
{
    
    private SkinnedMeshRenderer avatarSkinRenderer;
    public Material hostMat;
    public Material guestMat;

    private Billboard nameTag;

    private OVRCameraRig cameraRig;

    public GameObject SoldierPrefab;
    private GameObject Soldier;
    private bool isLocal;

    private PhotonView pHView;
    private VRIK ikSoldier;



    private void Start()
    {
        pHView = GetComponent<PhotonView>();
        isLocal = pHView.IsMine;
        cameraRig = GameObject.Find("PLAYER/OVRCameraRig").GetComponent<OVRCameraRig>();
        Initialization();   
    }
    private void Update()
    {
        if (isLocal&& Soldier != null)
        {
            Soldier.transform.position = cameraRig.centerEyeAnchor.position - new Vector3(0, 1.6f, 0);
        }
        if (nameTag != null)
        {
            nameTag.transform.position = ikSoldier.solver.spine.IKPositionHead + new Vector3(0, 0.6f, 0);
        }
        
        
    }
    void Initialization()
    {
        
        Soldier = Instantiate(SoldierPrefab);
        ikSoldier = Soldier.GetComponent<VRIK>();
        avatarSkinRenderer = Soldier.GetComponentInChildren<SkinnedMeshRenderer>();
        nameTag = Soldier.GetComponentInChildren<Billboard>();
        
        if (pHView.Owner.IsMasterClient)
        {
            avatarSkinRenderer.material = hostMat;
        }
        else
        {
            avatarSkinRenderer.material = guestMat;
        }
        if(pHView.Owner.UserId == "Computer")
        {
            avatarSkinRenderer.enabled = false;
        }
        if (isLocal)
        {
            
            Soldier.transform.position = cameraRig.centerEyeAnchor.position - new Vector3(0, 1.6f, 0);
            Soldier.transform.localEulerAngles = Vector3.zero;

            Transform targetKepala = cameraRig.centerEyeAnchor.Find("HeadTarget");

            Transform targetTanganKanan = cameraRig.rightHandAnchor.Find("RightHandTarget");
            Transform targetTanganKiri = cameraRig.leftHandAnchor.Find("LeftHandTarget");


            ikSoldier.solver.rightArm.target = targetTanganKanan;
            ikSoldier.solver.leftArm.target = targetTanganKiri;
            ikSoldier.solver.spine.headTarget = targetKepala;
            ikSoldier.solver.spine.pelvisTarget = targetKepala;

            nameTag.gameObject.SetActive(false);
        }
        else
        {
          

            Soldier.transform.SetParent(transform);
            Soldier.transform.localPosition = Vector3.zero;
            Soldier.transform.localEulerAngles = Vector3.zero;


            transform.SetParent(PlayerManagerMD.instance.transform);
            nameTag.cam = cameraRig.centerEyeAnchor;
            nameTag.GetComponent<TMPro.TMP_Text>().text = pHView.Owner.UserId;

        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {
            // We own this player: send the others our data

            stream.SendNext(ikSoldier.solver.spine.IKPositionHead);
            stream.SendNext(ikSoldier.solver.spine.IKRotationHead);

            stream.SendNext(ikSoldier.solver.leftArm.IKPosition);
            stream.SendNext(ikSoldier.solver.leftArm.IKRotation);

            stream.SendNext(ikSoldier.solver.rightArm.IKPosition);
            stream.SendNext(ikSoldier.solver.rightArm.IKRotation);
        }
        else
        {
            // Network player, receive data
           


            ikSoldier.solver.spine.IKPositionHead = (Vector3)stream.ReceiveNext();
            ikSoldier.solver.spine.IKRotationHead = (Quaternion)stream.ReceiveNext();

            ikSoldier.solver.leftArm.IKPosition = (Vector3)stream.ReceiveNext();
            ikSoldier.solver.leftArm.IKRotation = (Quaternion)stream.ReceiveNext();

            ikSoldier.solver.rightArm.IKPosition = (Vector3)stream.ReceiveNext();
            ikSoldier.solver.rightArm.IKRotation = (Quaternion)stream.ReceiveNext();

            Debug.Log("Soldier bisa gerak");
        }

    }

}