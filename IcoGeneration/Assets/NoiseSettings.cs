using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    public float strength = 1;
    public float roughness = 1;
    public Vector3 centre;
    public float steepness = 2;
    public NoiseType type;

    public NoiseSettings( float strength, float roughness, Vector3 centre, float steepness, NoiseType type)
    {
        this.strength = strength;
        this.roughness = roughness;
        this.centre = centre;
        this.steepness = steepness;
        this.type = type;
    }
}
