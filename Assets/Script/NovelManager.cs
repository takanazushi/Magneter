using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static NovelCommand;


public class NovelManager : MonoBehaviour
{

    /// <summary>
    /// ノベルテキストファイルのResourcesからの相対パス
    /// </summary>
    private const string TextFile = "Texts/Scenario";

    /// <summary>
    /// ノベルテキスト全文
    /// </summary>
    private string ScenarioTetx = "";

    /// <summary>
    /// 次のページに進める場合に表示するアイコン
    /// </summary>
    [SerializeField]
    private GameObject Next_Icon;

    /// <summary>
    /// キャラ立ち絵表示位置
    /// </summary>
    [SerializeField]
    private Image Chraimage_L, Chraimage_R;

    /// <summary>
    /// 使用する画像
    /// </summary>
    [SerializeField]
    private Sprite[] Chra_Images;

    /// <summary>
    /// テキスト送り速度
    /// </summary>
    [SerializeField,
        Header("テキストの表示速度"),
        Tooltip("この秒数ごとに表示")]
    private float TextSeed = 0.1f;

    /// <summary>
    /// シナリオ1ページ分
    /// </summary>
    private Queue<char> _charQ;

    /// <summary>
    /// シナリオ全ページ分
    /// </summary>
    private Queue<string> _pageQ;

    /// <summary>
    /// メインテキストを表示するText
    /// </summary>
    [SerializeField]
    private Text MainText;

    /// <summary>
    /// 名前テキストを表示するText
    /// </summary>
    [SerializeField]
    private Text NameText;

    private void Start()
    {
        //初期化
        Initializa();


    }

