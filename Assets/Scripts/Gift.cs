using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    private GameObject[] gifts;
    int kind;
    void Start()
    {
        InitGift();
        if (gifts == null || gifts.Length == 0)
        {
            Debug.LogError("Gift 배열이 초기화되지 않았습니다.");
            return; //배열이 초기화되지 않앗을 경우 추가 오류를 방지하기 위한 조기 반환
        }
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
        GetComponent<SpriteRenderer>().sprite = sprites[kind];

    }

    //충돌처리 , 외부호출
    void GetGift()
    {
        //GameManager 에 통지
        GameObject.Find("GameManager").SendMessage("GetGift", kind);

        GetComponent<AudioSource>().Play();

        GameObject score = Instantiate(Resources.Load("Score")) as GameObject;
        score.SendMessage("SetScore", 100 + kind * 100);//100~300   ->kind의 값은 초기화 함수에서 0~2로 할당되어 있음
        score.transform.position = transform.position;

        //0.5초 후에 삭제
        Destroy(GetComponent<Collider>());             //콜라이제거, 충돌방지
        GetComponent<SpriteRenderer>().sprite = null;  //스프라이트 제거, 
        Destroy(gameObject, 0.5f);

       
    }
}

