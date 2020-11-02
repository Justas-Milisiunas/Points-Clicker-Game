using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public int ExtendAnimationSpeed = 15;

    public event Action ExtendingFinished;


    private bool extendAnimationActive = false;
    private float finalLength = 0f;

    private void Update()
    {
        // Animates length increase
        if (extendAnimationActive && GetCurrentLength() < finalLength)
        {
            IncreaseLength(ExtendAnimationSpeed * Time.deltaTime);
        }
        else if (extendAnimationActive)
        {
            finalLength = 0f;
            extendAnimationActive = false;

            ExtendingFinished();
        }
    }

    public void RotateAt(Transform target)
    {
        Vector2 direction = target.position - transform.position;

        var sign = direction.y >= 0 ? 1 : -1;
        var angle = Vector2.Angle(Vector2.right, direction) * sign;

        transform.Rotate(0f, 0f, angle - 90);
    }

    public void ExtendTo(Transform target)
    {
        finalLength = Vector2.Distance(transform.position, target.position) / transform.localScale.y;
        extendAnimationActive = true;
    }

    private void IncreaseLength(float length)
    {
        var currentLength = GetCurrentLength();
        spriteRenderer.size = new Vector2(0.45f, currentLength + length);
    }

    private float GetCurrentLength()
    {
        return spriteRenderer.size.y;
    }
}
