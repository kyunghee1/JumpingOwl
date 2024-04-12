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
        //참새의 위치를 Screen 좌표로 변환
        Vector3 worldPos = transform.position; ;
        Vector3 pos = Camera.main.WorldToScreenPoint(worldPos);

        //이동 방향조사
        if(dir == -1)
        {
            pos.x = Screen.width + 30;
        }
        else
        {
            pos.x = -30;  
        }

        //Screen좌표를 World 좌표로 변환
       // pos.z = 10;
        transform.position = Camera.main.ScreenToViewportPoint(pos);

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
    //충돌 처리, 외부 호출
    void DropBird()
    {
        //GameManager에 통지
        FindObjectOfType<GameManager>().SendMessage("BirdStrike");

        GetComponent<AudioSource>().Play();
        //참새회전 & 애니메이션 중지
        transform.localEulerAngles = new Vector3(0, 0, 180);
        anim.enabled = false;

        //콜라이더 제거 & 중력 적용
        Destroy(GetComponent<Collider2D>());
        GetComponent<Rigidbody2D>().gravityScale = 1;
        speed = 0;

        //점수
        GameObject score = Instantiate(Resources.Load("Score")) as GameObject;
        score.transform.position = transform.position;

        score.SendMessage("SetScore", -100);
    }
}
