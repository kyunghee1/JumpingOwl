using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    Material mat;            //���͸���
    float speed = 0.05f;     //��ũ�� �ӵ�
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

   
    void Update()
    {
        Vector2 ofs = mat.mainTextureOffset;
        ofs.x += speed * Time.deltaTime;

        mat.mainTextureOffset = ofs;
    }
}