    private void Update()
    {
        //左クリック
        if (Input.GetMouseButtonDown(0))
        {
            LClick();
        }
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Initializa()
    {
        //ロード
        ScenarioTetx = LoadTextFile(TextFile);

        //ページ分割
        _pageQ = SeparateString(ScenarioTetx, MAIN_PAGE);

        //ページ開始
        ShowNextPage();
    }

    /// <summary>
    /// 左クリックされたとき
    /// </summary>
    private void LClick()
    {
        if (_charQ.Count > 0)
        {
            //全文表示
            OutputAllChar();
        }
        else
        {
            //次のページを表示
            if (!ShowNextPage())
            {
                //todo:仮置き
                //実行終了
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
    }

    /// <summary>
    /// シナリオデータを読み込む
    /// </summary>
    /// <param name="textFile">Resourcesからの相対パス</param>
    /// <returns>シナリオデータ全文</returns>
    private string LoadTextFile(string textFile)
    {
        //読み込み
        string textAsset = Resources.Load<TextAsset>(textFile).text;

        //改行ごとに分割
        string[] textAssets = textAsset.Split('\n');


        string Rtext = new("");
        foreach (string item in textAssets)
        {
            //コメント行を取り除く
            if (!item.TrimStart().StartsWith("//"))
            {
                //追加
                Rtext += item;
            }

        }

        //改行コードを取り除く
        return Rtext.Replace("\n", "").Replace("\r", "");

    }

    /// <summary>
    /// １ページ分表示
    /// </summary>
    /// <returns>false:次のページがない</returns>
    private bool ShowNextPage()
    {
        if (_pageQ.Count <= 0)
        {
            return false;
        }

        //次へアイコン非表示
        Next_Icon.SetActive(false);

        //1ページ表示
        ReadLine(_pageQ.Dequeue());

        return true;

    }

    /// <summary>
    /// 1ページ分表示開始
    /// </summary>
    /// <param name="line">表示するテキスト</param>
    private void ReadLine(string line)
    {
        //コマンド開始を検知
        if (line[0] == TEXT_COMMAND)
        {
            //コマンド実行
            CommandExe(line);

            //次のページへ
            ShowNextPage();
            return;
        }

        //名前欄とテキストを分割
        string[] ts = line.Split(MAIN_START);
        string name = ts[0];

        //メインテキストからメインテキスト終了コマンドを削除
        string main = ts[1].Remove(ts[1].LastIndexOf(MAIN_END));

        //リセット
        NameText.text = name;
        MainText.text = "";

        //文字分割
        _charQ = QString(main);

        //表示コルーチン開始
        StartCoroutine(ShowChar(TextSeed));

    }

    /// <summary>
    /// コマンド開始
    /// </summary>
    /// <param name="Comline"></param>
    private void CommandExe(string Comline)
    {
        //コマンド開始を削除
        Comline = Comline.Remove(0, 1);

        //コマンドごとに分割
        Queue<string> cmdQ = SeparateString(Comline, TEXT_COMMAND);

        foreach (string cmd in cmdQ)
        {
            //パラメータを分割
            string[] cmds = cmd.Split(COMMAND_PARAM);

            //コマンドの種類を確認
            if (cmds[0].Contains(COMMAND_CHARAIMG))
            {
                //キャラ立ち絵
                SetCharaImage(cmds);
                break;
            }

            if (cmds[0].Contains(COMMAND_SCENE))
            {
                //シーン変更
                Change_scene(cmds);
                break;
            }

        }


    }

    /// <summary>
    /// シーンの切り替え
    /// </summary>
    /// <param name="cmd"></param>
    private void Change_scene(string[] cmd)
    {
        cmd[1] = GetParameter(cmd[1], COMMAND_PARASTART);
        SceneManager.LoadScene(cmd[1]);
    }

    /// <summary>
    /// キャラクター立ち絵
    /// </summary>
    /// <param name="cmd">コマンド文</param>
    private void SetCharaImage(string[] cmd)
    {
        //画像指定
        if (cmd[0].Contains(COMMAND_SPRITE))
        {
            //オブジェクト指定
            cmd[1] = GetParameter(cmd[1], COMMAND_PARASTART);


            //表示画像指定
            cmd[2] = GetParameter(cmd[2], COMMAND_PARASTART);

            if (cmd[1] == "L")
            {
                for (int i = 0; i < Chra_Images.Length; i++)
                {
                    if (Chra_Images[i].name == cmd[2])
                    {
                        Chraimage_L.sprite = Chra_Images[i];
                        break;
                    }
                }
            }
            else if (cmd[1] == "R")
            {
                for (int i = 0; i < Chra_Images.Length; i++)
                {
                    if (Chra_Images[i].name == cmd[2])
                    {
                        Chraimage_R.sprite = Chra_Images[i];
                        break;
                    }
                }
            }

        }
        else if (cmd[0].Contains(COMMAND_COLOR))
        {
            //オブジェクト指定
            cmd[1] = GetParameter(cmd[1], COMMAND_PARASTART);


            //表示色指定
            cmd[2] = GetParameter(cmd[2], COMMAND_PARASTART);

            string[] colosRGBA = cmd[2].Split(',');

            //RGBA
            Color32 color = new Color32(byte.Parse(colosRGBA[0]), byte.Parse(colosRGBA[1]),
                byte.Parse(colosRGBA[2]), byte.Parse(colosRGBA[3]));

            if (cmd[1] == "L")
            {
                Chraimage_L.color = color;
            }
            else if (cmd[1] == "R")
            {
                Chraimage_R.color = color;
            }

        }

    }

    /// <summary>
    /// キューの文字をMainに表示
    /// </summary>
    /// <param name="wait">表示速度</param>
    /// <returns></returns>
    private IEnumerator ShowChar(float wait)
    {

        WaitForSeconds waittext = new WaitForSeconds(wait);

        //無くなるまで続ける
        while (OutputChar())
        {
            //表示
            MainText.text += _charQ.Dequeue();

            yield return waittext;

        }

        //無くなったら終了
        yield break;
    }


    /// <summary>
    /// 1ページ分まとめて表示
    /// </summary>
    private void OutputAllChar()
    {
        //コルーチンを止める
        StopCoroutine(ShowChar(TextSeed));

        //無くなるまで表示
        while (OutputChar())
        {
            MainText.text += _charQ.Dequeue();
        }

        //次へアイコン表示
        Next_Icon.SetActive(true);
    }

    /// <summary>
    /// 1ページ分キューが無くなったか？
    /// </summary>
    /// <returns>false：無くなった</returns>
    private bool OutputChar()
    {
        if (_charQ.Count <= 0)
        {
            //次へアイコン表示
            Next_Icon.SetActive(true);

            return false;
        }

        return true;
    }

    /// <summary>
    /// stringをcharキューに変換
    /// </summary>
    /// <param name="text">変換string</param>
    /// <returns>charキュー</returns>
    private Queue<char> QString(string text)
    {
        char[] chars = text.ToCharArray();
        Queue<char> charQ = new Queue<char>();

        foreach (char _char in chars)
        {
            charQ.Enqueue(_char);
        }

        return charQ;
    }

    /// <summary>
    /// paeraで囲まれている文字を取り出す
    /// </summary>
    /// <param name="cmd">コマンド</param>
    /// <param name="para">囲み文字指定</param>
    /// <returns></returns>
    private string GetParameter(string cmd, string para)
    {
        //オブジェクト指定
        cmd = cmd.Substring(cmd.IndexOf(para) + 1,
            cmd.LastIndexOf(para) - 1);

        return cmd;
    }

    /// <summary>
    /// paeraで囲まれている文字を取り出す
    /// </summary>
    /// <param name="cmd">コマンド</param>
    /// <param name="para">囲み文字指定</param>
    /// <returns></returns>
    private string GetParameter(string cmd, char para)
    {
        //オブジェクト指定
        cmd = cmd.Substring(cmd.IndexOf(para) + 1,
            cmd.LastIndexOf(para) - 1);

        return cmd;
    }

    /// <summary>
    /// stringを分割してキューに追加
    /// </summary>
    /// <param name="str">分割するテキスト</param>
    /// <param name="sep">分割文字</param>
    /// <returns>分割したキュー</returns>
    private Queue<string> SeparateString(string str, char sep)
    {
        string[] strs = str.Split(sep);

        Queue<string> queue = new Queue<string>();

        foreach (string _c in strs)
        {
            queue.Enqueue(_c);

        }

        return queue;
    }

}

