using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoteText : MonoBehaviour
{
    public Text txtScore;         //Text Widget
    float speed = 1f;             //�̵��ӵ�
    void Start()
    {
        StartCoroutine(Fadeout());   //�����ϰ� �������
        //SetScore(-200);              //���� ������ �Ǵ��� �˾ƺ��� ���� �׽�Ʈ��: �׽�Ʈ ������ ����
    }
    //���� �̵�
    void Update()
    {
        float amount = speed * Time.deltaTime;
        transform.Translate(Vector3.up * amount);

    }

    //�����ϰ� �������
    IEnumerator Fadeout()
    {
        yield return new WaitForSeconds(1f);   //1�� �ĺ��� ��������. ����ϴ� ���ȿ��� Update()�� ����ǹǷ� ������Ʈ ���� �̵�
        Color color = txtScore.color;

        //�����ϰ� �������
        for(float alpha = 1; alpha > 0; alpha-= 0.02f)
        {
            color.a = alpha;
            txtScore.color = color;

            yield return null;
        }
        Destroy(gameObject);                 //������ ���������� ������Ʈ�� ����
    }

    //���� ����, �ܺ� ȣ��
    void SetScore (int score)
    {
        txtScore.text = score.ToString("+0; -0");

        if(score < 0)
        {
            txtScore.color = Color.red;
        }
    }
 
}
