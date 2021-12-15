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
            float v = noise.Evaluate(point * frequency);
            noiseValue += v * noiseSettings[i].strength;
        }
        return point + point * noiseValue;
    }
}
