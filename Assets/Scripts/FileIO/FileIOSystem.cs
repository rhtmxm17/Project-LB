using System;
using System.IO;
using UnityEngine;
using UnityEngine.Android;

public static class FileIOSystem
{

    //todo : 파일 저장경로 상의하고 결정하기.

    /// <summary>
    /// PlayerData인스턴스를 json파일 형식으로 저장
    /// </summary>
    /// <param name="data">저장할 PlayerData타입 인스턴스</param>
    public static void SavePlayerData(PlayerData data)
    {

        string jData = JsonUtility.ToJson(data, true);
        //todo : 덮어씁니까? 경고 필요한가?
        File.WriteAllText($"{Application.persistentDataPath}/playerData.json", jData);

    }

    /// <summary>
    /// 플레이어 세이브데이터를 읽어옴 [없을경우 null 반환]
    /// </summary>
    /// <returns>PlayerData 인스턴스 반환</returns>
    public static PlayerData LoadPlayerData()
    {
        string path = $"{Application.persistentDataPath}/playerData.json";
        PlayerData data = null;

        try
        {
            if (File.Exists(path))
            {
                string jData = File.ReadAllText(path);
                data = JsonUtility.FromJson<PlayerData>(jData);
                Debug.Log(path);
            }
            else
            {
                //todo : 세이브 파일이 존재하지 않는 경우 처리.
                Debug.LogWarning("세이브 파일이 존재하지 않습니다.");
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        return data;

    }

}
