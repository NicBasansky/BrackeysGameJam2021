using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jam.Dialogue;
using UnityStandardAssets.Characters.FirstPerson;
using System;

namespace Jam.Control
{
    public class PlayerInteracter : MonoBehaviour
    {
        public GameObject currentInteractable = null;
        
        FirstPersonController controller;
        ZoomLook zoom;

        float lineTraceRange = 3f;
        private bool interacting = false;
        private bool dialogueTriggered = false;


        private void OnEnable()
        {
            DialogueManager.Instance.onDialogueEnd += OnEndDialogue;
        }

        private void OnDisable()
        {
            DialogueManager.Instance.onDialogueEnd -= OnEndDialogue;
        }

        private void Awake()
        {
            controller = GetComponent<FirstPersonController>();
            zoom = GetComponent<ZoomLook>();
        }

        private void Update()
        {

            if (Input.GetButtonDown("Zoom") || Input.GetMouseButtonDown(1))
            {
                if (!interacting)
                {
                    zoom.ZoomIn();
                    ProcessRaycast();

                }
            }
            else if (Input.GetButtonUp("Zoom") || Input.GetMouseButtonUp(1))
            {
               
                zoom.ZoomOut();
            }

            if (dialogueTriggered && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
            {
                DisplayNextSentence();
            }
        }

        private void ProcessRaycast()
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, lineTraceRange))
            {
                var interactableObj = hit.collider.GetComponent<IInteractable>();
                if (interactableObj != null) // TODO need currentInteractable?
                {
                    currentInteractable = hit.transform.gameObject;
                    DisplayNextSentence();
                    interactableObj.OnInteract();
                }
            }
        }

        // START HERE try to discern from interacting and it is dialogue, physics pickup, or examinable
        private void DisplayNextSentence()
        {
            if (!interacting)
            {
                interacting = true;
                dialogueTriggered = true;
                controller.SetShouldFreeze(true);
            }
            else if (dialogueTriggered)
            {
                DialogueManager.Instance.DisplayNextSentence();
            }
        }

        private void OnEndDialogue() // TODO freeze movement
        {
            interacting = false;
            dialogueTriggered = false;
            currentInteractable = null;
            controller.SetShouldFreeze(false);
        }
    }
}
