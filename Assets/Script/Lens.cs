using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lens : MonoBehaviour
{
    public Color Color1, Color2;
    private LensManager lensMng;
    private Renderer renderer;
    public Color firstColor;
    public int colorMode = 0;
    private Player player;
    private float speed;
    public Enemy enemy;
    public GameObject enemyOb;

    void Awake()
    {
        renderer = GetComponent<Renderer>();
        firstColor = Color1;
        player = GameObject.Find("Player").GetComponent<Player>();
        lensMng = GameObject.Find("LensManager").GetComponent<LensManager>();

        if (Color1.r == 1f && Color1.g == 0f && Color1.b == 0f)
        {
            colorMode = 0;
        }
        else if (Color1.r == 0f && Color1.g == 1f && Color1.b == 0f)
        {
            colorMode = 1;
        }
        else if (Color1.r == 0f && Color1.g == 0f && Color1.b == 1f)
        {
            colorMode = 2;
        }
        else if (Color1.r == 1f && Color1.g == 1f && Color1.b == 1f)
        {
            colorMode = 3;
        }
    }

    void Update()
    {
        if (lensMng.isCool)
            StartCoroutine(lensMng.setColor());

        if (enemyOb != null)
        {
            Debug.Log("zz");
            enemy = enemyOb.GetComponent<Enemy>();
            PlayerPrefs.SetInt("nowLens", 1);
            colorMode = enemy.nowLens;
        }

        ChangeColor(colorMode);

        if (renderer.material.color == Color2)
            Color1 = Color2;
    }

    private void ChangeColor(int colorCode)
    {
        if (PlayerPrefs.GetInt("nowLens", 0) == 0)
        {
            Color2 = firstColor;
        }
        else if (PlayerPrefs.GetInt("nowLens", 0) == 1)
        {
            switch (colorCode)
            {
                case 0: Color2 = new Color(1f, 0f, 0f); break;
                case 1: Color2 = new Color(200f / 255f, 1f, 0f); break;
                case 2: Color2 = new Color(140f / 255f, 0f, 1f); break;
                case 3: Color2 = new Color(1f, 0f, 0f); break;
            }
        }
        else if (PlayerPrefs.GetInt("nowLens", 0) == 2)
        {
            switch (colorCode)
            {
                case 0: Color2 = new Color(1f, 100f / 255f, 0f); break;
                case 1: Color2 = new Color(0f, 1f, 0f); break;
                case 2: Color2 = new Color(0f, 170 / 255f, 1f); break;
                case 3: Color2 = new Color(0f, 1f, 0f); break;
            }
        }
        else if (PlayerPrefs.GetInt("nowLens", 0) == 3)
        {
            switch (colorCode)
            {
                case 0: Color2 = new Color(1f, 0f, 140f / 255f); break;
                case 1: Color2 = new Color(0f, 1f, 140f / 255f); break;
                case 2: Color2 = new Color(0f, 0f, 1f); break;
                case 3: Color2 = new Color(0f, 0f, 1f); break;
            }
        }
        renderer.material.color = Color.Lerp(Color1, Color2, lensMng.speed);

    }

}
