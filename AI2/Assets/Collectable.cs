using UnityEngine;

// 1. Keep only ONE enum list. This is what shows up in your Inspector dropdown.
public enum ItemType { Soul, Egg, Gloomroot, Biscuit, Feed, LiquidFire, Obol }

public class Collectable : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    // Static variables to track your inventory globally
    public static bool hasSoul = false;
    public static bool hasGloomroot = false;
    public static bool hasEgg = false;
    public static bool hasBiscuit = false;
    public static bool hasFeed = true; // Starts true as requested!
    public static bool hasLiquidFire = false;
    public static bool hasObol = false;
    public static bool itemsAreStamped = false; // New checkbox for the Appraisal

    // This creates the dropdown menu in Unity's Inspector
    public ItemType itemType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            HandleCollection();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            HandleCollection();
        }
    }

    private void HandleCollection()
    {
        PlayerInventory inv = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerInventory>();

        // Automatically detect item type based on the object's Tag
        if (gameObject.CompareTag("Soul")) itemType = ItemType.Soul;
        if (gameObject.CompareTag("Egg")) itemType = ItemType.Egg;
        if (gameObject.CompareTag("Gloomroot")) itemType = ItemType.Gloomroot;

        if (inv != null)
        {
            inv.UpdateCount(itemType, 1);
        }

        // Update the checkboxes (static bools)
        switch (itemType)
        {
            case ItemType.Soul: hasSoul = true; break;
            case ItemType.Egg: hasEgg = true; break;
            case ItemType.Gloomroot: hasGloomroot = true; break;
        }

        Debug.Log(gameObject.tag + " collected and tagged correctly!");
        Destroy(gameObject);
       

        Debug.Log(itemType + " collected! Logic updated.");

        // 3. Play sound or VFX here if you have them!

        // Destroy the object so it disappears from the world
        Destroy(gameObject);
    }
}
