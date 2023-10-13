using UnityEngine;
using UnityEngine.XR;
using Yudiz.CarVR.Managers;
using UnityEngine.XR.Interaction.Toolkit;

namespace Yudiz.CarVR.CoreGamePlay
{
    public class Hands : MonoBehaviour
    {
        #region PUBLIC_VARS
        //public Transform leftHand;
        //public Transform rightHand;
        private Vector3 bothHandIntitialPosManual;
        #endregion

        #region PRIVATE_VARS        
        [SerializeField] private SteeringWheel steeringWheel;

        [SerializeField] private GameObject leftHandController;
        [SerializeField] private GameObject rightHandController;

        private IXRSelectInteractor controller1;
        private IXRSelectInteractor controller2;

        private Vector3 bothHandIntitialPos;
        private Vector3 oneHandInitialPos;

        private Vector3 initialController1Position;
        private Vector3 initialController2Position;
        private Quaternion initialSteeringWheelRotation;

        

        private void BothHandInitialPosition2()
        {
            Debug.Log("Calculating BothHand Position");
            initialController1Position = controller1.transform.position; // right hand
            initialController2Position = controller2.transform.position; // Left hand
            initialSteeringWheelRotation = steeringWheel.transform.rotation;
        }
        #endregion

        #region UNITY_CALLBACKS
        private void Start()
        {
            Debug.Log("Inside - HandsScript");
            //if (steeringWheel == null) { Debug.Log("Steering Wheel Empty"); }

        }
        private void FixedUpdate()
        {
            CalculateHandAngle();
            //CalculateHandAngle2();
        }
        #endregion

        #region STATIC_FUNCTIONS
        #endregion

        #region PUBLIC_FUNCTIONS
        private void BothHandInitialPosition()
        {
            Debug.Log("Calculating BothHand Position");
            bothHandIntitialPos = controller1.transform.position - controller2.transform.position;
        }

        private void OneHandInitialPosition()
        {
            Debug.Log("Calculating OneHand Position");
            oneHandInitialPos = controller1.transform.position - transform.position;
        }

        public void OnEntered(SelectEnterEventArgs eventArgs)
        {
            ControllerHandSwitcher controller =
                eventArgs.interactorObject.transform.gameObject.GetComponent<ControllerHandSwitcher>();
            Debug.Log("Inside - OnEntered");
            if (controller1 == null)
            {
                Debug.Log("Controller1 is Touching");
                controller1 = eventArgs.interactorObject;
                //steeringWheel.wheelBeingHeld = true;
                OneHandInitialPosition();
            }
            else
            {
                Debug.Log("Controller2 is Touching");
                controller2 = eventArgs.interactorObject;
                //steeringWheel.wheelBeingHeld = true;
                BothHandInitialPosition();
            }
            
            // Enable Hands
            if (controller1 != null && controller2 != null)
            {
                // Both Hands
                leftHandController.SetActive(true);
                rightHandController.SetActive(true);
            }
            else if (controller1 != null && controller2 == null)
            {
                if (controller.handSide == HandSide.LeftHand)
                {
                    leftHandController.SetActive(true);
                    rightHandController.SetActive(false);
                }
                else if (controller.handSide == HandSide.RightHand)
                {
                    leftHandController.SetActive(false);
                    rightHandController.SetActive(true);
                }
            }
        }

        public void OnExit(SelectExitEventArgs eventArgs)
        {
            Debug.Log("Inside - OnExit");
            if (controller1 == eventArgs.interactorObject)
            {
                Debug.Log("Controller1 has Exited");
                //steeringWheel.wheelBeingHeld = false;
                controller1 = null;
            }
            else
            {
                Debug.Log("Controller2 has Exited");
                //steeringWheel.wheelBeingHeld = false;
                controller2 = null;
            }
            leftHandController.SetActive(false);
            rightHandController.SetActive(false);
        }
        #endregion

        #region PRIVATE_FUNCTIONS        
        private void CalculateHandAngle()
        {
            if (controller1 != null && controller2 != null)
            {
                Debug.Log("BothHands on Steering");
                steeringWheel.wheelBeingHeld = true;
                Vector3 newDifference = controller1.transform.position - controller2.transform.position;
                float angle = Vector3.SignedAngle(bothHandIntitialPos, newDifference, transform.forward);
                //Debug.LogWarning($"Angle:: {angle}");
                //steeringWheel.RotateSteeringWheelWithHands(angle);
                steeringWheel.RotateSteeringWithHands(angle);
                bothHandIntitialPos = newDifference;
            }
            else if (controller1 != null && controller2 == null)
            {
                Debug.Log("One hand on Steering");
                steeringWheel.wheelBeingHeld = true;
                Vector3 newDifference = controller1.transform.position - transform.position;
                float angle = Vector3.SignedAngle(oneHandInitialPos, newDifference, transform.forward);
                //steeringWheel.RotateSteeringWheelWithHands(angle);
                steeringWheel.RotateSteeringWithHands(angle);
                oneHandInitialPos = newDifference;
            }
            else if (controller1 == null && controller2 == null)
            {
                //steeringWheel.ResetSteeringWheel();
                steeringWheel.wheelBeingHeld = false;
            }
            else if (controller1 == null && controller2 != null)
            {
                steeringWheel.wheelBeingHeld = true;
            }
        }

        private void CalculateHandAngle2()
        {
            if (controller1 != null && controller2 != null)
            {
                Debug.Log("BothHands on Steering");

                // Calculate the current hand positions relative to their initial positions
                Vector3 currentController1Position = controller1.transform.position;
                Vector3 currentController2Position = controller2.transform.position;
                Vector3 handDifference = currentController1Position - currentController2Position;

                // Calculate the angle change based on the initial hand positions
                Quaternion initialRotationDifference = Quaternion.FromToRotation(
                    initialController1Position - initialController2Position,
                    handDifference
                );
                float angle = initialRotationDifference.eulerAngles.z;

                // Rotate the steering wheel
                steeringWheel.transform.rotation = initialSteeringWheelRotation * Quaternion.Euler(0, 0, angle);

                // Update the initial hand positions for the next frame
                initialController1Position = currentController1Position;
                initialController2Position = currentController2Position;
            }
        }

        //private void CalculateHandAngleManual()
        //{
        //    Vector3 newDifference = leftHand.transform.position - rightHand.transform.position;
        //    float angle = Vector3.SignedAngle(bothHandIntitialPosManual, newDifference, Vector3.forward);            
        //    steeringWheel.RotateSteeringWithHands(angle);
        //    bothHandIntitialPosManual = newDifference;
        //}

        //private void BothHandInitialPositionManual()
        //{
        //    Debug.Log("Calculating BothHand Position");
        //    bothHandIntitialPosManual = leftHand.transform.position - rightHand.transform.position;
        //}
        #endregion

        #region CO-ROUTINES
        #endregion

        #region EVENT_HANDLERS
        #endregion

        #region UI_CALLBACKS
        #endregion
    }
}