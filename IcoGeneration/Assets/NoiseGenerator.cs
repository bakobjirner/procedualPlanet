using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGenerator
{

    private static Noise noise = new Noise();


    public static Vector3 ApplyNoise(Vector3 point, NoiseSettings[] noiseSettings)
    {
        float noiseValue = 0;
        for(int i = 0; i< noiseSettings.Length; i++)
        {
            float frequency = noiseSettings[i].frequency;
            float v = (noise.Evaluate(point * frequency) + 1)*0.5f; //translate values between -1 and 1 to values between 0 and 1
            if (noiseValue >= noiseSettings[i].threshold)
            {
                noiseValue += v * noiseSettings[i].strength*(noiseValue- noiseSettings[i].threshold) * (noiseValue - noiseSettings[i].threshold);
            }
        }
        return point + point * noiseValue;
    }
}
