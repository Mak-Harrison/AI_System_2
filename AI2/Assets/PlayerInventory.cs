using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory Counts")]
    public int soulCount;
    public int eggCount;
    public int gloomrootCount;
    public int biscuitCount;
    public int liquidfireCount;
    public int feedCount = 1; // Starting with 1 feed as discussed

    public void UpdateCount(ItemType type, int amount)
    {
        switch (type)
        {
            case ItemType.Soul:
                soulCount += amount;
                break;
            case ItemType.Egg:
                eggCount += amount;
                break;
            case ItemType.Gloomroot:
                gloomrootCount += amount;
                break;
            case ItemType.Biscuit:
                biscuitCount += amount;
                break;
            case ItemType.LiquidFire:
                liquidfireCount += amount;
                break;
            case ItemType.Feed:
                feedCount += amount;
                break;
        }

        Debug.Log($"Inventory Updated: {type} is now {amount}");
    }

    public bool TryUseItem(ItemType type)
    {
        // This checks if you have the item, and if so, subtracts one and returns true
        switch (type)
        {
            case ItemType.Soul:
                if (soulCount > 0) { soulCount--; return true; }
                break;
            case ItemType.Egg:
                if (eggCount > 0) { eggCount--; return true; }
                break;
            case ItemType.Gloomroot:
                if (gloomrootCount > 0) { gloomrootCount--; return true; }
                break;
            case ItemType.Biscuit:
                if (biscuitCount > 0) { biscuitCount--; return true; }
                break;
            case ItemType.LiquidFire:
                if (liquidfireCount > 0) { liquidfireCount--; return true; }
                break;
            case ItemType.Feed:
                if (feedCount > 0) { feedCount--; return true; }
                break;
        }

        Debug.Log($"Not enough {type} in inventory!");
        return false;
    }
}

