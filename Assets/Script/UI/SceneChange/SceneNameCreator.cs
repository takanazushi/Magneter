using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class SceneNameCreator
{
    //無効な文字列定義
    private static readonly string[] INVALUD_CHARS =
     {
         " ", "!", "\"", "#", "$",
        "%", "&", "\'", "(", ")",
        "-", "=", "^",  "~", "\\",
        "|", "[", "{",  "@", "`",
        "]", "}", ":",  "*", ";",
        "+", "/", "?",  ".", ">",
        ",", "<"
    };

    //コマンド名
    private const string ITEM_NAME = "Tools/Create/Scene Name Enum";

    //ファイルパス
    private const string PATH = "Assets/Script/SceneChange/SceneNameManager.cs";

    //ファイル名（拡張子アリ）
    private static readonly string FILENAME = Path.GetFileName(PATH);

    //ファイル名（拡張子なし）
    private static readonly string FILENAME_WITHOUT_EXTENSION = Path.GetFileNameWithoutExtension(PATH);


    [MenuItem(ITEM_NAME)]
    //メニュー項目の作成
    public static void Create()
    {
        if (!CanCreate())
        {
            return;
        }

        CreateScript();

        //ダイアログボックスで完了通知
        EditorUtility.DisplayDialog(FILENAME, "作成が完了しました", "OK");
    }

    //新しいスクリプトを作成
    //Scene名を管理するためのEnumが書かれたスクリプトを作成する
    public static void CreateScript()
    {
        var builder = new StringBuilder();

        
        builder.AppendLine("/// <summary>")
               .AppendLine("/// Scene名を管理するEnum")
               .AppendLine("/// </summary>")
               .AppendLine($"public enum {FILENAME_WITHOUT_EXTENSION}")
                .AppendLine("{");

        foreach (var n in EditorBuildSettings.scenes.Select(c => Path.GetFileNameWithoutExtension(c.path))
                                                   .Distinct()
                                                   .Select(c => new { var = RemoveInvalidChars(c), val = c }))
        {
            builder.AppendLine($"\t{n.var},");
        }

        builder.AppendLine("}");

        var directoryName = Path.GetDirectoryName(PATH);
        if (!Directory.Exists(directoryName))
        {
            Directory.CreateDirectory(directoryName);
        }

        File.WriteAllText(PATH, builder.ToString(), Encoding.UTF8);
        AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);

    }

    [MenuItem(ITEM_NAME, true)]
    //アプリが再生中でなく、コンパイルもされていないかどうか確認する
    public static bool CanCreate() => !EditorApplication.isPlaying
                                    && !Application.isPlaying
                                    && !EditorApplication.isCompiling;

    //無効な文字列を削除する
    public static string RemoveInvalidChars(string str)
    {
        Array.ForEach(INVALUD_CHARS, c => str = str.Replace(c, string.Empty));
        return str;
    }
}
