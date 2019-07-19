using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2realdoor : MonoBehaviour
{

    private Stage2Manager stage2mng;

    void Start()
    {
        stage2mng = GameObject.Find("fuze").GetComponent<Stage2Manager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (stage2mng.quest && collision.gameObject.tag == "Player")
        {

            SceneManager.LoadScene("Stage3");
        }
    }

}
