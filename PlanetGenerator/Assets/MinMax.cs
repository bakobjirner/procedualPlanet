using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMax
{
    public float min;
    public float max;

    public MinMax()
    {
        min = float.MaxValue;
        max = float.MinValue;
    }

    public void AddValue(float v)
    {
        if (v > max)
        {
            max = v;
        }
        if (v < min)
        {
            min = v;
        }
    }
}
