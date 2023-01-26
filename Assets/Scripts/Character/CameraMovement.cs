using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Looktarget;
    public Transform FollowTarget;
    public Vector3 Offset = new Vector3(0,3,-10);
    public float smoothTime = 10f;

    private Vector3 velocity;
    private void LateUpdate()
    {
        //transform.position = Vector3.SmoothDamp(transform.position, FollowTarget.position + Offset, ref velocity, smoothTime);
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, FollowTarget.position + Offset, ref velocity, smoothTime);

    }
}
