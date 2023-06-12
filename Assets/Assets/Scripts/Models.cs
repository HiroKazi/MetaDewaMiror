using System;
using System.Collections.Generic;
using UnityEngine;

public static class Models
{
    #region - Pilot2-

    public enum PlayerStance
    {
        Stand,
        Crouch,
    }

    [Serializable]
    public class PlayerSettingsModel
    {
        [Header("View Settings")]
        public float ViewXsensitivity;
        public float ViewYsensitivity;

        public bool ViewXInverted;
        public bool ViewYInverted;

        [Header("Movement")]

        public float WalkingForwardSpeed;
        public float WalkingBackwardSpeed;
        public float WalkingStrafeSpeed;

        [Header("Jumping")]
        public float JumpingHeight;
        public float JumpingFallOff;



    }
    [Serializable]
    public class CharacterStance
    {
        public float CamereHeight;
        public CapsuleCollider StanceCollider;
    }

    #endregion
}