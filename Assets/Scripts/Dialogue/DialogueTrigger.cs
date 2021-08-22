using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jam.Control;

namespace Jam.Dialogue
{
    public class DialogueTrigger : MonoBehaviour, IInteractable
    {
        public Dialogue dialogue;
        bool hasShop = false;

        

        public void TriggerDialogue()
        {
            //FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            DialogueManager.Instance.StartDialogue(dialogue);
            //NPC_Controller aiController = GetComponent<NPC_Controller>();
            //if (aiController == null) return;
            // aiController.FacePlayer(playerPos);
        }

        public void OnInteract()
        {
            TriggerDialogue();
        }
    }

}