using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSteering : MonoBehaviour
{
    public WheelCollider wheelCollider;

    public Transform wheelTransform;
    float NewValue;
    //private Transform temp;

    Vector3 currentRotation;

    //private void Start()
    //{
    //    //temp = transform;
    //    //currentRotation = transform.rotation.eulerAngles;
    //}

    private void Update()
    {
        Debug.Log($"Rotation : {transform.rotation.eulerAngles.z}");
        currentRotation = transform.rotation.eulerAngles;
        if (currentRotation.z < 180 && currentRotation.z > -180)// && currentRotation.z < 361)
        {
            //NewValue = Map(currentRotation.z, 360, 180, 0, -180);
            NewValue = currentRotation.z;
        }
        //else
        //{
        //NewValue = currentRotation.z;
        //}
        Debug.LogWarning($"NewValue :: {NewValue}");
        //float steerValue = Map(NewValue, -180, 180, -1, 1);
        //wheelCollider.steerAngle = Mathf.Clamp(NewValue, -40, 40);
        wheelCollider.steerAngle = NewValue;

        UpdateWheels();
        //wheelCollider.steerAngle

    }

    private void ClampSteering()
    {
        float angle = transform.rotation.eulerAngles.z;
    }

    public float Map(float value, float leftMin, float leftMax, float rightMin, float rightMax)
    {
        return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
    }

    void UpdateWheels()
    {
        UpdateSingleWheel(wheelCollider, wheelTransform);
    }

    void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;

        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
