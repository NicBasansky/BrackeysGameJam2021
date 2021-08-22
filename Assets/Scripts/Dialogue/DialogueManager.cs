using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using MoreMountains.Tools;

// NPCs might have a speaker title and image so the text box size would differ if it was 
// just an interactable object. So two different text box GOs need to be considered, NPC
// or non-NPC. For now, everything is just one text box size so we can comment out everything
// for NPC specific dialogue
namespace Jam.Dialogue
{
    public class DialogueManager : MMSingleton<DialogueManager>
    {
        [SerializeField] float printSpeed = 0.1f;
        [SerializeField] TMP_Text nameText;
        [SerializeField] TMP_Text sentenceTextNPC;
        [SerializeField] TMP_Text sentenceTextInteractable;
        [SerializeField] GameObject NPC;
        [SerializeField] Image speakerImage;

        [SerializeField] Animator animator;

        public event Action onDialogueEnd;

        private Queue<string> sentences;
        private bool isNPC = false;
        //private Store store = null;
        private bool hasShop = false;

        private void Awake()
        {
            sentences = new Queue<string>();
        }

        // void Start()
        // {
        //     animator.SetBool("isOpen", true);
        // }

        public void StartDialogue(Dialogue dialogue)
        {
            animator.SetBool("isOpen", true);

            // if (dialogue.isNPC)
            // {
            //     nameText.text = dialogue.speakerName;
            //     speakerImage.sprite = dialogue.speakerImage;
            //     isNPC = true;
            // }
            // else
            // {
            //     isNPC = false;
            // }

            sentences.Clear();
            foreach (var sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
               
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();

            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

        private IEnumerator TypeSentence(string sentence)
        {
            SetupTextBox(isNPC);

            if (isNPC)
            {
                foreach (char letter in sentence.ToCharArray())
                {
                    sentenceTextNPC.text += letter;
                    yield return new WaitForSeconds(printSpeed);
                }
            }
            else
            {
                foreach (char letter in sentence.ToCharArray())
                {
                    sentenceTextInteractable.text += letter;
                    yield return new WaitForSeconds(printSpeed);
                }
            }

        }

        private void EndDialogue()
        {
            animator.SetBool("isOpen", false);
            onDialogueEnd();
        }

        private void SetupTextBox(bool isPerson)
        {
            if (NPC != null)
            {
                NPC.SetActive(isPerson);
            }
            sentenceTextInteractable.gameObject.SetActive(!isPerson);

            //sentenceTextNPC.text = "";
            sentenceTextInteractable.text = "";
        }

    }

}
