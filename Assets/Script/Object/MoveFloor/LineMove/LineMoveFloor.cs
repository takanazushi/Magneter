using UnityEngine;
using static MoveFloorMNG;

public class LineMoveFloor : MonoBehaviour
{
    //GameObject LineMoveFloor�Ŏg�p
    //GameObject LineMoveFloorRigitBody�̃{�f�B�^�C�v���L�l�}�e�B�b�N�ɕύX���Ă܂�
    //�L�l�}�e�B�b�N�ɕύX���邱�Ƃŏd�͂̉e���𖳂���
    //GameObject LineMoveFloor��PlatformEffector2D�����Ă܂�
    //PlatformEffector2D�œ����蔻����ゾ���Ɍ��肵�Ă܂�

    //�������̃X�s�[�h
    [SerializeField]
    private float speed;
    public float Setspeed
    {
        set { speed = value; }
    }

    /// <summary>
    /// ���݂̃|�C���g�ԍ�
    /// </summary>
    private int currentIndex;

    /// <summary>
    /// ������Rigidbody2D
    /// </summary>
    private Rigidbody2D rb;

    /// <summary>
    /// �i�񂾋���������ϐ�
    /// </summary>
    private Vector3 oldpos = Vector2.zero;

    /// <summary>
    /// �|�C���g��Transform
    /// </summary>
    [SerializeField]
    Transform[] Transform_Targets;
    public Transform[] SetTransform_Targets
    {
        set { Transform_Targets = value; }
    }

    /// <summary>
    /// ���݂̃|�C���g�ʒu
    /// </summary>
    Vector3 targetPosition;

    /// <summary>
    /// �ړ��p�^�[��
    /// </summary>
    MoveType Movetype;
    public MoveType SetMovetype
    {
        set { Movetype = value; }
    }

    /// <summary>
    /// �����p
    /// �|�C���g�ړ���
    /// </summary>
    int PointMove = 1;

    Vector3 vevold;

    /// <summary>
    /// ����ʍs�p
    /// �ړ��I���t���O
    /// <para>true:�I��</para>
    /// </summary>
    private bool EndMoveflg = false;

    public bool EndMoveFLG { get { return EndMoveflg; } }

    //�v���C���[��ǐՂ����邽��
    private Player_Move player;

    private void Reset()
    {
        this.tag = "MoveFloor";
        speed = 3;
    }

    private void Awake()
    {
        //���g��Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        //�J�n�n�_�Ɉړ�
        transform.position = Transform_Targets[0].position;
        //�����ݒ�
        oldpos = rb.position;
        currentIndex = 0;
        EndMoveflg = false;
        targetPosition = Transform_Targets[currentIndex].position;
        vevold = Vector3.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        player = collision.gameObject.GetComponent<Player_Move>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.name == "Player")
            player = null;
    }

    private void Update()
    {

        if (Transform_Targets.Length > 0)
        {
            //�ړ��ʒu�v��
            Vector3 newPosition = Vector2.MoveTowards(rb.position, targetPosition, speed * Time.deltaTime);

            //�ړ�
            transform.position = newPosition;

            //�ړ��ʒu���Z�o
            vevold = newPosition - oldpos;

            //�O�̈ʒu
            oldpos = newPosition;

            Debug.Log("D : " + rb.transform.position.x);

            //�v���C���[�̈ړ������s
            player?.MoveFloorExec(this);


            //�ڕW�ʒu�ɋ߂Â����玟�̒��_�𓾂�
            Vector3 len = transform.position - targetPosition;
            if (len.sqrMagnitude < 0.1 * 0.1)
            {
                NextTargetPosition();
            }
        }

    }

    /// <summary>
    /// ���̃|�W�V������ݒ�
    /// </summary>
    private void NextTargetPosition()
    {

        switch (Movetype)
        {
            case MoveType.Patrol:
                currentIndex = (currentIndex + 1) % Transform_Targets.Length;
                break;

            case MoveType.Round_Trip:

                currentIndex += PointMove;
                if (currentIndex == Transform_Targets.Length - 1 ||
                    currentIndex <= 0)
                {
                    PointMove = -PointMove;
                }
                break;

            case MoveType.One_Way:
                currentIndex++;
                if (currentIndex + 1 > Transform_Targets.Length)
                {
                    currentIndex--;
                    EndMoveflg = true;
                }
                break;

        }


        //�|�W�V����(���_�̍��W)�ݒ�
        targetPosition = Transform_Targets[currentIndex].position;

    }

    //Player���Ői�񂾋����𓾂邽�߂̊֐�
    public Vector3 GetoldPos()
    {
        return vevold;
    }
}
