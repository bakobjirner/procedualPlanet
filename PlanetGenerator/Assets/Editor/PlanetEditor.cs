using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
    Planet planet;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawSettingsEditor(planet.planetSettings, planet.OnPlanetSettingsUpdated);
    }

    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated)
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            Editor editor = CreateEditor(settings);
            editor.OnInspectorGUI();

            if (check.changed)
            {
                if(onSettingsUpdated != null)
                {
                    onSettingsUpdated();
                }
            }
        }
    }

    private void OnEnable()
    {
        planet = (Planet)target;
    }
}
