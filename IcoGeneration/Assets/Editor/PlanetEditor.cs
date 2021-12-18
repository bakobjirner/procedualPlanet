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

        DrawSettingsEditor(planet.planetSettings, ref planet.planetSettingsFoldOut, "planet");
        DrawSettingsEditor(planet.oceanSettings, ref planet.oceanSettingsFoldOut,"ocean");
    }

    void DrawSettingsEditor(Object settings, ref bool foldout, string scope)
    {
        foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            
            if (foldout)
            {
                Editor editor = CreateEditor(settings);
                editor.OnInspectorGUI();

                if (check.changed)
                {
                    switch (scope)
                    {
                        case "planet": planet.onPlanetSettingsUpdated();
                            break;
                        case "ocean": planet.onOceanSettingsUpdated();
                            break;
                        default: planet.onPlanetSettingsUpdated();
                            break;
                    }
                   
                }
            }
        }
    }

    private void OnEnable()
    {
        planet = (Planet)target;
    }
}
