using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;




public class Gift : MonoBehaviour
{
    public GameObject giftPrefab;
    public Sprite[] giftSprites;
    
    void Start()
    {
        SpawnRandomGift();
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
    void SpawnRandomGift()
    {

        GameObject newGift = Instantiate(giftPrefab, RandomPosition(), Quaternion.identity);
        int spriteIndex = Random.Range(0, giftSprites.Length);
        SpriteRenderer spriteRenderer = newGift.GetComponent<SpriteRenderer>();
        if(spriteRenderer != null)
        {
            spriteRenderer.sprite = giftSprites[spriteIndex];
        }

    }
    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(1.0f, 5.0f), 0);
    }

    //충돌처리 , 외부호출
   /* void GetGift()
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

       
    }*/
}

