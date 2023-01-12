using System;
using System.Timers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class Swimmer : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float swimForce = 2f;
    [SerializeField] private float dragForce = 1f;
    [SerializeField] private float minForce;
    [SerializeField] private float minTimeBetweenStrokes;
    [Header("References")]
    [SerializeField] private InputActionReference leftControllerSwimReference;
    [SerializeField] private InputActionReference leftControllerVelocity;
    [SerializeField] private InputActionReference rightControllerSwimReference;
    [SerializeField] private InputActionReference rightControllerVelocity;
    [SerializeField] private Transform trackingReference;

    private Rigidbody _rigidbody;
    private float _cooldownTimer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void FixedUpdate()
    {
        _cooldownTimer += Time.fixedDeltaTime;
        if (_cooldownTimer > minTimeBetweenStrokes
            && leftControllerSwimReference.action.IsPressed()
            && rightControllerSwimReference.action.IsPressed())
        {
            var leftHandVelocity = leftControllerVelocity.action.ReadValue<Vector3>();
            var rightHandVelocity = rightControllerVelocity.action.ReadValue<Vector3>();
            Vector3 localVelocity = leftHandVelocity + rightHandVelocity;
            localVelocity *= -1;

            if (localVelocity.sqrMagnitude > minForce * minForce)
            {
                Vector3 worldVelocity = trackingReference.TransformDirection(localVelocity);
                _rigidbody.AddForce(worldVelocity * swimForce, ForceMode.Acceleration);
                _cooldownTimer = 0f;
            }
        }

        if (_rigidbody.velocity.sqrMagnitude > 0.01f)
        {
            _rigidbody.AddForce(-_rigidbody.velocity * dragForce, ForceMode.Acceleration);
        }
    }
}
 