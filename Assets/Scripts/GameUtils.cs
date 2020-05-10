using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtils
{

    public static int getBestScore() {
        int retVal = 0;
#if UNITY_EDITOR
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, "score");
#else
        string path = System.IO.Path.Combine(Application.persistentDataPath, "score");
#endif
        if (System.IO.File.Exists(path)) {
            string fileString = System.IO.File.ReadAllText(path);

            int.TryParse(fileString, out retVal);
        }

        return retVal;
    }

    public static void saveBestScore(int score) {

#if UNITY_EDITOR
        if (!System.IO.Directory.Exists(Application.streamingAssetsPath)) {
            System.IO.Directory.CreateDirectory(Application.streamingAssetsPath);
        }

        string path = System.IO.Path.Combine(Application.streamingAssetsPath, "score");
#else
        string path = System.IO.Path.Combine(Application.persistentDataPath, "score");
#endif

        System.IO.File.WriteAllText(path, score.ToString());

    }


    public static Config readConfig() {
        Config retVal = null;
#if UNITY_EDITOR
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, "config");
#else
        string path = System.IO.Path.Combine(Application.persistentDataPath, "config");
#endif
        if (System.IO.File.Exists(path)) {
            string fileString = System.IO.File.ReadAllText(path);

            retVal = JsonUtility.FromJson<Config>(fileString);
        } else {
            retVal = new Config();
        }

        return retVal;
    }

    public static void saveConfig(Config config) {
#if UNITY_EDITOR
        if (!System.IO.Directory.Exists(Application.streamingAssetsPath)) {
            System.IO.Directory.CreateDirectory(Application.streamingAssetsPath);
        }

        string path = System.IO.Path.Combine(Application.streamingAssetsPath, "config");
#else
        string path = System.IO.Path.Combine(Application.persistentDataPath, "config");
#endif

        string json = JsonUtility.ToJson(config);

        System.IO.File.WriteAllText(path, json);

    }

}
