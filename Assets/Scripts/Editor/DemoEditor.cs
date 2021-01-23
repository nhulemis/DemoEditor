using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Scripts;

public class DemoEditor : EditorWindow
{
    int width = 0, prevWidth = 0;
    int height = 0, prevHeight = 0;
    int selectedIndex = 0;

    Level currenLevel;

    Vector2 scrollPos;

    [MenuItem("Window/Demo Editor/Editor")]
    static void Init()
    {
        var window = GetWindow(typeof(DemoEditor));
        window.titleContent = new GUIContent("Demo Editor");
    }

    private void OnEnable()
    {
        currenLevel = new Level();
    }

    private void OnGUI()
    {
        scrollPos = GUILayout.BeginScrollView(scrollPos);
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("SAVE", GUILayout.Width(50)))
            {
                SaveToJson();
            }
            if (GUILayout.Button("OPEN", GUILayout.Width(50)))
            {
                OpenJson();
            }
            GUILayout.EndHorizontal();
        } // Save button

        DrawMatrix();


        GUILayout.EndScrollView();
    }

    private void SaveToJson()
    {
        
    }

    private void OpenJson()
    {
        
    }

    void DrawMatrix() 
    {
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Width", GUILayout.Width(EditorGUIUtility.labelWidth));
        width = EditorGUILayout.IntField(width, GUILayout.Width(50));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Height", GUILayout.Width(EditorGUIUtility.labelWidth));
        height = EditorGUILayout.IntField(height, GUILayout.Width(50));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Type of Riser", GUILayout.Width(EditorGUIUtility.labelWidth));
        selectedIndex = GUILayout.SelectionGrid(selectedIndex, new[] { "None", "Riser" }, 2, GUILayout.Width(150));
        GUILayout.EndHorizontal();

        if (width != prevWidth || height != prevHeight)
        {
            prevHeight = height;
            prevWidth = width;

            currenLevel.Tiles = new List<Tile>();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    currenLevel.Tiles.Add(new Tile() { type = Type.None });
                }
            }
            //Debug.Log(currenLevel.Tiles.Count);

        }

        for (int i = 0; i < height; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < width; j++)
            {
                var index = width * i + j;
                try
                {
                    if (currenLevel.Tiles.Count >= index && currenLevel.Tiles[index] != null)
                    {
                        DrawButton(i, j, index);
                    }
                }
                catch 
                {
                    //Debug.Log($"index {index} - {i}-{j}");
                }
            }
            GUILayout.EndHorizontal();
        }
    }

    void DrawButton(int i, int j , int index)
    {
        if (currenLevel.Tiles[index].type != Type.None)
        {
            if (GUILayout.Button($"{i}-{j}", GUILayout.Width(40), GUILayout.Height(40)))
            {
                currenLevel.Tiles[index].type = (Type)selectedIndex;
            }
        }
        else
        {
            if (GUILayout.Button("", GUILayout.Width(40), GUILayout.Height(40)))
            {
                currenLevel.Tiles[index].type = (Type)selectedIndex;
            }
        }
    }
}
