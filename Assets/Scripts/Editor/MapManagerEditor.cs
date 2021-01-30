using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapManager), false)]
public class MapManagerEditor : Editor
{
    MapManager m_target;
    private void OnEnable()
    {
        m_target = (MapManager)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(10);

        if (GUILayout.Button("Gen map"))
        {
            m_target.GenerateMap();
        }
    }
    
}
