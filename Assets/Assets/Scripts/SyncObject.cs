using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Mirror.Examples.Operator
{
    public class SyncObject : NetworkBehaviour
    {
        public GameObject jantung;
        public GameObject paruParu;

        [SyncVar] int itemOrgan;





        [Command(requiresAuthority = false)]
        public void CmdJantung()
        {
            itemOrgan = 1;

            Debug.Log("jantung cmd");
        }
        [Command(requiresAuthority = false)]
        public void CmdParuParu()
        {
            itemOrgan = 2;

            Debug.Log("paru cmd");
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {

                CmdJantung();

                Debug.Log("jantung button");
            }
            if (Input.GetKeyDown(KeyCode.B))
            {

                CmdParuParu();

                Debug.Log("paru paru button");
            }
            if (itemOrgan == 1)//jantung
            {
                jantung.gameObject.SetActive(true);
                paruParu.gameObject.SetActive(false);
            }
            if (itemOrgan == 2)//paru paru
            {
                jantung.gameObject.SetActive(false);
                paruParu.gameObject.SetActive(true);
            }

        }


    }
}