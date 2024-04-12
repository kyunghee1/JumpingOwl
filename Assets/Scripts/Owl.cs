using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : MonoBehaviour
{
    Animator anim;         //Animator
    Transform chkPoint;    //Check Point

    float moveSpeed = 8f;  //�̵��ӵ�
    float jumpSpeed = 12f; //�����ӵ�
    float gravity = 19f;   //�߷�

    Vector3 moveDir;       //�̵������ �ӵ�
    bool isDead = false;   //���?

    public GameManager manager;
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        chkPoint = transform.Find("CheckPoint");
    }


    void Update()
    {
        if (isDead) return;

        CheckBranch();    //�������� ����
        MoveOwl();        //�û��� �̵�
    }

    //�û��� �̵�
    void MoveOwl()
    {
        //�û��̰� ȭ�� �Ʒ��� �����?
        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
        if(pos.y < -100)
        {
            isDead = true;
            return;
        }

        //Ű�Է�
        float keyValue = Input.GetAxis("Horizontal");
        if (manager.isMobile)
        {
            keyValue =manager.btnAxis;
        }

        //ȭ���� �����ڸ����� ����
        if((keyValue < 0 && pos.x < 40) || (keyValue > 0 && pos.x > Screen.width - 40))
        {
            keyValue = 0;
        }
        moveDir.x = keyValue * moveSpeed;

        //���
        moveDir.y -= gravity * Time.deltaTime;
        moveDir.y = 0;

        //�̵�
        transform.Translate(moveDir * Time.deltaTime);

        //�û��� �ִϸ��̼�
        anim.SetFloat("velocity", moveDir.y);
    }

    //�������� ����
    void CheckBranch()
    {
        //CheckPoint ���� �Ʒ������� 0.1m �̳� ����
        RaycastHit2D hit = Physics2D.Raycast(chkPoint.position, Vector2.down, 0.2f);

        //����� ���
        Debug.DrawRay(chkPoint.position, Vector2.down * 1f, Color.blue);

        //������ ��ü�� ���������̸� ���� �ӵ� ����
        if(hit.collider != null && hit.collider.tag == "Branch")
        {
            moveDir.y = jumpSpeed;
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        Transform other = coll.transform;

        switch(other.tag)
        {
            case "Bird":
                other.SendMessage("DropBird");
                break;
            case "Gift":
                other.SendMessage("GetGift");
                break;
        }
    }
}
