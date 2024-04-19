using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class Owl : MonoBehaviour
{
    Animator anim;
    Transform chkPoint;

    float moveSpeed = 8f;
    float jumpSpeed = 12f;
    float gravity = 19f;

    Vector3 moveDir;
    bool isDead = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        chkPoint = transform.Find("CheckPoint");
    }

    void Update()
    {
     if(isDead) return;

        CheckBranch();
        MoveOwl();
    }
    void MoveOwl()
    {
        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
        if(pos.y < -100)
        {
            isDead = true;
            return;
        }
        float keyValue = Input.GetAxis("Horizontal");

        if((keyValue < 0 && pos.x < 40) || (keyValue > 0 && pos.x > Screen.width - 40))
        {
            keyValue = 0;
        }
        moveDir.x = keyValue * moveSpeed;

        moveDir.y -= gravity * Time.deltaTime;
        moveDir.y = 0;

        transform.Translate(moveDir * Time.deltaTime);

        anim.SetFloat("velocity", moveDir.y);
    }
   void CheckBranch()
    {
        RaycastHit2D hit = Physics2D.Raycast(chkPoint.position, Vector2.down, 0.2f);

        Debug.DrawRay(chkPoint.position, Vector2.down * 1f, Color.blue);

        if(hit.collider != null && hit.collider.tag == "Branch")
        {
            moveDir.y = jumpSpeed;
        }
    }
}
