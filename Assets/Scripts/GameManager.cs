using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(AudioSource))]
public class GameManager : MonoBehaviour
{ 
    AudioSource music;           //배경 및 게임 오버 음악
    Transform spPoint;           //SpawnPoint
    Vector3 wrdSize;             //화면의 크기(월드좌표)

    Transform owl;               //Owl

    Image pnButton;              //Button Panel
    Image pnOver;                //Game Over Panel

    Text txtHeight;              //Text Widget
    Text txtGift;
    Text txtBird;
    Text txtScore;

    float owlHeight = 0;         //점수 처리용
    int giftScore = 0;
    int giftCnt = 0;
    int birdCnt = 0;
    int score = 0;

    public bool isMobile;        //Mobile Device인가?
    public float btnAxis;        //버튼 Value(-1.0 ~1.0)

    int dir;                     //-1: 왼쪽버튼, 1: 오른쪽 버튼
    bool isOver;                 //게임오버인가?
    void Awake()
    {
        InitGame();
        InitWidget();

    }

    //위젯초기화
    void InitWidget()
    {
        //Movile Device인가?
        isMobile = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;

        isMobile = true;        //Mobile 상태로 전환

        Cursor.visible = isMobile;

        //Mobile Device에만 Button Panel 보이기
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

        //World 의 크기
        Vector3 scrSize = new Vector3(Screen.width, Screen.height);
        scrSize.z = 10;
        wrdSize = Camera.main.ScreenToWorldPoint(scrSize);
    }
    void Update()
    {
        MakeBranch();
        MakeBird();
        MakeGift();

            //화면을 벗어난 나뭇가지 제거
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            if (pos.y < -30)
            {
                Destroy(gameObject);
            }
        if (!isOver) SetScore();
    }

    //Score 계산
    void SetScore()
    {
        //올빼미의 최대 높이 계산
        if (owl.position.y > owlHeight)
        {
            owlHeight = owl.position.y;
        }
        score = Mathf.FloorToInt(owlHeight) * 100 + giftScore - birdCnt * 100;

        //화면표시
        txtHeight.text = owlHeight.ToString("#, ##0.0");
        txtGift.text = giftCnt.ToString();
        txtBird.text = birdCnt.ToString();
        txtScore.text = score.ToString("#, ##0");

    }
    void MakeBranch()
    {
        //나뭇가지의 개수 구하기
        int cnt = GameObject.FindGameObjectsWithTag("Branch").Length;  //FomdGameObjectsWithTag()는 Tag가 같은 오브젝트를 배열로 구함. 오브젝트 하나를 구하는함수는
        if (cnt > 3) return;                                           //FindGameObjectWithTag()다

        //SpawnPoint 높이에 지그재그로 배치
        Vector3 pos = spPoint.position;
        pos.x = Random.Range(-wrdSize.x * 0.5f, wrdSize.x * 0.5f);     //화면의 좌우 절반을 범위로 한다. 월드 좌표는 화면의 중앙이 (0, 0, 0)

        //나뭇가지
        GameObject branch = Instantiate(Resources.Load("Branch")) as GameObject;
        branch.transform.position = pos;

        //SPawnPoint 를 위로 이동
        spPoint.position += new Vector3(0, 3, 0);                      //Vector는 개별적인 값을 할당할 수 없으므로 텍터 연산으로 처리
    }

    //참새만들기
    void MakeBird()
    {
        //참새 수 구하기
        int cnt = GameObject.FindGameObjectsWithTag("Bird").Length;
        if (cnt > 7 || Random.Range(0, 1000) < 980) return;         //매 프레임마다 20/ 1000의 확률로 참새 생성

        Vector3 pos = spPoint.position;
        pos.y -= Random.Range(0, 5f);                              //초기 위치는 SpawnPoint의 0~5 정도 아래

        GameObject bird = Instantiate(Resources.Load("Bird")) as GameObject;
        bird.transform.position = pos;
    }
    void MakeGift()
    {
        //선물상자 수 구하기
        int cnt = GameObject.FindGameObjectsWithTag("Gift").Length;
        if (cnt > 5 || Random.Range(0, 1000) < 900) return;

        //위치
        Vector3 pos = spPoint.position;
        pos.x = Random.Range(-wrdSize.x * 0.5f, wrdSize.x * 0.5f);
        pos.y += Random.Range(0.5f, 1.5f);

        //이름
        GameObject gift = Instantiate(Resources.Load("Gift0")) as GameObject;
        gift.name = "Gift" + Random.Range(0, 3);                 //0~2
        gift.transform.position = pos;
    }

    //선물 상자 만들기
    //나뭇가지 초기화
    void InitBranch()
    {
        //나뭇가지 크기
        float sx = Random.Range(0.5f, 1);

        //나뭇가지 방향
        int x = (Random.Range(0, 2) == 0) ? -1 : 1;    // -1 or 1
        int y = (Random.Range(0, 2) == 0) ? -1 : 1;    // -1 or 1

        //방향과 크기 설정
        transform.localScale = new Vector3(sx * x, y, 1);
    }

    public void OnButtonClick(GameObject button)  //버튼클릭 이벤트
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

    //버튼 Press
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

    //버튼 Up
    public void OnButtonUp()
    {
        //btnAxis = 0;
        dir = 0;
        StartCoroutine(GetButtonAxis());
    }
    //버튼의 가속도 처리
    IEnumerator GetButtonAxis()
    {
        while(true)
        {
            //버튼을 놓았을 때 0 근처이면 보간 중지
            if(dir == 0 && Mathf.Abs(btnAxis) < 0.01)
            {
                btnAxis = 0;
                yield break;
            }

            //결과값의 오차가 0.01 미만이면 보간 중지
            if(Mathf.Abs(dir) - Mathf.Abs(btnAxis) < 0.01)
            {
                btnAxis = dir;
                yield break;
            }

            //선형보간
            btnAxis = Mathf.MoveTowards(btnAxis, dir, 3 * Time.deltaTime);  //MoveTowards()는 Lerp()와 같은 기능의 함수
            yield return new WaitForFixedUpdate();
        }
    }
    //선물획득-외부호출
    void GetGift(int kind)
    {
        giftCnt++;
        giftScore += (kind * 100) + 100;
    }
    //참새와 충돌-외부 충돌
    void BirdStrike()
    {
        birdCnt++;
    }
    //게임 오버 -외부호출
    void SetGameOver()
    {
        isOver = true;
        pnOver.gameObject.SetActive(true);
        Cursor.visible = true;

        //배경음악 바꾸기
        music.clip = Resources.Load("gameover", typeof(AudioClip)) as AudioClip;
        music.loop = false;
        music.Play();
    }
}
