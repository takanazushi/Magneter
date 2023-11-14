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
    /// �m�x���e�L�X�g�t�@�C����Resources����̑��΃p�X
    /// </summary>
    private const string TextFile = "Texts/Scenario";

    /// <summary>
    /// �m�x���e�L�X�g�S��
    /// </summary>
    private string ScenarioTetx = "";

    /// <summary>
    /// ���̃y�[�W�ɐi�߂�ꍇ�ɕ\������A�C�R��
    /// </summary>
    [SerializeField]
    private GameObject Next_Icon;

    /// <summary>
    /// �L���������G�\���ʒu
    /// </summary>
    [SerializeField]
    private Image Chraimage_L, Chraimage_R;

    /// <summary>
    /// �g�p����摜
    /// </summary>
    [SerializeField]
    private Sprite[] Chra_Images;

    /// <summary>
    /// �e�L�X�g���葬�x
    /// </summary>
    [SerializeField,
        Header("�e�L�X�g�̕\�����x"),
        Tooltip("���̕b�����Ƃɕ\��")]
    private float TextSeed = 0.1f;

    /// <summary>
    /// �V�i���I1�y�[�W��
    /// </summary>
    private Queue<char> _charQ;

    /// <summary>
    /// �V�i���I�S�y�[�W��
    /// </summary>
    private Queue<string> _pageQ;

    /// <summary>
    /// ���C���e�L�X�g��\������Text
    /// </summary>
    [SerializeField]
    private Text MainText;

    /// <summary>
    /// ���O�e�L�X�g��\������Text
    /// </summary>
    [SerializeField]
    private Text NameText;

    private void Start()
    {
        //������
        Initializa();


    }

    private void Update()
    {
        //���N���b�N
        if (Input.GetMouseButtonDown(0))
        {
            LClick();
        }
    }

    /// <summary>
    /// ������
    /// </summary>
    private void Initializa()
    {
        //���[�h
        ScenarioTetx = LoadTextFile(TextFile);

        //�y�[�W����
        _pageQ = SeparateString(ScenarioTetx, MAIN_PAGE);

        //�y�[�W�J�n
        ShowNextPage();
    }

    /// <summary>
    /// ���N���b�N���ꂽ�Ƃ�
    /// </summary>
    private void LClick()
    {
        if (_charQ.Count > 0)
        {
            //�S���\��
            OutputAllChar();
        }
        else
        {
            //���̃y�[�W��\��
            if (!ShowNextPage())
            {
                //todo:���u��
                //���s�I��
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
    }

    /// <summary>
    /// �V�i���I�f�[�^��ǂݍ���
    /// </summary>
    /// <param name="textFile">Resources����̑��΃p�X</param>
    /// <returns>�V�i���I�f�[�^�S��</returns>
    private string LoadTextFile(string textFile)
    {
        //�ǂݍ���
        string textAsset = Resources.Load<TextAsset>(textFile).text;

        //���s���Ƃɕ���
        string[] textAssets = textAsset.Split('\n');


        string Rtext = new("");
        foreach (string item in textAssets)
        {
            //�R�����g�s����菜��
            if (!item.TrimStart().StartsWith("//"))
            {
                //�ǉ�
                Rtext += item;
            }

        }

        //���s�R�[�h����菜��
        return Rtext.Replace("\n", "").Replace("\r", "");

    }

    /// <summary>
    /// �P�y�[�W���\��
    /// </summary>
    /// <returns>false:���̃y�[�W���Ȃ�</returns>
    private bool ShowNextPage()
    {
        if (_pageQ.Count <= 0)
        {
            return false;
        }

        //���փA�C�R����\��
        Next_Icon.SetActive(false);

        //1�y�[�W�\��
        ReadLine(_pageQ.Dequeue());

        return true;

    }

    /// <summary>
    /// 1�y�[�W���\���J�n
    /// </summary>
    /// <param name="line">�\������e�L�X�g</param>
    private void ReadLine(string line)
    {
        //�R�}���h�J�n�����m
        if (line[0] == TEXT_COMMAND)
        {
            //�R�}���h���s
            CommandExe(line);

            //���̃y�[�W��
            ShowNextPage();
            return;
        }

        //���O���ƃe�L�X�g�𕪊�
        string[] ts = line.Split(MAIN_START);
        string name = ts[0];

        //���C���e�L�X�g���烁�C���e�L�X�g�I���R�}���h���폜
        string main = ts[1].Remove(ts[1].LastIndexOf(MAIN_END));

        //���Z�b�g
        NameText.text = name;
        MainText.text = "";

        //��������
        _charQ = QString(main);

        //�\���R���[�`���J�n
        StartCoroutine(ShowChar(TextSeed));

    }

    /// <summary>
    /// �R�}���h�J�n
    /// </summary>
    /// <param name="Comline"></param>
    private void CommandExe(string Comline)
    {
        //�R�}���h�J�n���폜
        Comline = Comline.Remove(0, 1);

        //�R�}���h���Ƃɕ���
        Queue<string> cmdQ = SeparateString(Comline, TEXT_COMMAND);

        foreach (string cmd in cmdQ)
        {
            //�p�����[�^�𕪊�
            string[] cmds = cmd.Split(COMMAND_PARAM);

            //�R�}���h�̎�ނ��m�F
            if (cmds[0].Contains(COMMAND_CHARAIMG))
            {
                //�L���������G
                SetCharaImage(cmds);
                break;
            }

            if (cmds[0].Contains(COMMAND_SCENE))
            {
                //�V�[���ύX
                Change_scene(cmds);
                break;
            }

        }


    }

    /// <summary>
    /// �V�[���̐؂�ւ�
    /// </summary>
    /// <param name="cmd"></param>
    private void Change_scene(string[] cmd)
    {
        cmd[1] = GetParameter(cmd[1], COMMAND_PARASTART);
        SceneManager.LoadScene(cmd[1]);
    }

    /// <summary>
    /// �L�����N�^�[�����G
    /// </summary>
    /// <param name="cmd">�R�}���h��</param>
    private void SetCharaImage(string[] cmd)
    {
        //�摜�w��
        if (cmd[0].Contains(COMMAND_SPRITE))
        {
            //�I�u�W�F�N�g�w��
            cmd[1] = GetParameter(cmd[1], COMMAND_PARASTART);


            //�\���摜�w��
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
            //�I�u�W�F�N�g�w��
            cmd[1] = GetParameter(cmd[1], COMMAND_PARASTART);


            //�\���F�w��
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
    /// �L���[�̕�����Main�ɕ\��
    /// </summary>
    /// <param name="wait">�\�����x</param>
    /// <returns></returns>
    private IEnumerator ShowChar(float wait)
    {

        WaitForSeconds waittext = new WaitForSeconds(wait);

        //�����Ȃ�܂ő�����
        while (OutputChar())
        {
            //�\��
            MainText.text += _charQ.Dequeue();

            yield return waittext;

        }

        //�����Ȃ�����I��
        yield break;
    }


    /// <summary>
    /// 1�y�[�W���܂Ƃ߂ĕ\��
    /// </summary>
    private void OutputAllChar()
    {
        //�R���[�`�����~�߂�
        StopCoroutine(ShowChar(TextSeed));

        //�����Ȃ�܂ŕ\��
        while (OutputChar())
        {
            MainText.text += _charQ.Dequeue();
        }

        //���փA�C�R���\��
        Next_Icon.SetActive(true);
    }

    /// <summary>
    /// 1�y�[�W���L���[�������Ȃ������H
    /// </summary>
    /// <returns>false�F�����Ȃ���</returns>
    private bool OutputChar()
    {
        if (_charQ.Count <= 0)
        {
            //���փA�C�R���\��
            Next_Icon.SetActive(true);

            return false;
        }

        return true;
    }

    /// <summary>
    /// string��char�L���[�ɕϊ�
    /// </summary>
    /// <param name="text">�ϊ�string</param>
    /// <returns>char�L���[</returns>
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
    /// paera�ň͂܂�Ă��镶�������o��
    /// </summary>
    /// <param name="cmd">�R�}���h</param>
    /// <param name="para">�͂ݕ����w��</param>
    /// <returns></returns>
    private string GetParameter(string cmd, string para)
    {
        //�I�u�W�F�N�g�w��
        cmd = cmd.Substring(cmd.IndexOf(para) + 1,
            cmd.LastIndexOf(para) - 1);

        return cmd;
    }

    /// <summary>
    /// paera�ň͂܂�Ă��镶�������o��
    /// </summary>
    /// <param name="cmd">�R�}���h</param>
    /// <param name="para">�͂ݕ����w��</param>
    /// <returns></returns>
    private string GetParameter(string cmd, char para)
    {
        //�I�u�W�F�N�g�w��
        cmd = cmd.Substring(cmd.IndexOf(para) + 1,
            cmd.LastIndexOf(para) - 1);

        return cmd;
    }

    /// <summary>
    /// string�𕪊����ăL���[�ɒǉ�
    /// </summary>
    /// <param name="str">��������e�L�X�g</param>
    /// <param name="sep">��������</param>
    /// <returns>���������L���[</returns>
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

