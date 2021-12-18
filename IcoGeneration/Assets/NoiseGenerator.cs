using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGenerator
{

    private static Noise noise = new Noise();


    public static float ApplyNoise(Vector3 point, NoiseSettings noiseSettings)
    {
        float noiseValue = 0;
        
            float frequency = noiseSettings.baseRoughness;
            float amplitude = 1;
            for(int t = 0; t<noiseSettings.numberOfLayers; t++)
            {
                float v = noise.Evaluate(point * frequency + noiseSettings.centre);
            if (noiseSettings.allowNegative)
            {
                noiseValue += v * amplitude;
            }
            else
            {
                noiseValue += (v + 1) * .5f * amplitude;
            }
                frequency *= noiseSettings.roughness;
                amplitude *= noiseSettings.persistance;
            }
            return noiseValue *= noiseSettings.strength;
        
    }
}
