using UnityEngine;

public class GloomField : MonoBehaviour
{
    // This creates a box in the Inspector for you to drag the plant into
    public GameObject plantToSpawn;

    private void OnTriggerEnter(Collider other)
    {
        // Debug: This tells us if the AI/Player actually touched the field
        Debug.Log("Object touched the field: " + other.name);

        if (Collectable.hasSoul)
        {
            // If the AI or the Player touches this area
            if (other.CompareTag("Player") || other.GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
            {
                SpawnPlant();
            }
        }
    }

    void SpawnPlant()
    {
        if (plantToSpawn != null)
        {
            Collectable.hasSoul = false; // Spend the soul
            plantToSpawn.SetActive(true); // Make the plant appear
            Debug.Log("The plant is now visible!");
        }
        else
        {
            Debug.LogError("The 'Plant To Spawn' box is EMPTY! Drag your plant into the Inspector.");
        }
    }
}