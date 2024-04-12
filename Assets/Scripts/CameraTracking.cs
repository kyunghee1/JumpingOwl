using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
  
    Transform target;       //Tracking ���
    float height;           //Target�� �ִ� ����
    void Start()
    {
        //�û����� �ִ� ����
        target = GameObject.Find("Owl").transform;
        height = target.position.y;
       
    }

    
    void LateUpdate()
    {
        //Target �� ����
        float ty = target.position.y;
        if (ty <= height)
        {
            return;
        }
            //���������� ����
            float cy = transform.position.y;
            cy = Mathf.Lerp(cy, ty, 5 * Time.deltaTime);

            //ī�޶� ���� ����
            Vector3 pos = transform.position;
            pos.y = cy - 0.1f;
            transform.position = pos;

            //�ִ� ���� ����
            height = ty;
        
    }
}
