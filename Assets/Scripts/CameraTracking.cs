using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
  
    Transform target;       //Tracking 대상
    float height;           //Target의 최대 높이
    void Start()
    {
        //올빼미의 최대 높이
        target = GameObject.Find("Owl").transform;
        height = target.position.y;
       
    }

    
    void LateUpdate()
    {
        //Target 의 높이
        float ty = target.position.y;
        if (ty <= height)
        {
            return;
        }
            //목적값까지 보간
            float cy = transform.position.y;   //카메라의 높이
            cy = Mathf.Lerp(cy, ty, 5 * Time.deltaTime);  //카메라의 높이를 올빼미의 최대 높이까지 보간

            //카메라 높이 조정
            Vector3 pos = transform.position;
            pos.y = cy - 0.1f;                //카메라 높이를 조금 낮게 해서 올빼미가 화면 아래로 너무 처지는 것 교정
            transform.position = pos;

            //최대 높이 갱신
            height = ty;
        
    }
}
