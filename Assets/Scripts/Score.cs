using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    private float time = 0.0f;
    private float score = 0.0f;

    private int difficultyLevel = 1;
    private int maxDifficultyLevel = 100;
    private int scoreToNextLevel = 10;
    public Text scoreText;
    public DeathMenu deathMenu;

    private bool isDead = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (isDead)
            return;

        if (time >= scoreToNextLevel)
            LevelUp();

        time += Time.deltaTime * difficultyLevel;
        scoreText.text = ((int)score).ToString();
    }

    void LevelUp()  // 레벨업 함수
    {
        if (difficultyLevel == maxDifficultyLevel)
            return;

        scoreToNextLevel *= 2;
        difficultyLevel++;
        GetComponent<PlayerMotor>().SetSpeed (difficultyLevel);
        Debug.Log("level up");
    }

    public void OnDeath()   // 죽으면 Death menu 나타남
    {
        isDead = true;
        deathMenu.ToggleEndMenu(score);
    }

    public void CoinScore()     // 코인과 충돌 시 이 함수 실행해 점수 증가
    {
        score += 20;
    }


}
