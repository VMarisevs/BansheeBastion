using UnityEngine;
using System.Collections;

public static class MapCalc {

    public static int GetMinCost(Vector2 positionA, Vector2 positionB)
    {
        return Mathf.Abs((int)(positionA.x - positionB.x)) + Mathf.Abs((int)(positionA.y - positionB.y));
    }
}
