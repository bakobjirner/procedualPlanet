using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public PlanetSettings planetSettings;
    public OceanSettings oceanSettings;
    
    public Material oceanMaterial;
    GameObject planet;
    GameObject ocean;
    MeshRenderer planetMeshRenderer;
    MeshRenderer oceanMeshRenderer;
    

    [HideInInspector]
    public bool planetSettingsFoldOut;
    [HideInInspector]
    public bool oceanSettingsFoldOut;
    ColorGenerator colorGenerator;





    public void OnValidate()
    {
        CreatePlanet();
        createOcean();
    }

   

    void CreatePlanet()
    {
        colorGenerator = new ColorGenerator(planetSettings);
     
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
        planetMeshRenderer.material = planetSettings.planetMaterial;
        PlanetGenerator.CreateIco(planet, planetSettings);
        colorGenerator.UpdateElevation(PlanetGenerator.elevationMinMax);
        colorGenerator.UpdateColors();
    }

    void createOcean()
    {
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
        OceanGenerator.CreateIco(ocean,oceanSettings);
        
    }

    public void onPlanetSettingsUpdated()
    {
        CreatePlanet();
    }
    public void onOceanSettingsUpdated()
    {
        createOcean();
    }
}
