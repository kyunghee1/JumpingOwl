using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    Animator anim;   //Animator

    int dir;         //이동방향
    float speed;     //속도
    void Start()
    {
    anim = GetComponent<Animator>();

    InitBird();      //참새초기화
    }

   void InitBird()   //참새초기화
    {
        //참새의 이동방향
        dir = (Random.Range(0, 2) == 0) ? -1 : 1;  //-1 or 1
        transform.localScale = new Vector3(dir, 1, 1);

        //이동속도
        speed = Random.Range(5, 8f);              //5~8
        anim.speed = 1 + (speed - 5) / 3;         //1~2
    }
  
    void Update()
    {
        CheckAlive();                       //참새가 화면을 벗어났나?
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
