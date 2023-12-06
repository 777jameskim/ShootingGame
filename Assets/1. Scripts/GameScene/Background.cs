using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [System.Serializable]

    public class Ground
    {
        public Transform bottom;
        public Transform middle;
        public Transform top;
    }

    [SerializeField] private List<Ground> grounds;

    //[SerializeField] private Shader scroll;


    // Start is called before the first frame update
    void Start()
    {
        /*
        bottom.GetComponent<SpriteRenderer>().material.shader = scroll;
        middle.GetComponent<SpriteRenderer>().material.shader = scroll;
        top.GetComponent<SpriteRenderer>().material.shader = scroll;
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
