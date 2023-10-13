using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yudiz.CarVR.CoreGamePlay
{
    public class XRPlayer : MonoBehaviour
    {
        public static XRPlayer instance;

        private void Awake()
        {
            instance = this;
        }

        public void TeleportPlayer(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
        }
    }
}