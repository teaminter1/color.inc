﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage2Manager : MonoBehaviour
{

    private Lens lens;
    public bool quest = false;

    private Transform target, targetTip, targetTip2;

    private GameObject panel;
    private GameObject tip;
    public Sprite check;
    public Transform leftDoor, rightDoor, right, left;
    public Image checklist1, checklist2, checklist3, txt1, txt2, txt3;

    private Player player;

    private bool isUiGone = false;

    // Start is called before the first frame update
    void Start()
    {
        lens = GameObject.Find("jemul").transform.GetChild(0).GetComponent<Lens>();
        panel = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        tip = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        target = GameObject.Find("Canvas").transform.GetChild(2).transform;
        targetTip = GameObject.Find("Canvas").transform.GetChild(3).transform;
        targetTip2 = GameObject.Find("Canvas").transform.GetChild(4).transform;
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        panel.transform.position = Vector3.Lerp(panel.transform.position, target.position, 3f * Time.deltaTime);
        if (isUiGone)
        {
            tip.transform.position = Vector3.Lerp(tip.transform.position, targetTip2.position, 3f * Time.deltaTime);
        }
        else
        {
            tip.transform.position = Vector3.Lerp(tip.transform.position, targetTip.position, 3f * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0))
        {
            isUiGone = true;
        }

        if (quest)
        {
            leftDoor.position = Vector3.Lerp(leftDoor.position, left.position, 0.6f * Time.deltaTime);
            rightDoor.position = Vector3.Lerp(rightDoor.position, right.position, 0.6f * Time.deltaTime);
        }

        if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2)
    || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4)))
        {
            checklist1.sprite = check;
            txt1.color = new Color(txt1.color.r, txt1.color.g, txt1.color.b, 0.5f);
        }

        if (player.questDone)
        {
            checklist2.sprite = check;
            txt2.color = new Color(txt2.color.r, txt2.color.g, txt2.color.b, 0.5f);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (PlayerPrefs.GetInt("nowLens", 0) == 1 && collision.gameObject.name == "jemul")
        {
            quest = true;
            checklist3.sprite = check;
            txt3.color = new Color(txt3.color.r, txt3.color.g, txt3.color.b, 0.5f);
        }

    }

}
