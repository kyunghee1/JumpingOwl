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
        //GetComponent<SpriteRenderer>().sprite = sprites[kind];

    }

    //�浹ó�� , �ܺ�ȣ��
    void GetGift()
    {
        //GameManager �� ����
        GameObject.Find("GameManager").SendMessage("GetGift", kind);

        GetComponent<AudioSource>().Play();

        GameObject score = Instantiate(Resources.Load("Score")) as GameObject;
        score.SendMessage("SetScore", 100 + kind * 100);//100~300
        score.transform.position = transform.position;

        //0.5�� �Ŀ� ����
        Destroy(GetComponent<Collider>());
        GetComponent<SpriteRenderer>().sprite = null;
        Destroy(gameObject, 0.5f);
    }
}

