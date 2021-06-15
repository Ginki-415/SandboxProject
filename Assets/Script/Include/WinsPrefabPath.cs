using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����prefab·��ע����
/// </summary>
public class WinsPrefabPath : Singleton<WinsPrefabPath>
{
    private static Dictionary<WinType, string> pathList = new Dictionary<WinType, string>();

    private void Awake()
    {
        pathList.Add(WinType.GAME_MAIN_WIN, "UI/UIPrefabs/GameMainWin");
        pathList.Add(WinType.WAITING_LOADING_WIN, "UI/UIPrefabs/WaitingLoadingWin");
    }

    public string GetPath(WinType winType) {
        string curWin = null;
        pathList.TryGetValue(winType, out curWin);
        if (curWin == null)
        {
            Debug.LogWarning("================δ����Ĵ�������===============");
        }
        return curWin;
    }
}
