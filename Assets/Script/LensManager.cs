using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensManager : MonoBehaviour
{

    public bool isCool = false;
    public float speed;
    
    public IEnumerator setColor()
    {
        isCool = false;
        for (float i = 0; i <= 100; i += 2)
        {
            speed = i / 100f;
            yield return new WaitForSeconds(0.01f);
        }
    }

}
