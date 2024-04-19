using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    AudioSource music;
    Transform spPoint;
    Vector3 wrdSize;
    void Start()
    {
        InitGame();
    }

    void InitGame()
    {
        music = GetComponent<AudioSource>();
        music.loop = true;

        if(music.clip != null)
        {
            music.Play();
        }

        spPoint = GameObject.Find("SpawnPoint").transform;

        Vector3 scrSize = new Vector3(Screen.width, Screen.height);
        scrSize.z = 10;
        wrdSize = Camera.main.ScreenToWorldPoint(scrSize);
    }
    void Update()
    {
        MakeBranch();
    }
    void MakeBranch()
    {
        int cnt = GameObject.FindGameObjectsWithTag("Branch").Length;
        if (cnt > 3) return;

        Vector3 pos = spPoint.position;
        pos.x = Random.Range(-wrdSize.x * 0.5f, wrdSize.x * 0.5f);

        GameObject branch = Instantiate(Resources.Load("Branch")) as GameObject;
        branch.transform.position = pos;

        spPoint.position += new Vector3(0, 3, 0);
    }
}
