using DefoultNamespace;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Orders : MonoBehaviour
{
    private List<CreateOrder> slotsOrders = new List<CreateOrder>();
    public List<ItemInstance> orders = new List<ItemInstance>();
    private List<int> countItem = new List<int>();

    [SerializeField] private Sprite empty;
    [SerializeField] private Inventory playerInvetory;

    public Shop items;
    public PlayerInformation playerInformation;

    private void Start()
    {
        UpdateOrder();
    }

    public void UpdateOrder()
    {
        orders.Clear();
        countItem.Clear();

        slotsOrders = GetComponentsInChildren<CreateOrder>().ToList();

        foreach (var slot in slotsOrders)
        {
            Image slotImage = slot.transform.GetChild(0).GetComponent<Image>();
            TextMeshProUGUI slotText = slot.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            slotImage.sprite = empty;
            slotText.text = "";
        }

        Dictionary<ItemInstance, bool> usedItem = new Dictionary<ItemInstance, bool>();
        List<ItemInstance> avalibelItem = items.items.Where(item => item.item.openLvl <= PlayerInfo.level).ToList();
        int SlotCount = Random.Range(0, avalibelItem.Count + 1);
        for (int i = 0; i < SlotCount; i++)
        {
            var slot = slotsOrders[i];

            int index = Random.Range(0, avalibelItem.Count - 1);
            ItemInstance select = items.items[index];

            while (usedItem.ContainsKey(select))
            {
                index = Random.Range(0, 4);
                select = items.items[index];
            }

            usedItem.Add(select, true);
            int count = Random.Range(5, 16);

            if (PlayerInfo.level >= items.items[index].item.openLvl)
            {
                slot.Create(items.items[index], count);
                orders.Add(items.items[index]);
                countItem.Add(count);
            }
        }

        usedItem.Clear();

        if (orders.Count == 0 || countItem.Count == 0)
        {
            UpdateOrder();
        }

    }

    public void CompleteOrder()
    {
        slotsOrders = GetComponentsInChildren<CreateOrder>().ToList();
        bool enought = playerInvetory.IsEnoughtItem(orders);
        if (enought)
        {

            bool itemsGet = true;

            for (int i = 0; i < orders.Count; i++)
            {
                ItemInstance order = orders[i];
                int countToRemove = Mathf.Min(countItem[i], playerInvetory.inventory[i].count);

                if (countToRemove < countItem[i])
                {
                    itemsGet = false;
                    break;
                }

            }

            if (itemsGet)
            {
                for (int i = 0; i < orders.Count; i++)
                {
                    Debug.Log(orders[i].item.name);
                    bool action = playerInvetory.RemoveItem(orders[i].item.name, countItem[i]);

                    if (action)
                    {
                        playerInformation.AddExp(10);
                        PlayerInfo.money += 40;
                        UpdateOrder();

                    }
                    else
                    {
                        break;
                    }

                }
            }

        }
    }
}