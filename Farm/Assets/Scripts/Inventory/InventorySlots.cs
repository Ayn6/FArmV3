using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventorySlots : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory;
    private List<InventorySlot> slots = new List<InventorySlot>();
    private bool firstClick = false, secondClick = false;
    private int indexChange, indexChange2;

    private void Start()
    {
        Restart();
    }

    public void Restart()
    {
        slots = GetComponentsInChildren<InventorySlot>().ToList();

        for (int i =0; i < slots.Count; i++)
        {
            var slot = slots[i];

            if(i < playerInventory.inventory.Count)
            {
                slot.FillSlot(playerInventory.inventory[i]);
            }
            else
            {
                slot.FillSlot(null);
            }
        }

    }

    public void GetIndex(GameObject obj)
    {

        int index = obj.transform.GetSiblingIndex();

        if(index > playerInventory.inventory.Count - 1)
        {
            firstClick = secondClick = false;
            return;
        }
        if (firstClick)
        {
            indexChange2 = index;
            secondClick = true;
        }
        else
        {
            indexChange = index;
            firstClick = true;
        }
        Change(indexChange, indexChange2);
    }

    private void Change(int index1, int index2)
    {
        if (firstClick && secondClick && index1 != index2)
        {
            var slot = playerInventory.inventory[index1];

            playerInventory.inventory[index1] = playerInventory.inventory[index2];
            playerInventory.inventory[index2] = slot;
            firstClick = secondClick = false;
            Restart();

        }
        else
        {
            return;
        }
    }

}
