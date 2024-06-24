using DefoultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image imageItem;
    [SerializeField] private TextMeshProUGUI count;
    [SerializeField] private Sprite empty;

    public void FillSlot(ItemInstance item)
    {
        if(item == null || (item != null && item.count < 0))
        {
            count.text = "";
            imageItem.sprite = empty;
        }
        else if (item != null && (item.item.type == Item.TYPEFOOD))
        {
            count.text = "x" + item.count.ToString();
            imageItem.sprite = item.item.sprite;
        }
        else if (item != null && (item.item.type != Item.TYPEFOOD))
        {
            count.text = item.count.ToString();
            imageItem.sprite = item.item.sprite;
        }
    }
}
