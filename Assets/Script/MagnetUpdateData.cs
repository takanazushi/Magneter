using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//マグネットマネージャー用データ型定義
[System.Serializable]
public class MagnetUpdateData
{
    public Magnet gbMagnet;
    public Rigidbody2D gbRid;

    //タイルマップ用：タイルマップに対応可能かわかんない
    public Tilemap Til;
}
