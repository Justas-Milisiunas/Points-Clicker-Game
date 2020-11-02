using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Level
{
    [SerializeField]
    private int[] level_data;
    private List<Point> points;

    public readonly int Order;
    public List<Point> Points
    {
        get
        {
            if (points != null)
            {
                return points;
            }

            points = FormatPointsData(level_data);
            return points;
        }
    }

    private List<Point> FormatPointsData(int[] pointsData)
    {
        var splittedPointsArray = pointsData
            .Select((point, i) => new { Index = i, Value = point })
            .GroupBy(p => p.Index / 2)
            .Select(p => p.Select(v => v.Value).ToArray())
            .ToList();

        var convertedPoints = new List<Point>();

        for (int i = 0; i < splittedPointsArray.Count; i++)
        {
            var point = splittedPointsArray[i];

            var newPoint = new Point(new Vector2(point[0], point[1]), i + 1);
            convertedPoints.Add(newPoint);
        }

        return convertedPoints;
    }
}
