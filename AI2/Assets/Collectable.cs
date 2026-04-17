using UnityEngine;

public enum ItemType { Soul, Egg, Gloomroot, Biscuit, Feed, LiquidFire, Obol }

public class Collectable : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private string aiTag = "AI"; // Added for your AI

    public static bool hasSoul = false;
    public static bool hasGloomroot = false;
    public static bool hasEgg = false;
    public static bool hasBiscuit = false;
    public static bool hasFeed = true;
    public static bool hasLiquidFire = false;
    public static bool hasObol = false;
    public static bool itemsAreStamped = false;

    public ItemType itemType;

    // FIX: 3D Trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) || other.CompareTag(aiTag))
        {
            HandleCollection(other.gameObject);
        }
    }

    // FIX: 2D Trigger (matches the new function signature)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) || other.CompareTag(aiTag))
        {
            HandleCollection(other.gameObject);
        }
    }

    private void HandleCollection(GameObject collector)
    {
        // 1. Try to find the inventory ONLY if it's the player
        if (collector.CompareTag(playerTag))
        {
            PlayerInventory inv = collector.GetComponent<PlayerInventory>();
            if (inv != null)
            {
                inv.UpdateCount(itemType, 1);
            }
        }

        // 2. Automatically detect item type based on the object's Tag
        if (gameObject.CompareTag("Soul")) itemType = ItemType.Soul;
        else if (gameObject.CompareTag("Egg")) itemType = ItemType.Egg;
        else if (gameObject.CompareTag("Gloomroot")) itemType = ItemType.Gloomroot;

        // 3. Update the static bools
        switch (itemType)
        {
            case ItemType.Soul: hasSoul = true; break;
            case ItemType.Egg: hasEgg = true; break;
            case ItemType.Gloomroot: hasGloomroot = true; break;
        }

        Debug.Log(itemType + " collected by " + collector.name + ". hasSoul: " + hasSoul);

        // 4. Destroy the object
        Destroy(gameObject);
    }
}
