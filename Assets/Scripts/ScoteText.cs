using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoteText : MonoBehaviour
{
    public Text txtScore;         //Text Widget
    float speed = 1f;             //이동속도
    void Start()
    {
        StartCoroutine(Fadeout());   //투명하게 사라지기
        //SetScore(-200);              //점수 설정이 되는지 알아보기 위한 테스트용: 테스트 끝나면 삭제
    }
    //위로 이동
    void Update()
    {
        float amount = speed * Time.deltaTime;
        transform.Translate(Vector3.up * amount);

    }

    //투명하게 사라지기
    IEnumerator Fadeout()
    {
        yield return new WaitForSeconds(1f);   //1초 후부터 투명해짐. 대기하는 동안에도 Update()는 실행되므로 오브젝트 위로 이동
        Color color = txtScore.color;

        //투명하게 사라지기
        for(float alpha = 1; alpha > 0; alpha-= 0.02f)
        {
            color.a = alpha;
            txtScore.color = color;

            yield return null;
        }
        Destroy(gameObject);                 //완전히 투명해지면 오브젝트를 삭제
    }

    //점수 설정, 외부 호출
    void SetScore (int score)
    {
        txtScore.text = score.ToString("+0; -0");

        if(score < 0)
        {
            txtScore.color = Color.red;
        }
    }
 
}
