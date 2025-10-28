using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class DialogueBoxHandler : MonoBehaviour
{
    private TextMeshProUGUI dialogueText;
    private PlayerMovement playerMovement;

    public InputActionReference interactAction;

    [SerializeField] private float typingSpeed = 0.03f;
    private Coroutine typingCoroutine;
    private string dialogueToType;

    private bool isTyping = false;

    private void Awake()
    {
        dialogueText = GetComponentInChildren<TextMeshProUGUI>();
        if (dialogueText == null)
        {
            Debug.LogError("DiaglogueBoxHandler: No TextMeshProUGUI component found in children!", this);
        }

        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }

    private void OnEnable()
    {
        // 2. We still disable the player and start the coroutine here.
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if (!string.IsNullOrEmpty(dialogueToType))
        {
            if (typingCoroutine != null) StopCoroutine(typingCoroutine);
            typingCoroutine = StartCoroutine(TypeDialogue(dialogueToType));
        }
    }

    private void OnDisable()
    {
        // 1. Re-enable the player
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        // 2. Standard cleanup
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
            isTyping = false;
        }
        dialogueToType = "";
    }

    /// <summary>
    /// This function runs every single frame the dialogue box is active.
    /// </summary>
    private void Update()
    {
        if (interactAction == null || interactAction.action == null) return;

        // Check if the interact button was pressed down *on this frame*
        if (interactAction.action.WasPressedThisFrame())
        {
            // If we are past that first frame, check our logic:
            if (isTyping)
            {
                // --- SKIP LOGIC ---
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                }
                dialogueText.maxVisibleCharacters = dialogueText.text.Length;
                isTyping = false;
                typingCoroutine = null;
            }
            else
            {
                // --- CLOSE LOGIC ---
                // If not typing, the text is finished. Close the box.
                gameObject.SetActive(false); // This will trigger OnDisable
            }
        }
    }

    public void HandleDialogueText(string dialogue)
    {
        if (dialogue == null)
        {
            Debug.LogError("Cannot enter dialogue: dialogue is null!");
            return;
        }

        // Store the text and activate the object.
        // OnEnable() and Update() will handle the rest.
        dialogueToType = dialogue;
        gameObject.SetActive(true);
    }

    private IEnumerator TypeDialogue(string dialogue)
    {
        if (dialogueText == null)
        {
            isTyping = false;
            yield break;
        }

        isTyping = true;
        dialogueText.text = dialogue;
        dialogueText.maxVisibleCharacters = 0;

        for (int i = 0; i < dialogue.Length; i++)
        {
            dialogueText.maxVisibleCharacters = i + 1;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        typingCoroutine = null;
    }
}