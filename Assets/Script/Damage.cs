using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    //�^�C���ɑ΂��Ă��Ă�Player����OnCollisionStay2D������ł��܂������Ȃ����Ƃ��m�F����Ă��
    [SerializeField, Header("�ڐG���_���[�W")]
    private int Hit_Damage;

    public int hit_damage => Hit_Damage;
}
