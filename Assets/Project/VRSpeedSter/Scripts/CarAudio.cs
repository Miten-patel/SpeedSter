using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yudiz.CarVR.Managers;

namespace Yudiz.CarVR.CoreGamePlay
{
    public class CarAudio : MonoBehaviour
    {
        #region PUBLIC_VARS
        #endregion

        #region PRIVATE_VARS
        private CarController carController;
        private AudioSource audioSource;

        private float minSpeed = 0.1f;
        private float maxSpeed;
        private float minPitch = 0.9f;
        private float maxPitch = 2.4f;

        private bool isEngineRunning;
        #endregion

        #region UNITY_CALLBACKS
        private void OnEnable()
        {
            InputController.OnCarStart += StartCarSound;
            InputController.OnTriggeringCarHorn += TriggerCarHorn;
        }

        private void OnDisable()
        {
            InputController.OnCarStart -= StartCarSound;
            InputController.OnTriggeringCarHorn -= TriggerCarHorn;
        }

        private void Start()
        {
            carController = GetComponent<CarController>();
            audioSource = AudioManager.instance.audioSource;
            maxSpeed = carController.maxSpeed;                        
        }

        private void Update()
        {
            if (audioSource != null && isEngineRunning)
            {
                EngineSound();
            }
        }
        #endregion

        #region PUBLIC_FUNCTIONS
        #endregion

        #region PRIVATE_FUNCTIONS

        private void StartCarSound()
        {
            InputController.OnCarStart = null;
            StartCoroutine(SoundCooldown());
        }

        private void EngineSound()
        {
            float currentSpeed = carController.CarSpeedRigidBody();
            float carPitch = currentSpeed / maxSpeed;
            Debug.Log("CarPitch: " + carPitch);
            if (currentSpeed < minSpeed)
            {
                audioSource.pitch = minPitch;
            }

            if (currentSpeed > minSpeed && currentSpeed < maxSpeed)
            {
                audioSource.pitch = minPitch + carPitch;
            }

            if (currentSpeed > maxSpeed)
            {
                audioSource.pitch = maxPitch;
            }
        }


        private void TriggerCarHorn()
        {
            AudioManager.instance.PlaySound(AudioTrack.CarHorn, false);
        }
        #endregion

        #region CO-ROUTINES
        IEnumerator SoundCooldown()
        {           
            AudioManager.instance.PlaySound(AudioTrack.CarStart, false);
            yield return new WaitForSeconds(0.5f);
            AudioManager.instance.PlaySound(AudioTrack.CarEngine, true);
            isEngineRunning = true;            
        }
        #endregion

        #region EVENT_HANDLERS
        #endregion

        #region UI_CALLBACKS
        #endregion
    }
}
