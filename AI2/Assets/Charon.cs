using UnityEngine;

public class CharonTrade : MonoBehaviour
{
    [SerializeField] private GameObject obol;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Collectable.hasBiscuit && Collectable.hasLiquidFire && Collectable.itemsAreStamped)
            {
                obol.SetActive(true);
                Debug.Log("Charon: 'A stamped offering... acceptable.'");

                // Reset everything
                Collectable.hasBiscuit = false;
                Collectable.hasLiquidFire = false;
                Collectable.itemsAreStamped = false;
            }
            else if (!Collectable.itemsAreStamped && Collectable.hasBiscuit)
            {
                Debug.Log("Charon points a bony finger toward the Clerk's desk.");
            }
        }
    }
}
