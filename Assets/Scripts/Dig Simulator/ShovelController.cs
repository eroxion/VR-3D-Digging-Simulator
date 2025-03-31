using System;
using System.Collections;
using UnityEngine;

namespace Dig_Simulator
{
    public class ShovelController : MonoBehaviour
    {
        private XRIDefaultInputActions inputSystem;
        private const string IS_DIGGING = "Dig";
        private bool diggingBool = false;
        private Animator animator;
        
        private void Awake()
        {
            animator = GetComponent<Animator>();
            inputSystem = new XRIDefaultInputActions();
            inputSystem.XRIRightInteraction.Enable();
        }

        private void Update(){
            animator.SetBool(IS_DIGGING, diggingBool);
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Mud") && inputSystem.XRIRightInteraction.Activate.IsPressed() && diggingBool == false)
            {
                diggingBool = true;
                Destroy(other.gameObject);
                StartCoroutine(diggingDelay());
            }
        }

        private IEnumerator diggingDelay()
        {
            yield return new WaitForSeconds(1f);
            diggingBool = false;
        }
    }
}