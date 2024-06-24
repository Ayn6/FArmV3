using UnityEngine;

public class Actions : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer; 
    [SerializeField] private Sprite sprite, spriteSelect;
    [SerializeField] private GameObject btn;
    private void OnTriggerEnter2D(Collider2D other)
    {
        btn.SetActive(true);
        spriteRenderer.sprite = spriteSelect;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        btn.SetActive(false);
        spriteRenderer.sprite = sprite;
    }
}
