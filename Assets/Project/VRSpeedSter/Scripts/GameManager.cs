using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Yudiz.CarVR.Camera;
using Yudiz.CarVR.CoreGamePlay;

namespace Yudiz.CarVR.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region PUBLIC_VARS

        #endregion

        #region PRIVATE_VARS
        [SerializeField] private CameraFollow cameraFollow;
        [SerializeField] private GameObject carPrefab;
        [SerializeField] private Transform spawnPoint;



        [Header("CheckPoints")]
        //[SerializeField] private List<Transform> checkPoints = new List<Transform>();
        private Transform lastCheckPoint;

        private GameObject carClone;

        public static Action OnRessetingMotor;

        #endregion

        private void OnEnable()
        {
            InputController.OnResetCarPos += ResetCarToNearestCheckPoint;
            CheckPointTrigger.onCarTrigger += StoreLastCheckPoint;
        }


        private void OnDisable()
        {
            InputController.OnResetCarPos -= ResetCarToNearestCheckPoint;
            CheckPointTrigger.onCarTrigger -= StoreLastCheckPoint;
        }


        #region UNITY_CALLBACKS
        private void Start()
        {
            carClone = Instantiate(carPrefab, spawnPoint.position, spawnPoint.rotation);
            if (carClone == null) { return; }
            cameraFollow.AssignTargetTransform(carClone.transform);
            cameraFollow.SetParentToCarView();
            cameraFollow.transform.localPosition = Vector3.zero;

            lastCheckPoint = spawnPoint;
        }
        #endregion

        #region PUBLIC_FUNCTIONS

        #endregion

        #region PRIVATE_FUNCTIONS

        private void ResetCarToNearestCheckPoint()
        {
            
            if (lastCheckPoint != null)
            {
                Debug.Log("Resseted Nearest Checkpoint");

                carClone.transform.position = lastCheckPoint.position;
                carClone.transform.rotation = lastCheckPoint.rotation;
                OnRessetingMotor?.Invoke();
            }
        }

        private void StoreLastCheckPoint(Transform lastCheckPointToStore)
        {
            lastCheckPoint = lastCheckPointToStore;
        }


        #endregion

        #region CO-ROUTINES

        #endregion

        #region EVENT_HANDLERS
        #endregion

        #region UI_CALLBACKS
        #endregion
    }
}
