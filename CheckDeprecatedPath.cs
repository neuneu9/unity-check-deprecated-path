using UnityEditor;
using UnityEngine;

/// <summary>
/// ASCII以外の文字を含むパスを見つける
/// </summary>
public class CheckDeprecatedPath
{
    /// <summary>
    /// チェック実行
    /// </summary>
    [MenuItem("Custom/Check Deprecated Path")]
    private static void Check()
    {
        // プロジェクトフォルダ
        bool hasFound = WarnIfPathIsDeprecated(Application.dataPath);

        // 全アセット
        var guids = AssetDatabase.FindAssets("t:Object", new string[] { "Assets" });
        foreach (var guid in guids)
        {
            if (WarnIfPathIsDeprecated(AssetDatabase.GUIDToAssetPath(guid)))
            {
                hasFound = true;
            }
        }

        if (!hasFound)
        {
            Debug.Log("CheckDeprecatedPath: There is no deprecated path.");
        }
    }

    /// <summary>
    /// ASCII以外の文字を含むパスであれば警告ログ
    /// </summary>
    /// <param name="path"></param>
    private static bool WarnIfPathIsDeprecated(string path)
    {
        foreach (var c in path)
        {
            if (!IsAscii(c))
            {
                Debug.LogWarning("CheckDeprecatedPath: [" + c + "] is contained in " + path);
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// ASCIIか判定
    /// https://stackoverflow.com/questions/18596245/in-c-how-can-i-detect-if-a-character-is-a-non-ascii-character
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    private static bool IsAscii(char c)
    {
        return (c <= sbyte.MaxValue);
    }
}
