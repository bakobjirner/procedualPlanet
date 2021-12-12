using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilter : INoiseFilter
{

    NoiseSettings.SimpleNoiseSettings noiseSettings;
    Noise noise = new Noise();

    public SimpleNoiseFilter(NoiseSettings.SimpleNoiseSettings noiseSettings)
    {
        this.noiseSettings = noiseSettings;
    }
    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = noiseSettings.baseRougjness;
        float amplitude = 1;

        for(int i = 0; i< noiseSettings.layers; i++)
        {
            float v = noise.Evaluate(point * frequency + noiseSettings.center);
            noiseValue += (v + 1) * .5f * amplitude;
            frequency *= noiseSettings.roughness;
            amplitude *= noiseSettings.persistance;
        }
        noiseValue = Mathf.Max(0, noiseValue - noiseSettings.minValue);
        return noiseValue*noiseSettings.strength;
    }
}
