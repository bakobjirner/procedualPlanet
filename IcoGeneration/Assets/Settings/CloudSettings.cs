using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CloudSettings : ScriptableObject
{
    [Range(2, 7)]
    public int detailLevel;
    public float skyHeight;
    public float cloudSize;
}