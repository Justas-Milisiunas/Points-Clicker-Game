using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeCreator : MonoBehaviour
{
    public GameObject ropePrefab;

    public RopeController CreateRope(Transform origin, Transform target)
    {
        var newRopePrefab = ropePrefab.GetComponent<RopeController>();

        var createdRope = Instantiate(newRopePrefab);

        createdRope.transform.position = origin.position;

        createdRope.RotateAt(target);
        createdRope.ExtendTo(target);

        return createdRope;
    }
}
