using Assets.Scripts;
using FullSerializer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    GameObject roadPrefab;

    [SerializeField]
    GameObject tilePrefab;

    [SerializeField]
    int level;

    Level currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

#if UNITY_EDITOR
    public void GenerateMap()
    {
        RemoveOldMap();

        currentLevel = LoadJsonFile<Level>("Levels/" + level.ToString());

        Vector3 pos = transform.position;

        Vector3 size = roadPrefab.GetComponent<BoxCollider>().size;

        for (int i = 0; i < currentLevel.Height; i++)
        {
            for (int j = 0; j < currentLevel.Width; j++)
            {
                var index = currentLevel.Width * i + j;
                if (currentLevel.Tiles[index].type != Type.None)
                {
                    GameObject go = Instantiate(roadPrefab, transform);
                    go.transform.position = pos;
                }
                else
                {
                    GameObject go = Instantiate(tilePrefab, transform);
                    go.transform.position = pos;
                }
                pos.x += size.x;
            }
            pos.x = transform.position.x;
            pos.z -= size.z;
        }
    }

    private void RemoveOldMap()
    {
        var childs = transform.childCount;

        for (int i = childs - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
#endif

    public T LoadJsonFile<T>(string path) where T : class
    {
        var serializer = new fsSerializer();
        var textAsset = Resources.Load<TextAsset>(path);
        Assert.IsNotNull(textAsset);
        var data = fsJsonParser.Parse(textAsset.text);
        object deserialized = null;
        serializer.TryDeserialize(data, typeof(T), ref deserialized).AssertSuccessWithoutWarnings();
        return deserialized as T;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
