using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    //タイルに対してつけてもPlayer側のOnCollisionStay2Dあたりでうまくいかないことが確認されてるよ
    [SerializeField, Header("接触時ダメージ")]
    private int Hit_Damage;

    public int hit_damage => Hit_Damage;
}
