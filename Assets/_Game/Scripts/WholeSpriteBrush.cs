using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeSpriteBrush : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Color color;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastSprites();
        }
    }

    private void RaycastSprite()
    {
        Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = Vector2.zero;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction);

        if (hit.collider == null)
            return;

        ColorSprite(hit.collider);

        Debug.Log(hit.collider.name);
    }
    private void RaycastSprites()
    {
        Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = Vector2.zero;

        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction);

        if (hits.Length <= 0)
            return;

        int highestOrderInLayer = -1000;
        int topIndex = -1;

        for (int i = 0; i < hits.Length; i++)
        {
            SpriteRenderer spriteRenderer = hits[i].collider.GetComponent<SpriteRenderer>();

            if (spriteRenderer == null)
                continue;

            int orderInLayer = spriteRenderer.sortingOrder;

            if (orderInLayer > highestOrderInLayer)
            {
                highestOrderInLayer = orderInLayer;
                topIndex = i;
            }
        }

        Collider2D highestCollider = hits[topIndex].collider;

        ColorSprite(highestCollider);
    }


    private void ColorSprite(Collider2D collider)
    {
        SpriteRenderer spriteRenderer = collider.GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
            return;

        spriteRenderer.color = color;
    }
}
