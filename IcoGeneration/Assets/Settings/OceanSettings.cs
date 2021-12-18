using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class OceanSettings : ScriptableObject
{
    public float oceandepth = 100f;
    [Range(2, 7)]
    public int detailLevel;
}