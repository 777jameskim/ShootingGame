using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class GameParams
{
    public static float boundaryX = 2.8f;
    public static float boundaryY = 5f;

    public static float playerX = 2.3f;
    public static float playerY = 4.5f;

    public static Vector3 startPosition = new Vector3(0f, -4f, 0);
    public static int playerHP = 1;
    public static int lives = 3;

    public static int scoreA = 10;
    public static int scoreCoin = 1;

    public static float hitBlink = 0.05f;
    public static float invincibleBlink = 0.05f;
    public static float invincibleTime = 2f;

    public static bool OutOfBounds(Transform t, float padding = 0)
    {
        if (t.position.x > boundaryX + padding
            || t.position.x < 0 - boundaryX - padding
            || t.position.y > boundaryY + padding
            || t.position.y < 0 - boundaryY - padding)
            return true;
        return false;
    }

    public static bool OutOfBounds(Transform t, bool protectEntry, float padding = 0)
    {
        if (t.position.x > boundaryX + padding
            || t.position.x < 0 - boundaryX - padding
            || t.position.y < 0 - boundaryY - padding)
            return true;
        if (!protectEntry)
        {
            if (t.position.y > boundaryY + padding)
                return true;
        }
        return false;
    }
}
