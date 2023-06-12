using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Oculus.Interaction;
using System;

public class NM_MDE : NetworkBehaviour
{

    [SerializeField] private InteractableUnityEventWrapper[] raycastObjects;

    [SerializeField] private GameObject[] popupObjects;




    private SyncList<bool> isHover = new SyncList<bool>();

    [SyncVar] int bagian_jantung;
    [SyncVar] int bagian_paru;

    /*
     public List<SyncObject> bagianJantung = new List<SyncObject>();
    public List<SyncObject> bagianParu = new List<SyncObject>();*/
    // Start is called before the first frame update

    void Start()
    {
        //first.WhenHover.AddListener(() => { isHoverFirst = true; });
        //first.WhenUnhover.AddListener(() => { isHoverFirst = false; });

        //second.WhenHover.AddListener(() => { isHoverFirst = true; });
        //second.WhenUnhover.AddListener(() => { isHoverFirst = false; });

        //third.WhenHover.AddListener(() => { isHoverFirst = true; });
        //third.WhenUnhover.AddListener(() => { isHoverFirst = false; });

        //fourth.WhenHover.AddListener(() => { isHoverFirst = true; });
        //fourth.WhenUnhover.AddListener(() => { isHoverFirst = false; });

        //fifth.WhenHover.AddListener(() => { isHoverFirst = true; });
        //fifth.WhenUnhover.AddListener(() => { isHoverFirst = false; });

        //sixth.WhenHover.AddListener(() => { isHoverFirst = true; });
        //sixth.WhenUnhover.AddListener(() => { isHoverFirst = false; });

        //seventh.WhenHover.AddListener(() => { isHoverFirst = true; });
        //seventh.WhenUnhover.AddListener(() => { isHoverFirst = false; });

        //eighth.WhenHover.AddListener(() => { isHoverFirst = true; });
        //eighth.WhenUnhover.AddListener(() => { isHoverFirst = false; });



        for (int i = 0; i < raycastObjects.Length; i++)
        {
            int index = i;
            InteractableUnityEventWrapper eventWrapper = raycastObjects[index];
            eventWrapper.WhenHover.AddListener(() => { CmdHoverComponnent(index, true); });
            eventWrapper.WhenUnhover.AddListener(() => { CmdHoverComponnent(index, false); });
        }
        isHover.Callback += updateVisibility;
        initServer();
    }

    // Update is called once per frame
    void updateVisibility(SyncList<bool>.Operation op, int index, bool olditem, bool newItem)
    {
        switch (op)
        {
            case SyncList<bool>.Operation.OP_INSERT: isHover.Add(newItem); break;
            case SyncList<bool>.Operation.OP_SET: popupObjects[index].SetActive(newItem); break;

        }
        
    }
    [Command(requiresAuthority = false)]
    public void CmdHoverComponnent(int index, bool value)
    {
        isHover[index] = value;
    }


    [Server]
    private void initServer()
    {
        for (int i = 0; i < raycastObjects.Length; i++)
            isHover.Add(false);
    }
}
