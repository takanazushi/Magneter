
using UnityEngine;
using UnityEngine.UI;

public class Player_HP_UI : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField, Header("HP�A�C�R��")]
    private GameObject playerIcon;

    private int beforeHP;

    [SerializeField]
    private bool HPFrame;

    private void Start()
    {
        //HP�擾
        beforeHP = GameManager.instance.RestHP;
        CreateHPIcon();
    }

    private void Update()
    {
        ShowHPIcon();
    }

    //�A�C�R���쐬
    private void CreateHPIcon()
    {
        for (int i = 0; i < beforeHP; i++)
        {
            //playerIcon�ɓ����Ă�摜�𐶐�
            GameObject playerHPObj = Instantiate(playerIcon,transform);
            //�I�u�W�F�N�gHP��e�ɂ���
            playerHPObj.transform.parent = transform;
        }
    }

    //HP�\��
    private void ShowHPIcon()
    {
        //����HP�Ɠ����Ƃ��̓X���[
        if (beforeHP == GameManager.instance.GetHP()||HPFrame)
        {
            return;
        }

        //HP���ω�������
        //�q(playerIcon)�̃R���|�[�l���g�擾
        Image[] icons = transform.GetComponentsInChildren<Image>();

        for (int i = 0; i < icons.Length; i++)
        {
            //HP�̕\������؂�ւ���
            icons[i].gameObject.SetActive(i < GameManager.instance.HP);
        }

        //HP�擾
        beforeHP = GameManager.instance.GetHP();
    }
}
