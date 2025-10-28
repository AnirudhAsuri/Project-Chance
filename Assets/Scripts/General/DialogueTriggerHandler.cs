using UnityEngine;
using System.Linq;

public class DialogueTriggerHandler : MonoBehaviour
{
    private DialogueBoxHandler dialogueBoxHandler;
    [SerializeField] private string inputDialogue;

    private string playerTag = "Player";

    private void Start()
    {
        dialogueBoxHandler = Resources.FindObjectsOfTypeAll<DialogueBoxHandler>().FirstOrDefault(s => s.gameObject.scene.IsValid());
        if (dialogueBoxHandler == null)
        {
            Debug.LogWarning("DialogueBoxHandler not found in the scene (maybe inactive or missing).");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(playerTag))
        {
            dialogueBoxHandler.HandleDialogueText(inputDialogue);
        }

        Destroy(gameObject);
    }
}