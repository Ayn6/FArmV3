using DefoultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateOrder : MonoBehaviour
{
    [SerializeField] private Image imageItem;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Sprite empty;

    public void Create(ItemInstance item, int count)
    {
        if (PlayerInfo.level >= item.item.openLvl && item.item.type == Item.TYPEFOOD)
        {
            countText.text = count.ToString();
            imageItem.sprite = item.item.sprite;
        }
        else
        {
            return;
        }
    }
}
