using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools
{
    public static float AddAngles(float a, float b)
    {
        return (a + b) % 360f;
    }

    public static float SubtractAngles(float a, float b)
    {
        float result = a - b;
        while (result < 0)
        {
            result += 360;
        }
        return result % 360;
    }
}
