using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] private Transform rootObject, followObject;
    [SerializeField] private Vector3 positionOffset, rotationOffset, headBodyOffset;

    //private void Start()
    //{
    //    headBodyOffset = rootObject.position - followObject.position;
    //}

    private void LateUpdate()
    {
        //rootObject.position = transform.position + headBodyOffset;
        //rootObject.forward = Vector3.ProjectOnPlane(followObject.up, Vector3.up).normalized;
        rootObject.position = followObject.position + headBodyOffset;
        rootObject.forward = Vector3.ProjectOnPlane(followObject.forward, Vector3.up).normalized;
        //rootObject.rotation = followObject.rotation * Quaternion.Euler(rotationOffset);

        transform.position = followObject.TransformPoint(positionOffset);
        transform.rotation = followObject.rotation * Quaternion.Euler(rotationOffset);
    }
}
