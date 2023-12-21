using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//マグネットマネージャー用データ型定義
[System.Serializable]
public class MagnetUpdateData
{
    /// <summary>
    /// マグネットスクリプト
    /// </summary>
    public Magnet gbMagnet;

    /// <summary>
    /// Rigidbody2D
    /// </summary>
    public Rigidbody2D gbRid;

    /// <summary>
    /// タグ
    /// </summary>
    public string gbtag;

}
