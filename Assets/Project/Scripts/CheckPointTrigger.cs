using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Yudiz.CarVR.CoreGamePlay
{
    public class CheckPointTrigger : MonoBehaviour
    {
        public static Action<Transform> onCarTrigger;

        private void OnTriggerEnter(Collider other)
        {
            CarController car = other.gameObject.GetComponent<CarController>();

            if(car != null)
            {
                Debug.LogError("Car Exited");
                onCarTrigger?.Invoke(transform);
            }
        }
    }
}