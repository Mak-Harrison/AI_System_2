using UnityEngine;

public class SkeletonClerk : MonoBehaviour
{
    [SerializeField] private GameObject stampVFX; // Optional: A "poof" or "stamp" particle
    [SerializeField] private GameObject uiPrompt; // Optional: "Press E to Appraise"

    private bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (uiPrompt != null) uiPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (uiPrompt != null) uiPrompt.SetActive(false);
        }
    }

    void Update()
    {
        // If player is close and has the goods, they can get the stamp
        if (playerInRange && Input.GetKeyDown(KeyCode.E)) // You can change 'E' to any key
        {
            TryAppraise();
        }
    }

    void TryAppraise()
    {
        if (Collectable.hasBiscuit && Collectable.hasLiquidFire)
        {
            Collectable.itemsAreStamped = true;

            if (stampVFX != null) Instantiate(stampVFX, transform.position, Quaternion.identity);

            Debug.Log("SKELETON CLERK: 'Everything seems to be in order. You may pass.'");

            // UI design moment: You could trigger an animation here!
        }
        else
        {
            Debug.Log("SKELETON CLERK: 'Incomplete paperwork! Come back with the Biscuit and the Fire.'");
        }
    }
}
