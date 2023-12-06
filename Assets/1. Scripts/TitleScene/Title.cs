using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Title : MonoBehaviour
{
    [SerializeField] Transform titleText;
    [SerializeField] Transform startButton;

    // Start is called before the first frame update
    void Start()
    {
        titleText.DOMoveY(-0.5f, 1)
            .SetDelay(1f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                startButton.DOScale(Vector3.one, 0.5f)
                .SetEase(Ease.OutBounce)
                .SetDelay(0.5f);
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
