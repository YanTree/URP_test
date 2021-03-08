using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameComplete;
    [SerializeField] GameObject eggImage;
    [Header("鸡蛋计数器")]
    [SerializeField] TextMeshProUGUI eggScore;

    int currentEggScore = 0;
    Animator animator;

    public void ScorePlusOne()
    {
        currentEggScore += 1;
        eggScore.text = currentEggScore.ToString() + " / 3";
        animator.SetTrigger("PlusOne");
    }

    // 进入下一个场景
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // 退出游戏
    public void QuitGame()
    {
        Debug.Log("You have Quit Game!");
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = eggImage.GetComponent<Animator>();
        FindObjectOfType<AudioManager>().Play("BGM");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEggScore == 3)
        {
            gameComplete.SetActive(true);
            FindObjectOfType<AudioManager>().Play("GameComplete");
            FindObjectOfType<AudioManager>().Stop("BGM");
        }
    }
}
