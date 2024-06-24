using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Sprite front, left, right, back;

    [SerializeField] private float speed;
    private float horizontal, vertical;

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(horizontal, vertical, 0);
    }

    public void OnUp()
    {
        spriteRenderer.sprite = back;
        vertical = speed;
    }
    public void OnDown()
    {
        spriteRenderer.sprite = front;
        vertical = -speed;
    }
    public void OnLeft()
    {
        spriteRenderer.sprite = left;
        horizontal = -speed;
    }
    public void OnRight()
    {
        spriteRenderer.sprite = right;
        horizontal = speed;
    }
    public void Stop()
    {
        spriteRenderer.sprite = front;
        vertical = horizontal = 0;
    }

}
