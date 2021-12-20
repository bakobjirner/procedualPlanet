using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour
{
    public GameObject planetGameObject;
    GravityAttractor planet;

    private void Awake()
    {
        planet = planetGameObject.GetComponent<GravityAttractor>();
        Rigidbody bodyRigidbody = gameObject.GetComponent<Rigidbody>();
        bodyRigidbody.useGravity = false;
        bodyRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void FixedUpdate()
    {
        planet.Attract(transform);
    }
}
