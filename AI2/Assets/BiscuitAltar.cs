using UnityEngine;

public class BiscuitAltar : MonoBehaviour
{
    [SerializeField] private GameObject biscuitToShow; // Drag the hidden Biscuit here

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if BOTH boxes are checked
            if (Collectable.hasGloomroot && Collectable.hasEgg)
            {
                biscuitToShow.SetActive(true); // The Biscuit appears!

                // Optional: Reset the items so you have to find them again
                Collectable.hasGloomroot = false;
                Collectable.hasEgg = false;

                Debug.Log("The Gloomroot and Egg have combined into a Biscuit!");
            }
            else
            {
                // Helpful hint for the player
                string missing = "";
                if (!Collectable.hasGloomroot) missing += "Gloomroot ";
                if (!Collectable.hasEgg) missing += "Egg";
                Debug.Log("Missing ingredients: " + missing);
            }
        }
    }
}
