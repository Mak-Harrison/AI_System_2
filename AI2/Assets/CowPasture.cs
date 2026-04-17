using UnityEngine;

public class CowPasture : MonoBehaviour
{
    [SerializeField] private GameObject liquidFireToShow; // Drag hidden Liquid Fire here

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Collectable.hasFeed)
            {
                liquidFireToShow.SetActive(true);
                Collectable.hasFeed = false; // Feed is used up
                Collectable.hasLiquidFire = true; // Player now has the fire

                Debug.Log("You fed the cows! Liquid Fire obtained.");
            }
            else if (!Collectable.hasLiquidFire)
            {
                Debug.Log("The cows are already fed and the fire is burning.");
            }
        }
    }
}
