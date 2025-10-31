using UnityEngine;

public class DropBoxManager : MonoBehaviour
{
    [SerializeField] private GameObject box;
    public Transform boxTransform;
    private GameObject newBox;

    public void DropBox()
    {
        // --- ADD THESE CHECKS ---
        if (box == null)
        {
            Debug.LogError("The 'box' prefab is not assigned in the Inspector!");
            return; // Stop the function here to prevent a crash
        }

        if (boxTransform == null)
        {
            Debug.LogError("The 'boxTransform' is not assigned in the Inspector!");
            return; // Stop the function here to prevent a crash
        }
        // --- END OF CHECKS ---

        // Your original logic is good.
        // If a box already exists, destroy it.
        if (newBox != null)
        {
            Destroy(newBox);
        }

        // Now you can be 100% sure this line will work
        newBox = Instantiate(box, boxTransform);

        if (newBox != null)
        {
            Debug.Log("SUCCESS: Spawned '" + newBox.name + "'!");
            Debug.Log("New box is active in hierarchy: " + newBox.GetComponentInParent<Collider2D>().name);
        }
        else
        {
            Debug.LogError("Instantiate FAILED for some reason.");
        }
    }
}