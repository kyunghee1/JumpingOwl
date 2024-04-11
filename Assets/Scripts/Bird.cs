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

   void InitBird()   //�����ʱ�ȭ
    {
        //������ �̵�����
        dir = (Random.Range(0, 2) == 0) ? -1 : 1;  //-1 or 1
        transform.localScale = new Vector3(dir, 1, 1);

        //�̵��ӵ�
        speed = Random.Range(5, 8f);              //5~8
        anim.speed = 1 + (speed - 5) / 3;         //1~2
    }
  
    void Update()
    {
        CheckAlive();                       //������ ȭ���� �����?
        float amount = speed * dir * Time.deltaTime;
        transform.Translate(Vector3.right * amount);
    }

    void CheckAlive()
    {
        Vector3 worldPos = transform.position;
        worldPos.z = 10;

        Vector2 pos = Camera.main.WorldToScreenPoint(worldPos);
        if((pos.y < -30) ||(dir == -1 && pos.x < -30) || (dir == 1 && pos.x > Screen.width + 30))
        {
            Destroy(gameObject);
        }
    }
}
