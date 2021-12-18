using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{

    [Range(1, 8)]
    public int numberOfLayers = 1;
    public float strength = 1;
    public float roughness = 1;
    public float baseRoughness = 1;
    public float persistance = 1;
    public Vector3 centre;
    public bool allowNegative = false;

    public NoiseSettings(int numberOfLayers, float strength, float roughness, float baseRoughness, float persistance, Vector3 centre, bool allowNegative)
    {
        this.numberOfLayers = numberOfLayers;
        this.strength = strength;
        this.roughness = roughness;
        this.baseRoughness = baseRoughness;
        this.persistance = persistance;
        this.centre = centre;
        this.allowNegative = allowNegative;
    }
}
