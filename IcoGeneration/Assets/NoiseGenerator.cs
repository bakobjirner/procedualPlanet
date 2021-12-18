using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGenerator
{

    private static Noise noise = new Noise();


    public static float ApplyNoise(Vector3 point, NoiseSettings noiseSettings)
    {
        float noiseValue;
        float frequency = noiseSettings.roughness;
        noiseValue = noise.Evaluate(point * frequency + noiseSettings.centre);

        switch (noiseSettings.type)
        {
            case NoiseType.CONTINENTAL: noiseValue = noiseValue / (1 + Mathf.Abs(noiseSettings.steepness * noiseValue));
                break;
            case NoiseType.MOUNTAIN:
                noiseValue = Mathf.Pow((noiseValue+1)*0.5f, noiseSettings.steepness);
                break;
            case NoiseType.LINEAR:
                noiseValue = (noiseValue + 1) * 0.5f;
                break;
            default:
                noiseValue = Mathf.Pow(noiseValue,3);
                break;
        }

        return noiseValue * noiseSettings.strength;
    }
}
