using DefoultNamespace;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemInstance> inventory = new List<ItemInstance>();

    public bool IsAdded(ItemInstance item, int count = 1)
    {
        Debug.Log(inventory.Count);
        if (inventory.Count < 15)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (item.item == inventory[i].item && item.item.type == Item.TYPEFOOD && inventory[i].count < 32)
                {
                    inventory[i].count += count;
                    return true;
                }
                if (item.item == inventory[i].item && item.item.type != Item.TYPEFOOD)
                {
                    inventory.Add(item);
                    return true;
                }
                if (inventory[i].item == null)
                {
                    inventory[i] = item;
                    return true;
                }
            }
            inventory.Add(item);
            return true;
        }
        return false;
    }


    public bool IsEnoughtItem(List<ItemInstance> required)
    {
        int itemsToComplete = required.Count;

        for (int i = 0; i < required.Count; i++)
        {
            foreach (ItemInstance item in inventory)
            {
                if (item.item.name == required[i].item.name)
                {
                    if (item.count >= required[i].count)
                    {
                        itemsToComplete--;
                    }
                }
            }
        }

        if (itemsToComplete == 0)
        {
            return true;
        }
        else return false;
    }

    public bool RemoveItem(string name, int count)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].item.name == name)
            {

                if (inventory[i].count < count)
                {
                    return false;
                }
                if (inventory[i].count == count)
                {
                    inventory.RemoveAt(i);
                    return true;
                }
                else
                {
                    inventory[i].count -= count;
                    return true;
                }
            }
        }
        return false;
    }


}
