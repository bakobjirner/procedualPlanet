using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    public SimpleNoiseSettings simpleNoiseSettings;
    public RigidNoiseSettings rigidNoiseSettings;
    public enum FilterType { Simple, Rigid };
    public FilterType filterType;

    [System.Serializable]
    public class SimpleNoiseSettings
    {
        public float strength = 1;
        public float roughness = 2;
        public float baseRougjness = 1;
        public Vector3 center;
        [Range(1, 8)]
        public int layers = 1;
        public float persistance = .5f;
        public float minValue;
    }

    [System.Serializable]
    public class RigidNoiseSettings: SimpleNoiseSettings
    {
        public float weightMultiplier = .8f;
    }
}
