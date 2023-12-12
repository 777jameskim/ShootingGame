using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class GameParams
{
    public static float boundaryX = 3.5f;
    public static float boundaryY = 5.5f;

    public static bool OutOfBounds(Transform t)
    {
        if (t.position.x > boundaryX
            || t.position.x < 0 - boundaryX
            || t.position.y > boundaryY
            || t.position.y < 0 - boundaryY)
            return true;
        return false;
    }
}