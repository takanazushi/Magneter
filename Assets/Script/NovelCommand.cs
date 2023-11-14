using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovelCommand
{

    /// <summary>
    /// コマンド：メインテキストの始まり
    /// </summary>
    public const string MAIN_START = "@「";

    /// <summary>
    /// コマンド：メインテキストの終わり
    /// </summary>
    public const string MAIN_END = "」@";

    /// <summary>
    /// コマンド：ページ分割
    /// </summary>
    public const char MAIN_PAGE = '&';

    /// <summary>
    /// コマンド：コマンド開始
    /// </summary>
    public const char TEXT_COMMAND = '!';

    /// <summary>
    /// コマンド：パラメータ指定
    /// </summary>
    public const char COMMAND_PARAM = '=';

    /// <summary>
    /// コマンド：パラメータ区切り
    /// </summary>
    public const char COMMAND_PARASTART = '"';

    /// <summary>
    /// コマンド：キャラ立ち絵操作
    /// </summary>
    public const string COMMAND_CHARAIMG = "charaimg";

    /// <summary>
    /// コマンド：シーン切り替え
    /// </summary>
    public const string COMMAND_SCENE= "scene"; 

    /// <summary>
    /// コマンド：画像指定
    /// </summary>
    public const string COMMAND_SPRITE = "_sprite";

    /// <summary>
    /// コマンド：色操作
    /// </summary>
    public const string COMMAND_COLOR = "_color";


}
