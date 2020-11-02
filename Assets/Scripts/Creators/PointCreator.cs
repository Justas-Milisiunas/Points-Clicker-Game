using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCreator : MonoBehaviour
{
    public GameObject pointPrefab;

    public PointController CreatePoint(Point point)
    {
        var createdPoint = Instantiate(pointPrefab.GetComponent<PointController>());
        createdPoint.Order = point.OrderNumber;

        float convertedX = (float)(point.Position.x * 0.018 - 9) * 0.9f;
        float convertedY = (float)(-(point.Position.y * 0.01 - 5));

        createdPoint.transform.position = new Vector3(convertedX, convertedY, 0);
        return createdPoint;
    }
}
