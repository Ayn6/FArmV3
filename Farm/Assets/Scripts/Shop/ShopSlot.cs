using DefoultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] private Image imageItem;
    [SerializeField] private TextMeshProUGUI count;
    [SerializeField] private Sprite empty;

    public void FillSlot(ItemInstance item)
    {
        if (item == null)
        {
            count.text = "";
            imageItem.sprite = empty;
        }
        else
        {
            count.text = item.item.price.ToString();
            imageItem.sprite = item.item.sprite;
        }

    }
}
