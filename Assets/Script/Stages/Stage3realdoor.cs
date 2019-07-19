using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3realdoor : MonoBehaviour
{

    public Stage3Manager stage3mng;


    private void OnTriggerEnter(Collider collision)
    {
        if (stage3mng.done && collision.gameObject.tag == "Player")
        {

            SceneManager.LoadScene("Stage5");
        }
    }

}
