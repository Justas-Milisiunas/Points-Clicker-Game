using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public Vector2 Position { get; private set; }
    public int OrderNumber { get; private set; }

    public Point(Vector2 position, int orderNumber)
    {
        Position = position;
        OrderNumber = orderNumber;
    }
}
