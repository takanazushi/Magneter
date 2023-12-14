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
    //�����ȕ������`
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

    //�R�}���h��
    private const string ITEM_NAME = "Tools/Create/Scene Name Enum";

    //�t�@�C���p�X
    private const string PATH = "Assets/Script/SceneChange/SceneNameManager.cs";

    //�t�@�C�����i�g���q�A���j
    private static readonly string FILENAME = Path.GetFileName(PATH);

    //�t�@�C�����i�g���q�Ȃ��j
    private static readonly string FILENAME_WITHOUT_EXTENSION = Path.GetFileNameWithoutExtension(PATH);


    [MenuItem(ITEM_NAME)]
    //���j���[���ڂ̍쐬
    public static void Create()
    {
        if (!CanCreate())
        {
            return;
        }

        CreateScript();

        //�_�C�A���O�{�b�N�X�Ŋ����ʒm
        EditorUtility.DisplayDialog(FILENAME, "�쐬���������܂���", "OK");
    }

    //�V�����X�N���v�g���쐬
    //Scene�����Ǘ����邽�߂�Enum�������ꂽ�X�N���v�g���쐬����
    public static void CreateScript()
    {
        var builder = new StringBuilder();

        
        builder.AppendLine("/// <summary>")
               .AppendLine("/// Scene�����Ǘ�����Enum")
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
    //�A�v�����Đ����łȂ��A�R���p�C��������Ă��Ȃ����ǂ����m�F����
    public static bool CanCreate() => !EditorApplication.isPlaying
                                    && !Application.isPlaying
                                    && !EditorApplication.isCompiling;

    //�����ȕ�������폜����
    public static string RemoveInvalidChars(string str)
    {
        Array.ForEach(INVALUD_CHARS, c => str = str.Replace(c, string.Empty));
        return str;
    }
}
