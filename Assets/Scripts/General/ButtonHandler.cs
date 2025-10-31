using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using TMPro;
using System;

public class ButtonHandler : MonoBehaviour
{
    public InputActionReference interactAction;

    private TextHandler textHandler;
    private TextMeshProUGUI textMesh;
    private string playerTag = "Player";

    [SerializeField] private UnityEvent ButtonAction;

    private void Awake()
    {
        textHandler = GetComponent<TextHandler>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void PressButton(InputAction.CallbackContext obj)
    {
        ButtonAction.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(playerTag))
        {
            textHandler.HandleTextAppearing(textMesh);

            interactAction.action.Enable();
            interactAction.action.started += PressButton;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag(playerTag))
        {
            textHandler.HandleTextDisappering(textMesh);

            interactAction.action.started -= PressButton;
            interactAction.action.Disable();
        }
    }
}