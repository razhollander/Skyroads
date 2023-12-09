﻿using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public float Distance = 10.0f;
    public float Height = 5.0f;
    public float HeightDamping = 2.0f;
    public float RotationDamping = 3.0f;
    [SerializeField] private Transform _target;
    [SerializeField] private ZoomConfig _zoomInConfig;
    [SerializeField] private ZoomConfig _zoomOutConfig;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void ChangeZoom(bool isZoomIn)
    {
        var zoomConfig = isZoomIn ? _zoomInConfig : _zoomOutConfig;
        ChangeZoom(zoomConfig);
    }
    
    private void ChangeZoom(ZoomConfig zoomConfig)
    {
        Height = zoomConfig.Height;
        Distance = zoomConfig.Distance;
    }
    
    private void LateUpdate()
    {
        // Early out if we don't have a target
        if (!_target)
        {
            return;
        }

        // Calculate the current rotation angles
        var wantedRotationAngle = _target.eulerAngles.y;
        var wantedHeight = _target.position.y + Height;

        var currentRotationAngle = transform.eulerAngles.y;
        var currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, RotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, HeightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        var pos = transform.position;
        pos = _target.position - currentRotation * Vector3.forward * Distance;
        pos.y = currentHeight;
        transform.position = pos;

        // Always look at the target
        transform.LookAt(_target);
    }
    
    [System.Serializable]
    private class ZoomConfig
    {
        public float Height;
        public float Distance;
    }
}

