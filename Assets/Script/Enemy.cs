using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private GameObject eye, head;
    private Renderer eyeRnd, headRnd;
    private float speed;
    private int colorMode = 3;
    private Color Color1, Color2;
    private Transform rayPos;
    private LineRenderer _line;
    private Renderer renderer;
    private Player player;
    private int rotate;
    public int nowLens;
    private LensManager lensMng;

    RaycastHit rayHit;
    Ray ray;

    void Start()
    {
        eye = GameObject.Find("Enemy").transform.GetChild(0).transform.GetChild(1).gameObject;
        head = GameObject.Find("Enemy").transform.GetChild(0).transform.GetChild(4).gameObject;
        eyeRnd = eye.GetComponent<Renderer>();
        headRnd = head.GetComponent<Renderer>();
        ray = new Ray();
        player = GameObject.Find("Player").GetComponent<Player>();
        rayPos = GameObject.Find("Enemy").transform.GetChild(1).transform;
        Color1 = new Color(1f, 0f, 0f);
        renderer = GameObject.Find("Enemy").transform.GetChild(1).GetComponent<Renderer>();
        _line = GameObject.Find("Enemy").transform.GetChild(1).GetComponent<LineRenderer>();
        _line.enabled = true;
        lensMng = GameObject.Find("LensManager").GetComponent<LensManager>();
        _line.SetWidth(0.2f, 0.2f);
    }

    private void colorSet(int rot)
    {
        if (rot == 0)
        {
            StartCoroutine(setColor(0));
            Color2 = new Color(1f, 0f, 0f);
            colorMode = 0;
            nowLens = 0;
        }
        if (rot == 1)
        {
            StartCoroutine(setColor(1));
            Color2 = new Color(0f, 1f, 0f);
            colorMode = 1;
            nowLens = 1;
        }
        if (rot == 2)
        {
            StartCoroutine(setColor(2));
            Color2 = new Color(0f, 0f, 1f);
            colorMode = 2;
            nowLens = 2;
        }
        if (rot == 3)
        {
            StartCoroutine(setColor(3));
            Color2 = new Color(1f, 1f, 1f);
            colorMode = 3;
            nowLens = 3;
        }
        lensMng.isCool = true;
    }

    void Update()
    {
        if (player.target != null)
            if (player.target.name == "Enemy" && player.got)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    if (rotate == 0)
                    {
                        colorSet(rotate);
                        rotate = 1;
                        return;
                    }
                    else if (rotate == 1)
                    {
                        colorSet(rotate);
                        rotate = 2;
                        return;
                    }
                    else if (rotate == 2)
                    {
                        colorSet(rotate);
                        rotate = 3;
                        return;
                    }
                    else if (rotate == 3)
                    {
                        colorSet(rotate);
                        rotate = 0;
                        return;
                    }
                }

            }

        ray.origin = rayPos.position;
        ray.direction = rayPos.forward;

        switch (colorMode)
        {
            case 0: renderer.material.color = new Color(1f, 0f, 0f, 0.5f); break;
            case 1: renderer.material.color = new Color(0f, 1f, 0f, 0.5f); break;
            case 2: renderer.material.color = new Color(0f, 0f, 1f, 0.5f); break;
            case 3: renderer.material.color = new Color(1f, 1f, 1f, 0.5f); break;
        }


        if (Physics.Raycast(ray.origin, ray.direction, out rayHit, Mathf.Infinity))
        {
            _line.SetPosition(0, ray.origin);
            _line.SetPosition(1, rayHit.point);
            if (rayHit.collider.gameObject.tag == "fuseParent")
            {
                if (rayHit.collider.gameObject.name == "jemul")
                {
                    rayHit.collider.transform.GetChild(0).GetComponent<Lens>().Color1 = Color2;

                    rayHit.collider.transform.GetChild(0).GetComponent<Lens>().colorMode = colorMode;

                    rayHit.collider.transform.GetChild(0).GetComponent<Lens>().firstColor = Color2;
                } else if (rayHit.collider.gameObject.name == "fuze")
                {

                    rayHit.collider.transform.GetChild(1).GetComponent<Lens>().Color1 = Color2;
                    rayHit.collider.transform.GetChild(3).GetComponent<Lens>().Color1 = Color2;
                    rayHit.collider.transform.GetChild(1).GetComponent<Lens>().colorMode = colorMode;
                    rayHit.collider.transform.GetChild(3).GetComponent<Lens>().colorMode = colorMode;
                    rayHit.collider.transform.GetChild(1).GetComponent<Lens>().firstColor = Color2;
                    rayHit.collider.transform.GetChild(3).GetComponent<Lens>().firstColor = Color2;
                }
            }
        }

    }

    IEnumerator setColor(int color)
    {
        switch (colorMode)
        {
            case 0: Color1 = new Color(1f, 0f, 0f); break;
            case 1: Color1 = new Color(0f, 1f, 0f); break;
            case 2: Color1 = new Color(0f, 0f, 1f); break;
            case 3: Color1 = new Color(1f, 1f, 1f); break;
        }
        colorMode = color;

        for (float i = 0; i <= 100; i += 2)
        {
            speed = i / 100f;
            eyeRnd.material.color = Color.Lerp(Color1, Color2, speed);
            headRnd.material.color = Color.Lerp(Color1, Color2, speed);
            yield return new WaitForSeconds(0.01f);
        }
    }

}
