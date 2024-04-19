using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    Animator anim;           //Animator

    int dir;                 //이동방향
    float speed;             //속도
    void Start()
    {
       anim = GetComponent<Animator>();

        InitBird();
    }

    
    void Update()
    {
        float amount = speed * dir * Time.deltaTime;
        transform.Translate(Vector3.right * amount);
        CheckAlive();
    }
    void InitBird()
    {
        dir = (Random.Range(0, 2) == 0) ? -1 : 1;
        transform.localScale = new Vector3(dir, 1, 1);

        speed = Random.Range(5, 8f);
        anim.speed = 1 + (speed - 5) / 3;

        //참새의 위치를 Screen 좌표로 변환
        Vector3 worldPos = transform.position;
        Vector3 pos = Camera.main .WorldToScreenPoint(worldPos);

        if(dir == -1)
        {
            pos.x = Screen.width + 30;
        }
        else
        {
            pos.x = -30;
        }
        pos.z = 10;
        transform.position = Camera.main.ScreenToWorldPoint(pos);
    }
    void CheckAlive()
    {
        Vector3 worldPos = transform.position;
        worldPos.z = 10;

        Vector2 pos = Camera.main.WorldToScreenPoint(worldPos);
        if((pos.y < -30 || (dir == -1 && pos.x < -30) || (dir == 1 && pos.x > Screen.width + 30)))
        {
            Destroy(gameObject);
        }
    }
}
