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


  
    void Update()
    {
        CheckAlive();                       //참새가 화면을 벗어났나?
        float amount = speed * dir * Time.deltaTime;
        transform.Translate(Vector3.right * amount);
    }

    void CheckAlive()  //화면을 벗어난 참새 제거
    {
        Vector3 worldPos = transform.position;
        worldPos.z = 10;

        Vector2 pos = Camera.main.WorldToScreenPoint(worldPos);

        if((pos.y < -30) ||(dir == -1 && pos.x < -30) || (dir == 1 && pos.x > Screen.width + 30))
        {//아래로 벗어남     왼쪽으로 벗어남               오른쪽으로 벗어남
            Destroy(gameObject);
        }
    }

    void InitBird()   //참새초기화
    {
        //참새의 위치를 Screen 좌표로 변환
        Vector3 worldPos = transform.position; ;
        Vector3 pos = Camera.main.WorldToScreenPoint(worldPos);

        //이동 방향조사
        if (dir == -1)
        {
            pos.x = Screen.width + 30;
        }
        else
        {
            pos.x = -30;
        }

        //Screen좌표를 World 좌표로 변환
         pos.z = 10;
        transform.position = Camera.main.ScreenToViewportPoint(pos);

        //참새의 이동방향
        dir = (Random.Range(0, 2) == 0) ? -1 : 1;  //-1 or 1
        transform.localScale = new Vector3(dir, 1, 1);

        //이동속도
        speed = Random.Range(5, 8f);              //5~8
        anim.speed = 1 + (speed - 5) / 3;         //1~2
    }
    //충돌 처리, 외부 호출
    void DropBird()
    {
        //GameManager에 통지
        FindObjectOfType<GameManager>().SendMessage("BirdStrike");

        GetComponent<AudioSource>().Play();
        //참새회전 & 애니메이션 중지
        transform.localEulerAngles = new Vector3(0, 0, 180);    //참새의 머리가 위를 향하고 있으므로 180도 회전시켜 지면 향하게
        anim.enabled = false;

        //콜라이더 제거 & 중력 적용
        Destroy(GetComponent<Collider2D>());                   //콜라이더를 제거해서 추락하는 참새는 충돌 이벤트가 발생하지 않도록
        GetComponent<Rigidbody2D>().gravityScale = 1;          //리지드바디의 중력으로 추락. 값을 높이면 빨리 추락
        speed = 0;                                             //참새의 이동 속도를 0으로 해서 Update()의 참새 이동을 금지

        //점수
        GameObject score = Instantiate(Resources.Load("Score")) as GameObject;
        score.transform.position = transform.position;

        score.SendMessage("SetScore", -100);                   //화면에 감점 표시
    }
}
