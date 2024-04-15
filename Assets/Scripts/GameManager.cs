using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(AudioSource))]
public class GameManager : MonoBehaviour
{ 
    AudioSource music;           //��� �� ���� ���� ����
    Transform spPoint;           //SpawnPoint
    Vector3 wrdSize;             //ȭ���� ũ��(������ǥ)

    Transform owl;               //Owl

    Image pnButton;              //Button Panel
    Image pnOver;                //Game Over Panel

    Text txtHeight;              //Text Widget
    Text txtGift;
    Text txtBird;
    Text txtScore;

    float owlHeight = 0;         //���� ó����
    int giftScore = 0;
    int giftCnt = 0;
    int birdCnt = 0;
    int score = 0;

    public bool isMobile;        //Mobile Device�ΰ�?
    public float btnAxis;        //��ư Value(-1.0 ~1.0)

    int dir;                     //-1: ���ʹ�ư, 1: ������ ��ư
    bool isOver;                 //���ӿ����ΰ�?
    void Awake()
    {
        InitGame();
        InitWidget();

    }

    //�����ʱ�ȭ
    void InitWidget()
    {
        //Movile Device�ΰ�?
        isMobile = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;

        isMobile = true;        //Mobile ���·� ��ȯ

        Cursor.visible = isMobile;

        //Mobile Device���� Button Panel ���̱�
        pnButton = GameObject.Find("PanelButton").GetComponent<Image>();
        pnButton.gameObject.SetActive(isMobile);

        //Quit Panel
        pnOver = GameObject.Find("PanelOver").GetComponent<Image>();
        pnOver.gameObject.SetActive(false);

        //Scpre Text
        txtHeight = GameObject.Find("TxtHeight").GetComponent<Text>();
        txtGift = GameObject.Find("TxtGift").GetComponent<Text>();

        txtBird = GameObject.Find("TxtBird").GetComponent<Text>();
        txtScore = GameObject.Find("TxtScore").GetComponent<Text>();

        //Owl
        owl = GameObject.Find("Owl").transform;
    }
  

   void InitGame()
    {
        music = GetComponent<AudioSource>();
        music.loop = true;

        if(music.clip != null)
        {
            music.Play();
        }

        //SpawnPoint
        spPoint = GameObject.Find("SpawnPoint").transform;

        //World �� ũ��
        Vector3 scrSize = new Vector3(Screen.width, Screen.height);
        scrSize.z = 10;
        wrdSize = Camera.main.ScreenToWorldPoint(scrSize);
    }
    void Update()
    {
        MakeBranch();
        MakeBird();
        MakeGift();

            //ȭ���� ��� �������� ����
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            if (pos.y < -30)
            {
                Destroy(gameObject);
            }
        if (!isOver) SetScore();
    }

    //Score ���
    void SetScore()
    {
        //�û����� �ִ� ���� ���
        if (owl.position.y > owlHeight)
        {
            owlHeight = owl.position.y;
        }
        score = Mathf.FloorToInt(owlHeight) * 100 + giftScore - birdCnt * 100;

        //ȭ��ǥ��
        txtHeight.text = owlHeight.ToString("#, ##0.0");
        txtGift.text = giftCnt.ToString();
        txtBird.text = birdCnt.ToString();
        txtScore.text = score.ToString("#, ##0");

    }
    void MakeBranch()
    {
        //���������� ���� ���ϱ�
        int cnt = GameObject.FindGameObjectsWithTag("Branch").Length;  //FomdGameObjectsWithTag()�� Tag�� ���� ������Ʈ�� �迭�� ����. ������Ʈ �ϳ��� ���ϴ��Լ���
        if (cnt > 3) return;                                           //FindGameObjectWithTag()��

        //SpawnPoint ���̿� ������׷� ��ġ
        Vector3 pos = spPoint.position;
        pos.x = Random.Range(-wrdSize.x * 0.5f, wrdSize.x * 0.5f);     //ȭ���� �¿� ������ ������ �Ѵ�. ���� ��ǥ�� ȭ���� �߾��� (0, 0, 0)

        //��������
        GameObject branch = Instantiate(Resources.Load("Branch")) as GameObject;
        branch.transform.position = pos;

        //SPawnPoint �� ���� �̵�
        spPoint.position += new Vector3(0, 3, 0);                      //Vector�� �������� ���� �Ҵ��� �� �����Ƿ� ���� �������� ó��
    }

