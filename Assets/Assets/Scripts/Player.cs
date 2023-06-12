/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Models;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    
    public Vector2 input_Movement;
    public Vector2 input_View;

    private Vector3 newCameraRotation;
    private Vector3 newCharacterRotation;

    [Header("References")]
    public Transform CamHolder;

    [Header("Settings")]
    public PlayerSettingsModel playerSettings;
    public float viewClampYMin = -70;
    public float viewClampYMax = 80;

    [Header("Gravity")]

    public float gravityAmount;
    public float gravityMin;
    private float playerGravity;

    public Vector3 jumpingForce;
    private Vector3 jumpingForceVelocity;

    [Header("stance")]
    public PlayerStance playerStance;
    public float playerStanceSmoothing;
    public float cameraStandHeight;
    public float cameraCrouchHeight;

    private float cameraHeight;
    private float cameraHeightVelocity;

    private void Awake()
    {
        defaultInput = new DefaultInput();

        defaultInput.Character.Movement.performed += e => input_Movement = e.ReadValue<Vector2>();
        defaultInput.Character.View.performed += e => input_View = e.ReadValue<Vector2>();
        defaultInput.Character.Jump.performed += e => Jump();

        defaultInput.Enable();

        newCameraRotation = CamHolder.localRotation.eulerAngles;
        newCharacterRotation = transform.localRotation.eulerAngles;

        characterController = GetComponent<CharacterController>();

        cameraHeight = CamHolder.localPosition.y;

    }

    private void Update()
    {
        CalculateView();
        CalculateMovement();
        CalculateJump();
        CalculateCameraHeight();


    }

    private void CalculateView()
    {
        newCharacterRotation.y += playerSettings.ViewXsensitivity * (playerSettings.ViewXInverted ? -input_View.x : input_View.x) * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(newCharacterRotation);

        newCameraRotation.x += playerSettings.ViewYsensitivity * (playerSettings.ViewYInverted ? input_View.y : -input_View.y) * Time.deltaTime;
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x, viewClampYMin, viewClampYMax);

        CamHolder.localRotation = Quaternion.Euler(newCameraRotation);
    }

    private void CalculateMovement()
    {

        var verticalSpeed = playerSettings.WalkingForwardSpeed * input_Movement.y * Time.deltaTime;
        var horizontalSpeed = playerSettings.WalkingStrafeSpeed * input_Movement.x * Time.deltaTime;

        var newMovementSpeed = new Vector3(horizontalSpeed, 0, verticalSpeed);
        newMovementSpeed = transform.TransformDirection(newMovementSpeed);

        if (playerGravity > gravityMin && jumpingForce.y < 0.1f)
        {
            playerGravity -= gravityAmount * Time.deltaTime;
        }

        if (playerGravity < -1 && characterController.isGrounded)
        {
            playerGravity = -1;
        }

        if (jumpingForce.y > 0.1f)
        {
            playerGravity = 0;
        }

        newMovementSpeed.y += playerGravity;
        newMovementSpeed += jumpingForce * Time.deltaTime;

        characterController.Move(newMovementSpeed);
    }

    private void CalculateJump()
    {
        jumpingForce = Vector3.SmoothDamp(jumpingForce, Vector3.zero, ref jumpingForceVelocity, playerSettings.JumpingFallOff);
    }

    private void CalculateCameraHeight()
    {
        float stanceHeight = cameraStandHeight;

        if (playerStance == PlayerStance.Crouch)
        {
            stanceHeight = cameraCrouchHeight;
        }



        cameraHeight = Mathf.SmoothDamp(CamHolder.localPosition.y, stanceHeight, ref cameraHeightVelocity, playerStanceSmoothing);

        CamHolder.localPosition = new Vector3(CamHolder.localPosition.x, cameraHeight, CamHolder.localPosition.z);

        ;
    }
    private void Jump()
    {
        if (!characterController.isGrounded)
        {
            return;
        }
        jumpingForce = Vector3.up * playerSettings.JumpingHeight;
    }
}*/