using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����Prefab·��ע����
/// </summary>
public class CubePath : Singleton<CubePath>
{
    private static Dictionary<CubeType, string> pathList = new Dictionary<CubeType, string>();

    private void Awake()
    {
        pathList.Add(CubeType.Grass , "Prefabs/Grass");
        pathList.Add(CubeType.Stone , "Prefabs/Stone");
    }

    public string GetPath(CubeType cubeType)
    {
        string curCube = null;
        pathList.TryGetValue(cubeType, out curCube);
        if (curCube == null)
        {
            Debug.LogWarning("================ δ����ķ������� ===============");
        }
        return curCube;
    }
}
