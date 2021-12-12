using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlanetSettings : ScriptableObject
{

    public float radius = 1;
    public NoiseLayer[] noiseLayers;
    public Material planetMaterial;
    public Gradient gradient;
    public Material oceanMaterial;
    public float oceanDepth;

    [System.Serializable]
    public class NoiseLayer
    {
        public NoiseSettings noiseSettings;
        public bool enabled = true;
        public bool useFirstLayerAsMask = true;
    }
}
