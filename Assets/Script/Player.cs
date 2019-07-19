using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private LensManager lensMng;
    public float Speed = 0;
    private Animator animator;
    public Transform cameraPos, hand;
    private RaycastHit hitObj;
    public Transform site;
    public float xSensitivity, ySensitivity;
    public bool got = false;
    public GameObject target;
    private bool euler = false;
    public bool questDone = false;
    public Image center;

    public Sprite[] sprites;

    void Start()
    {
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        lensMng = GameObject.Find("LensManager").GetComponent<LensManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        transform.localRotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * xSensitivity, 0);
        cameraPos.localRotation *= Quaternion.Euler(-Input.GetAxis("Mouse Y") * ySensitivity, 0, 0);
        site.localRotation *= Quaternion.Euler(-Input.GetAxis("Mouse Y") * ySensitivity, 0, 0);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerPrefs.SetInt("nowLens", 0);
            lensMng.isCool = true;
            center.sprite = sprites[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerPrefs.SetInt("nowLens", 1);
            lensMng.isCool = true;
            center.sprite = sprites[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerPrefs.SetInt("nowLens", 2);
            lensMng.isCool = true;
            center.sprite = sprites[2];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerPrefs.SetInt("nowLens", 3);
            lensMng.isCool = true;
            center.sprite = sprites[3];
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.enabled = false;
            cameraPos.localPosition = new Vector3(cameraPos.position.x, cameraPos.position.y, 55f);
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            animator.enabled = true;
            cameraPos.localPosition = new Vector3(cameraPos.position.x, cameraPos.position.y, -19.5f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (got)
            {
                if (target.GetComponent<Rigidbody>() != null)
                    target.GetComponent<Rigidbody>().useGravity = true;
                euler = false;
                got = false;
            }
            else if (Physics.Raycast(ray, out hitObj, 10f))
            {
                if (hitObj.collider.gameObject.name != "fuze2")
                    target = hitObj.collider.gameObject;
                if (hitObj.collider.gameObject.tag == "fuze")
                {
                    got = true;
                    target.gameObject.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezeRotation;
                    target.gameObject.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePosition;
                }
                else if (hitObj.collider.gameObject.tag == "Enemy")
                {
                    got = true;
                    target = hitObj.collider.gameObject;
                    euler = true;
                }
            }
        }

        if (euler)
            hitObj.collider.gameObject.transform.eulerAngles = transform.eulerAngles;

        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (got && target.gameObject.name == "jemul")
            {
                if (Physics.Raycast(ray, out hitObj, 10f))
                {
                    if(hitObj.collider.gameObject.name == "fuze")
                    {
                        target.gameObject.transform.position = hitObj.collider.transform.position;
                        target.gameObject.transform.eulerAngles = new Vector3(0f, 90f, 0f);
                        target.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        target.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                        questDone = true;
                        got = false;
                    }
                    if (hitObj.collider.gameObject.name == "fuze2")
                    {
                        target.gameObject.transform.position = hitObj.collider.transform.position;
                        target.gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                        target.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        target.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                        questDone = true;
                        got = false;
                    }
                }
            }
        }

    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * Speed * Time.deltaTime);

        if (got)
        {
            if (target.GetComponent<Rigidbody>() != null)
                target.GetComponent<Rigidbody>().useGravity = false;
            target.transform.position = Vector3.Lerp(target.transform.position, hand.position, 13f * Time.deltaTime);
        }
    }

}
