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
        //ȭ���� ����� ����
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        if(pos.y < -30)
        {
            Destroy(gameObject);
        }
    }

    //Gift �ʱ�ȭ
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

    //�浹ó�� , �ܺ�ȣ��
   /* void GetGift()
    {
        //GameManager �� ����
        GameObject.Find("GameManager").SendMessage("GetGift", kind);

        GetComponent<AudioSource>().Play();

        GameObject score = Instantiate(Resources.Load("Score")) as GameObject;
        score.SendMessage("SetScore", 100 + kind * 100);//100~300   ->kind�� ���� �ʱ�ȭ �Լ����� 0~2�� �Ҵ�Ǿ� ����
        score.transform.position = transform.position;

        //0.5�� �Ŀ� ����
        Destroy(GetComponent<Collider>());             //�ݶ�������, �浹����
        GetComponent<SpriteRenderer>().sprite = null;  //��������Ʈ ����, 
        Destroy(gameObject, 0.5f);

       
    }*/
}

