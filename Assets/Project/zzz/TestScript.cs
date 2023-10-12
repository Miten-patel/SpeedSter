using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] int yolo;
    [SerializeField] private WheelCollider frontLeftWC, frontRightWC, backLeftWC, backRightWC;
    [SerializeField] private Transform frontLeftWheelTranform, frontRightWheelTranform, backLeftWheelTranform, backRightWheelTranform;
    private void FixedUpdate()
    {
        float InputValue = Input.GetAxis("Vertical");
        if (Input.GetButton("Vertical"))
        {            
            frontLeftWC.brakeTorque = 0;
            frontRightWC.brakeTorque = 0;
        }
        backLeftWC.motorTorque = InputValue * 200f;
        backRightWC.motorTorque = InputValue * 200f;
        frontLeftWC.motorTorque = InputValue * 200f;
        frontRightWC.motorTorque = InputValue * 200f;

        if (Input.GetButton("Jump"))
        {
            Debug.Log("Breaking");
            frontLeftWC.brakeTorque = yolo;
            frontRightWC.brakeTorque = yolo;
        }
        UpdateWheels();

    }

    private void Update()
    {
        Debug.Log(frontLeftWC.brakeTorque);
    }

    void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWC, frontLeftWheelTranform);
        UpdateSingleWheel(frontRightWC, frontRightWheelTranform);
        UpdateSingleWheel(backLeftWC, backLeftWheelTranform);
        UpdateSingleWheel(backRightWC, backRightWheelTranform);
    }

    void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;

        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }


    //float lerpDuration = 3;
    //float startValue = 0;
    //float endValue = 0;
    //float rotationValue;
    //Vector3 currentRotation;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    ResetRotation();
    //    //ResetRotation();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //}

    //private void ResetRotation()
    //{

    //    StartCoroutine(Lerp());

    //}

    //IEnumerator Lerp()
    //{
    //    float timeElapsed = 0;
    //    while (timeElapsed < lerpDuration)
    //    {
    //         currentRotation = transform.rotation.eulerAngles;
    //        rotationValue = Mathf.Lerp(transform.rotation.eulerAngles.z, endValue, timeElapsed / lerpDuration);
    //        timeElapsed += Time.deltaTime;
    //        currentRotation.z = -rotationValue;
    //        transform.rotation = Quaternion.Euler(currentRotation);
    //        yield return null;
    //    }
    //    currentRotation.z = endValue;
    //    transform.rotation = Quaternion.Euler(currentRotation);
    //}


    ////private void ResetRotation()
    ////{
    ////    StartCoroutine(LerpRotation());
    ////}

    //IEnumerator LerpRotation()
    //{
    //    float timeElapsed = 0;
    //    Quaternion initialRotation = transform.rotation;
    //    float startValue = transform.rotation.eulerAngles.z;

    //    while (timeElapsed < lerpDuration)
    //    {
    //        timeElapsed += Time.deltaTime;
    //        float t = timeElapsed / lerpDuration;
    //        float newRotationValue = Mathf.Lerp(startValue, endValue, t);
    //        Vector3 currentRotation = transform.rotation.eulerAngles;
    //        currentRotation.z = newRotationValue;
    //        transform.rotation = Quaternion.Euler(currentRotation);
    //        yield return null;
    //    }

    //    Vector3 finalRotation = transform.rotation.eulerAngles;
    //    finalRotation.z = endValue;
    //    transform.rotation = Quaternion.Euler(finalRotation);
    //}



    //// Call this method to initiate the reset
    ////public void StartReset(float targetRotationValue, float duration)
    ////{
    ////    endValue = targetRotationValue;
    ////    lerpDuration = duration;
    ////    ResetRotation();
    ////}

}
