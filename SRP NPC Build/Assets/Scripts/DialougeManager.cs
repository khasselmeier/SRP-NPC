using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    private bool isDialogueActive = false;

    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (dialogueText != null)
        {
            dialogueText.gameObject.SetActive(false);
        }
    }

    public void ShowDialogue(string message, float duration)
    {
        if (dialogueText != null && !isDialogueActive)
        {
            StartCoroutine(DisplayDialogue(message, duration));
        }
    }

    private IEnumerator DisplayDialogue(string message, float duration)
    {
        isDialogueActive = true;
        dialogueText.text = message;
        dialogueText.gameObject.SetActive(true);  // Show the text

        yield return new WaitForSeconds(duration);  // Wait for the specified duration

        dialogueText.gameObject.SetActive(false);  // Hide the dialogue text after the duration
        isDialogueActive = false;
    }
}