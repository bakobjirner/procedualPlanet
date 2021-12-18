using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlanetSettings : ScriptableObject
{
    public float radius = 100;
    public NoiseLayer[] noiseLayers;
    [Range(2, 7)]
    public int detailLevel;

    public Gradient gradient;
    public Material planetMaterial;
}
