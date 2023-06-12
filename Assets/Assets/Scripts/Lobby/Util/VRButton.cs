using System;
using System.Collections.Generic;
using UnityEngine;


namespace MPI.VRpark.Lobby
{
    [RequireComponent(typeof(BoxCollider))]
    public class VRButton : MonoBehaviour
    {
        bool isButtonTouched = false;

        private PlayerLobby playerLobby;

        private BoxCollider buttonCollider;

        public Action OnClick;

        private void Awake()
        {
            buttonCollider = GetComponent<BoxCollider>();
        }

        // Update is called once per frame
        void Update()
        {
            if (playerLobby == null)
                playerLobby = PlayerLobby.instance;

            if (playerLobby != null)
            {
                if (buttonCollider.enabled)
                {
                    bool isRHandPointing = OVRInput.Get(OVRInput.RawButton.RHandTrigger, OVRInput.Controller.RTouch);
                    bool isLHandPointing = OVRInput.Get(OVRInput.RawButton.LHandTrigger, OVRInput.Controller.LTouch);

                    bool isTouchedByRightHand = isRHandPointing && playerLobby.RightIndexFinger.bounds.Intersects(buttonCollider.bounds);
                    bool isTouchedByLeftHand = isLHandPointing && playerLobby.LeftIndexFinger.bounds.Intersects(buttonCollider.bounds);

                    bool isTouched = isTouchedByRightHand || isTouchedByLeftHand;

                    // update button state
                    if (isButtonTouched != isTouched)
                    {
                        if (isTouched)
                        {

                        }
                        else
                        {
                            OnClick?.Invoke();
                        }

                        isButtonTouched = isTouched;
                    }
                    else
                    {
                        //OnButtonTouched
                    }

                    

                }
            }
        }
    }
}