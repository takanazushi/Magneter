using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovelCommand
{

    /// <summary>
    /// �R�}���h�F���C���e�L�X�g�̎n�܂�
    /// </summary>
    public const string MAIN_START = "@�u";

    /// <summary>
    /// �R�}���h�F���C���e�L�X�g�̏I���
    /// </summary>
    public const string MAIN_END = "�v@";

    /// <summary>
    /// �R�}���h�F�y�[�W����
    /// </summary>
    public const char MAIN_PAGE = '&';

    /// <summary>
    /// �R�}���h�F�R�}���h�J�n
    /// </summary>
    public const char TEXT_COMMAND = '!';

    /// <summary>
    /// �R�}���h�F�p�����[�^�w��
    /// </summary>
    public const char COMMAND_PARAM = '=';

    /// <summary>
    /// �R�}���h�F�p�����[�^��؂�
    /// </summary>
    public const char COMMAND_PARASTART = '"';

    /// <summary>
    /// �R�}���h�F�L���������G����
    /// </summary>
    public const string COMMAND_CHARAIMG = "charaimg";

    /// <summary>
    /// �R�}���h�F�V�[���؂�ւ�
    /// </summary>
    public const string COMMAND_SCENE= "scene"; 

    /// <summary>
    /// �R�}���h�F�摜�w��
    /// </summary>
    public const string COMMAND_SPRITE = "_sprite";

    /// <summary>
    /// �R�}���h�F�F����
    /// </summary>
    public const string COMMAND_COLOR = "_color";


}