    //���������
    void MakeBird()
    {
        //���� �� ���ϱ�
        int cnt = GameObject.FindGameObjectsWithTag("Bird").Length;
        if (cnt > 7 || Random.Range(0, 1000) < 980) return;         //�� �����Ӹ��� 20/ 1000�� Ȯ���� ���� ����

        Vector3 pos = spPoint.position;
        pos.y -= Random.Range(0, 5f);                              //�ʱ� ��ġ�� SpawnPoint�� 0~5 ���� �Ʒ�

        GameObject bird = Instantiate(Resources.Load("Bird")) as GameObject;
        bird.transform.position = pos;
    }
    void MakeGift()
    {
        //�������� �� ���ϱ�
        int cnt = GameObject.FindGameObjectsWithTag("Gift").Length;
        if (cnt > 5 || Random.Range(0, 1000) < 900) return;

        //��ġ
        Vector3 pos = spPoint.position;
        pos.x = Random.Range(-wrdSize.x * 0.5f, wrdSize.x * 0.5f);
        pos.y += Random.Range(0.5f, 1.5f);

        //�̸�
        GameObject gift = Instantiate(Resources.Load("Gift0")) as GameObject;
        gift.name = "Gift" + Random.Range(0, 3);                 //0~2
        gift.transform.position = pos;
    }

    //���� ���� �����
    //�������� �ʱ�ȭ
    void InitBranch()
    {
        //�������� ũ��
        float sx = Random.Range(0.5f, 1);

        //�������� ����
        int x = (Random.Range(0, 2) == 0) ? -1 : 1;    // -1 or 1
        int y = (Random.Range(0, 2) == 0) ? -1 : 1;    // -1 or 1

        //����� ũ�� ����
        transform.localScale = new Vector3(sx * x, y, 1);
    }

    public void OnButtonClick(GameObject button)  //��ưŬ�� �̺�Ʈ
    {
        switch(button.name)
        {
            case "BtnAgain":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "BtnQuit":
                Application.Quit();
                break;
        }
    }

    //��ư Press
    public void OnButtonPress(GameObject button)
    {
        switch(button.name)
        {
            case "BtnLeft":
                //btnAxis = -1;
                dir = -1;
                StartCoroutine(GetButtonAxis());
                break;
            case "BtnRight":
                //btnAxis = 1;
                dir = 1;
                StartCoroutine(GetButtonAxis());
                break;
        }
    }

    //��ư Up
    public void OnButtonUp()
    {
        //btnAxis = 0;
        dir = 0;
        StartCoroutine(GetButtonAxis());
    }
    //��ư�� ���ӵ� ó��
    IEnumerator GetButtonAxis()
    {
        while(true)
        {
            //��ư�� ������ �� 0 ��ó�̸� ���� ����
            if(dir == 0 && Mathf.Abs(btnAxis) < 0.01)
            {
                btnAxis = 0;
                yield break;
            }

            //������� ������ 0.01 �̸��̸� ���� ����
            if(Mathf.Abs(dir) - Mathf.Abs(btnAxis) < 0.01)
            {
                btnAxis = dir;
                yield break;
            }

            //��������
            btnAxis = Mathf.MoveTowards(btnAxis, dir, 3 * Time.deltaTime);  //MoveTowards()�� Lerp()�� ���� ����� �Լ�
            yield return new WaitForFixedUpdate();
        }
    }
    //����ȹ��-�ܺ�ȣ��
    void GetGift(int kind)
    {
        giftCnt++;
        giftScore += (kind * 100) + 100;
    }
    //������ �浹-�ܺ� �浹
    void BirdStrike()
    {
        birdCnt++;
    }
    //���� ���� -�ܺ�ȣ��
    void SetGameOver()
    {
        isOver = true;
        pnOver.gameObject.SetActive(true);
        Cursor.visible = true;

        //������� �ٲٱ�
        music.clip = Resources.Load("gameover", typeof(AudioClip)) as AudioClip;
        music.loop = false;
        music.Play();
    }
}
