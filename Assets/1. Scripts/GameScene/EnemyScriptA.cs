using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyScriptA : MonoBehaviour
{
    public PlayerScript player;

    [SerializeField] private float Xboundary = 2;
    [SerializeField] private float Yboundary = 2;
    [SerializeField] private float speed = 1;

    [SerializeField] private float startX = 0;

    [SerializeField] private int intelligence = 1;

    private float Xtravel;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(
            startX,
            Yboundary + 1,
            0);
        transform.DOMoveY(Yboundary, 0.5f)
            .SetEase(Ease.Linear);
        Xtravel = player.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (intelligence == 1)
        {
            speed = 0;
            TracePlayerX();
        }
    }

    private void TracePlayerX()
    {
        if (Xtravel > 0)
        {
            if (Xtravel > Time.deltaTime * speed)
            {
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x + Time.deltaTime * speed, 0 - Xboundary, Xboundary),
                    transform.position.y,
                    0);
            }
            else
            {
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x + Xtravel, 0 - Xboundary, Xboundary),
                    transform.position.y,
                    0);
            }
        }
        else
        {
            if (Xtravel < 0 - Time.deltaTime * speed)
            {
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x - Time.deltaTime * speed, 0 - Xboundary, Xboundary),
                    transform.position.y,
                    0);
            }
            else
            {
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x + Xtravel, 0 - Xboundary, Xboundary),
                    transform.position.y,
                    0);
            }
        }
        Xtravel = player.transform.position.x - transform.position.x;
    }

    public void SetPlayer (PlayerScript player)
    {
        this.player = player;
    }
}
