using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    private string[] gifts;
    int kind;
    void Start()
    {
        InitGift();
    }

    
    void Update()
    {
        //화면을 벗어나면 제거
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        if(pos.y < -30)
        {
            Destroy(gameObject);
        }
    }

    //Gift 초기화
    void InitGift()
    {

        //Gift번호
        kind = int.Parse(transform.name.Substring(4, 1));
        


        //Gift Sprite 설정
        Sprite[] sprites = Resources.LoadAll<Sprite>("gift");
        //GetComponent<SpriteRenderer>().sprite = sprites[kind];

    }

    //충돌처리 , 외부호출
    void GetGift()
    {
        //GameManager 에 통지
        GameObject.Find("GameManager").SendMessage("GetGift", kind);

        GetComponent<AudioSource>().Play();

        GameObject score = Instantiate(Resources.Load("Score")) as GameObject;
        score.SendMessage("SetScore", 100 + kind * 100);//100~300
        score.transform.position = transform.position;

        //0.5초 후에 삭제
        Destroy(GetComponent<Collider>());
        GetComponent<SpriteRenderer>().sprite = null;
        Destroy(gameObject, 0.5f);
    }
}

