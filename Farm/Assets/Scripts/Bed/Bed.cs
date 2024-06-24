using DefoultNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bed : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer spriteRendererItem;

    [SerializeField] private Sprite bed, selectBed, grasseCarrot;

    [SerializeField] private PlayerInformation playerInformation;

    [SerializeField] private GameObject player;

    private static int STEP_EMPTY = 0;
    private static int STEP_GROW = 1;
    private static int STEP_READY = 2;
    private static int STEP_PLOW = 3;
    private static int STEP_WEED = 4;

    private int step = 0;

    private bool readyForAction = false;
    private bool weed = false;
    private Coroutine grow;

    private ItemInstance growItem;
    [FormerlySerializedAs("Plant")] public Inventory playerInventory;

    private void FixedUpdate()
    {
        if (step != STEP_GROW)
        {
            if (Vector3.Distance(this.transform.position, player.transform.position) < 0.2f)
            {
                readyForAction = true;
                spriteRenderer.sprite = selectBed;
            }
            else
            {
                readyForAction = false;
                spriteRenderer.sprite = bed;
            }
        }
        else
        {
            spriteRenderer.sprite = bed;

            if(!weed)
            {
                StartCoroutine(Weed());
                weed = true;
            }
        }
    }

    private void OnMouseDown()
    {
        if((step == STEP_EMPTY || step == STEP_PLOW) && playerInventory.inventory.Count < 0)
        {
            return;
        }

        var active = playerInventory.inventory[0];

        if (readyForAction)
        {
            if(step == STEP_EMPTY && active.item.type == Item.TYPEFOOD)
            {
 
                if (active.count <= 0)
                {
                    playerInventory.inventory.RemoveAt(0);
                    return ;
                }

                spriteRendererItem.sprite = items[0].sprite;
                step = STEP_GROW;
                active.count -= 1;

                grow = StartCoroutine(Grow(playerInventory.inventory[0].item.time, active.item, playerInventory.inventory[0].item.count));

                if (active.count <= 0)
                {
                    playerInventory.inventory.RemoveAt(0);
                }
            }
            else if (step == STEP_WEED && weed)
            {
                
                spriteRendererItem.sprite = items[0].sprite;
                step = STEP_GROW;
                
            }
            else if(step == STEP_READY)
            {
                if (!weed)
                {
                    spriteRendererItem.sprite = items[2].sprite;
                    step = STEP_PLOW;
                }
                else
                {
                    if (growItem != null && !playerInventory.IsAdded(growItem, growItem.item.count))
                    {
                        Debug.Log("Не добавильсь в инвентарь");
                    }
                    else
                    {
                        playerInformation.AddExp(growItem.item.exp);
                        spriteRendererItem.sprite = items[2].sprite;
                        step = STEP_PLOW;
                    }
                }
               
            }
            else if(step == STEP_PLOW && active.item.type == Item.TYPEPLOW)
            {
                if (active.count <= 0)
                {
                    return;
                }

                spriteRendererItem.sprite = null;
                step = STEP_EMPTY;
                active.count -= 1;

                if (active.count <= 0)
                {
                    playerInventory.inventory.RemoveAt(0);
                }

            }
        }
    }

    private IEnumerator Grow(float time, Item grow, int count)
    {
        yield return new WaitForSeconds(time);

        if(grow.name == "Carrot")
        {
            spriteRendererItem.sprite = grasseCarrot;
        }
        else
        {
            spriteRendererItem.sprite = grow.sprite;
        }
        

        growItem = new ItemInstance
        {
            item = grow,
            count = count
        };

        step = STEP_READY;
    }

    private IEnumerator Weed()
    {
        growItem = playerInventory.inventory[0];
        while (step == STEP_GROW)
        {
            yield return new WaitForSeconds(20f);

            if(step == STEP_GROW && weed)
            {
                spriteRendererItem.sprite = items[3].sprite;
                step = STEP_WEED;
                yield return new WaitForSeconds(10f);

                if(step == STEP_WEED && weed)
                {
                    spriteRendererItem.sprite = growItem.item.badSprite;
                    step = STEP_READY;
                    weed = false;
                    StopCoroutine(grow);
                }
            }

        }
    }

}
