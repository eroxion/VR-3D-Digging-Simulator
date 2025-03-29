using System;
using UnityEngine;

namespace Dig_Simulator
{
    public class ShovelController : MonoBehaviour
    {
        private XRIDefaultInputActions inputSystem;
        private void Awake()
        {
            inputSystem = new XRIDefaultInputActions();
            inputSystem.XRIRightInteraction.Enable();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Mud") && inputSystem.XRIRightInteraction.Activate.IsPressed())
            {
                Destroy(other.gameObject);
            }
        }
    }
}