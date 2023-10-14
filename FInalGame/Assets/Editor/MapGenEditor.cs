using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGenEditor : Editor {
    public override void OnInspectorGUI() {
        MapGenerator mapGen = (MapGenerator)target;

        // Draw the default inspector fields
        DrawDefaultInspector();

        // Check if the "Auto Update" option is enabled
        if (mapGen.autoUpdate) {
            // If it is, generate the map automatically whenever changes are made
            if (GUI.changed) {
                mapGen.GenerateMap();
            }
        }

        // Add a "Generate" button for manual map generation
        if (GUILayout.Button("Generate")) {
            mapGen.GenerateMap();
        }
    }
}
