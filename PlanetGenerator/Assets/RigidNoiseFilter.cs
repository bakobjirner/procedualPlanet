using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilter : INoiseFilter
{
    NoiseSettings.RigidNoiseSettings noiseSettings;
    Noise noise = new Noise();

    public RigidNoiseFilter(NoiseSettings.RigidNoiseSettings noiseSettings)
    {
        this.noiseSettings = noiseSettings;
    }
    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = noiseSettings.baseRougjness;
        float amplitude = 1;
        float weight = 1;

        for (int i = 0; i < noiseSettings.layers; i++)
        {
            float v = 1-Mathf.Abs(noise.Evaluate(point * frequency + noiseSettings.center));
            v *= v;
            v *= weight;
            weight = Mathf.Clamp01(v*noiseSettings.weightMultiplier);

            noiseValue += v * amplitude;
            frequency *= noiseSettings.roughness;
            amplitude *= noiseSettings.persistance;
        }
        noiseValue = Mathf.Max(0, noiseValue - noiseSettings.minValue);
        return noiseValue * noiseSettings.strength;
    }
}
