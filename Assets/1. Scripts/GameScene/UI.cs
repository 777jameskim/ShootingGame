using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public static UI Instance;
    private void Awake() => Instance = this;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;
    private int score;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreText.text = $"Score: {score}";
        }
    }
    private int lives;
    public int Lives
    {
        get { return lives; }
        set
        {
            lives = value;
            livesText.text = $"{value}";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        Lives = GameParams.lives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
