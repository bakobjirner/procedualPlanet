using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public Material planetMaterial;
    public Material oceanMaterial;
    GameObject planet;
    GameObject ocean;
    MeshRenderer planetMeshRenderer;
    MeshRenderer oceanMeshRenderer;
    [Range(2,7)]
    public int detailLevel;

    //noisesettings
    //continents
    public int noiseFrequencyContinents = 100;
    public float noiseStrengthContinents = 0.1f;
    //mountains
    public int noiseFrequencyMountains = 100;
    public float noiseStrengthMountains = 0.1f;
    //details
    public int noiseFrequencyDetails = 100;
    public float noiseStrengthDetails = 0.1f;


    public void OnValidate()
    {
        CreatePlanet();
        createOcean();
    }

   

    void CreatePlanet()
    {
        //set noise settings
        NoiseSettings[] noiseSettings = new NoiseSettings[3];
        noiseSettings[0] = new NoiseSettings(noiseFrequencyContinents, noiseStrengthContinents);
        noiseSettings[1] = new NoiseSettings(noiseFrequencyMountains, noiseStrengthMountains);
        noiseSettings[2] = new NoiseSettings(noiseFrequencyDetails, noiseStrengthDetails);

        if (planet == null)
        {
            planet = new GameObject("planetMesh");
        }
        planet.transform.SetParent(transform);
        if (planet.GetComponent<MeshFilter>() == null)
        {
            planet.AddComponent<MeshFilter>();
        }
        if (planet.GetComponent<MeshRenderer>() == null)
        {
            planetMeshRenderer = planet.AddComponent<MeshRenderer>();
        }
        else
        {
            planetMeshRenderer = planet.GetComponent<MeshRenderer>();
        }
        planetMeshRenderer.material = planetMaterial;
        SphereGenerator.CreateIco(planet, detailLevel, noiseSettings);
    }

    void createOcean()
    {
        NoiseSettings[] noiseSettings = new NoiseSettings[0];
        if(ocean == null)
        {
            ocean = new GameObject("oceanMesh");
        }
        ocean.transform.SetParent(transform);
        if (ocean.GetComponent<MeshFilter>() == null)
        {
            ocean.AddComponent<MeshFilter>();
        }
        if (ocean.GetComponent<MeshRenderer>() == null)
        {
            oceanMeshRenderer = ocean.AddComponent<MeshRenderer>();
        }
        else
        {
            oceanMeshRenderer = ocean.GetComponent<MeshRenderer>();
        }
        oceanMeshRenderer.material = oceanMaterial;
        SphereGenerator.CreateIco(ocean, detailLevel, noiseSettings);
    }
  
}
