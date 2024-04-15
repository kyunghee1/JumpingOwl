using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    Animator anim;   //Animator

    int dir;         //�̵�����
    float speed;     //�ӵ�
    void Start()
    {
    anim = GetComponent<Animator>();

    InitBird();      //�����ʱ�ȭ
    }


  
    void Update()
    {
        CheckAlive();                       //������ ȭ���� �����?
        float amount = speed * dir * Time.deltaTime;
        transform.Translate(Vector3.right * amount);
    }

    void CheckAlive()  //ȭ���� ��� ���� ����
    {
        Vector3 worldPos = transform.position;
        worldPos.z = 10;

        Vector2 pos = Camera.main.WorldToScreenPoint(worldPos);

        if((pos.y < -30) ||(dir == -1 && pos.x < -30) || (dir == 1 && pos.x > Screen.width + 30))
        {//�Ʒ��� ���     �������� ���               ���������� ���
            Destroy(gameObject);
        }
    }

    void InitBird()   //�����ʱ�ȭ
    {
        //������ ��ġ�� Screen ��ǥ�� ��ȯ
        Vector3 worldPos = transform.position; ;
        Vector3 pos = Camera.main.WorldToScreenPoint(worldPos);

        //�̵� ��������
        if (dir == -1)
        {
            pos.x = Screen.width + 30;
        }
        else
        {
            pos.x = -30;
        }

        //Screen��ǥ�� World ��ǥ�� ��ȯ
         pos.z = 10;
        transform.position = Camera.main.ScreenToViewportPoint(pos);

        //������ �̵�����
        dir = (Random.Range(0, 2) == 0) ? -1 : 1;  //-1 or 1
        transform.localScale = new Vector3(dir, 1, 1);

        //�̵��ӵ�
        speed = Random.Range(5, 8f);              //5~8
        anim.speed = 1 + (speed - 5) / 3;         //1~2
    }
    //�浹 ó��, �ܺ� ȣ��
    void DropBird()
    {
        //GameManager�� ����
        FindObjectOfType<GameManager>().SendMessage("BirdStrike");

        GetComponent<AudioSource>().Play();
        //����ȸ�� & �ִϸ��̼� ����
        transform.localEulerAngles = new Vector3(0, 0, 180);    //������ �Ӹ��� ���� ���ϰ� �����Ƿ� 180�� ȸ������ ���� ���ϰ�
        anim.enabled = false;

        //�ݶ��̴� ���� & �߷� ����
        Destroy(GetComponent<Collider2D>());                   //�ݶ��̴��� �����ؼ� �߶��ϴ� ������ �浹 �̺�Ʈ�� �߻����� �ʵ���
        GetComponent<Rigidbody2D>().gravityScale = 1;          //������ٵ��� �߷����� �߶�. ���� ���̸� ���� �߶�
        speed = 0;                                             //������ �̵� �ӵ��� 0���� �ؼ� Update()�� ���� �̵��� ����

        //����
        GameObject score = Instantiate(Resources.Load("Score")) as GameObject;
        score.transform.position = transform.position;

        score.SendMessage("SetScore", -100);                   //ȭ�鿡 ���� ǥ��
    }
}
