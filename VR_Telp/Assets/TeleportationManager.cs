using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationManager : MonoBehaviour
{
    [SerializeField]    private InputActionAsset actionAsset;
    [SerializeField]    private XRRayInteractor rayInteractor;
    [SerializeField]    private TeleportationProvider provider;

    private InputAction _thumbstick;
    private bool _isActive;

    private void Start()
    {

        rayInteractor.enabled = false;

        //var activate = actionAsset.FindActionMap("XRI LeftHand").FindAction("Teleport Mode Activate");
        var activate = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Activate");
        activate.Enable();
        activate.performed += OnTeleportActivate;

        var cancel = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Cancel");
        cancel.Enable();
        cancel.performed += OnTeleportCancel;

        _thumbstick = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Move");
        _thumbstick.Enable();
    }

    private void Update()
    {
        if (!_isActive)
            return;

        if (_thumbstick.triggered)
            return;
        
        if(!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            rayInteractor.enabled = false;
            _isActive = false;
            return;
        }

        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = hit.point,
            destinationRotation = Quaternion.Euler(0, gameObject.GetComponentInParent<Transform>().rotation.y, 0),
            matchOrientation = MatchOrientation.TargetUp,
            requestTime = 1f
        };

        provider.QueueTeleportRequest(request);
    }

    private void OnTeleportActivate(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = true;
        _isActive = true;
    }

    private void OnTeleportCancel(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = false;
        _isActive = false;
    }
}
