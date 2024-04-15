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
            Debug.LogError("Gift �迭�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
            return; //�迭�� �ʱ�ȭ���� �ʾ��� ��� �߰� ������ �����ϱ� ���� ���� ��ȯ
        }
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
    void InitGift()
    {

        //Gift��ȣ
        kind = int.Parse(transform.name.Substring(4, 1));
        


        //Gift Sprite ����
        Sprite[] sprites = Resources.LoadAll<Sprite>("gift");
        GetComponent<SpriteRenderer>().sprite = sprites[kind];

    }

    //�浹ó�� , �ܺ�ȣ��
    void GetGift()
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

       
    }
}

