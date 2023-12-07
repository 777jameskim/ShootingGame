using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float Xboundary = 2;
    [SerializeField] private float Yboundary = 2;
    [SerializeField] private float speed = 2;
    [SerializeField] private float delay = 0.2f;

    [SerializeField] private Sprite centersprite;
    [SerializeField] private Sprite leftsprite;
    [SerializeField] private Sprite rightsprite;

    private int spritemode;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<PlayerAnimation>().delay = delay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(
                transform.position.x,
                Mathf.Clamp(transform.position.y + Time.deltaTime * speed, 0 - Yboundary, Yboundary),
                0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(
                transform.position.x,
                Mathf.Clamp(transform.position.y - Time.deltaTime * speed, 0 - Yboundary, Yboundary),
                0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x + Time.deltaTime * speed, 0 - Xboundary, Xboundary),
                transform.position.y,
                0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x - Time.deltaTime * speed, 0 - Xboundary, Xboundary),
                transform.position.y,
                0);
        }
        int newmode = 0;
        if (Input.GetKey(KeyCode.RightArrow))
            newmode++;
        if (Input.GetKey(KeyCode.LeftArrow))
            newmode--;
        AnimationHandler(newmode);
    }

    private void AnimationHandler(int newmode)
    {
        if(newmode != spritemode)
        {
            switch (newmode)
            {
                case -1:
                    GetComponent<PlayerAnimation>().SetAnimation(PlayerDirection.Left);
                    break;
                case 1:
                    GetComponent<PlayerAnimation>().SetAnimation(PlayerDirection.Right);
                    break;
                default:
                    GetComponent<PlayerAnimation>().SetAnimation(PlayerDirection.Center);
                    break;
            }
            spritemode = newmode;
        }
    }
}
