using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage1Manager : MonoBehaviour
{

    private GameObject panel;
    private GameObject tip;
    private Transform target, targetTip, targetTip2;
    public Sprite check;
    public Image checklist1, checklist2, txt1, txt2;
    public Transform leftDoor, rightDoor;
    private bool quest1 = false, quest2 = false;

    public int quest = 0;

    private bool isUiGone = false;

    // Start is called before the first frame update
    void Start()
    {
        panel = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        tip = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        target = GameObject.Find("Canvas").transform.GetChild(2).transform;
        targetTip = GameObject.Find("Canvas").transform.GetChild(3).transform;
        targetTip2 = GameObject.Find("Canvas").transform.GetChild(4).transform;
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

        if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2)
            || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4)) && !quest1)
        {
            checklist1.sprite = check;
            txt1.color = new Color(txt1.color.r, txt1.color.g, txt1.color.b, 0.5f);
            quest++;
            quest1 = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && !quest2)
        {
            checklist2.sprite = check;
            txt2.color = new Color(txt2.color.r, txt2.color.g, txt2.color.b, 0.5f);
            quest++;
            quest2 = true;
        }

        if(quest == 2)
        {
            leftDoor.position = Vector3.Lerp(leftDoor.position, new Vector3(-10.25f, 3.253f, -4.37f), 2f * Time.deltaTime);
            rightDoor.position = Vector3.Lerp(rightDoor.position, new Vector3(-10.25f, 3.253f, 1.66f), 2f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player" && quest == 2)
        {
            SceneManager.LoadScene("Stage2");
        }
    }

}
