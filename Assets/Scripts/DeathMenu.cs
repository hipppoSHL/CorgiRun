using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

    public Text scoreText;
    public Image backgroundImg;
    public Color startColor;
    public Color endColor;

    public AudioSource clickSound;

    private bool isShowned = false;
    private float transition = 0;

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);    // 처음에는 Deathmenu 비활성화
	}
	
	// Update is called once per frame
	void Update () {
        if (!isShowned)
            return;
        transition += Time.deltaTime;
        backgroundImg.color = Color.Lerp(startColor, endColor, transition);
        
    }

    public void ToggleEndMenu(float score)
    {
        gameObject.SetActive(true);
        scoreText.text = ((int)score).ToString();
        isShowned = true;
    }

    public void Restart()   // onclick에 연결
    {
        clickSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMenu()    // onclick에 연결
    {
        clickSound.Play();
        SceneManager.LoadScene("Menu");
    }
}
