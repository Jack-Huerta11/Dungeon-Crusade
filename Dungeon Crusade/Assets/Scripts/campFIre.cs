using UnityEngine;
using System.Collections;

public class campFire : Interactable
{
    public GameObject levelingPageCanvas;
    public float interactionCooldown = 1f; // Adjust as needed

    private bool canInteract = true;

    public override void Interact()
    {
        if (canInteract)
        {
            Debug.Log("Interact method called");

            if (levelingPageCanvas != null)
            {
                levelingPageCanvas.SetActive(true);
                Debug.Log("LevelingPage opened. Interact with the campfire to close the LevelingPage.");

                // Start cooldown timer
                StartCoroutine(InteractionCooldown());
            }
        }
    }

    IEnumerator InteractionCooldown()
    {
        canInteract = false;
        yield return new WaitForSeconds(interactionCooldown);
        canInteract = true;
    }

    void Start()
    {
        // Ensure the LevelingPage canvas is initially not visible
        if (levelingPageCanvas != null)
        {
            levelingPageCanvas.SetActive(false);
        }
    }
}
