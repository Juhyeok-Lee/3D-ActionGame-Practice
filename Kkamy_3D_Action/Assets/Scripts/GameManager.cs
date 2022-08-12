using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager m_instance;
    [HideInInspector] public static int currentScene;

    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }

    private void Awake()
    {
        // 게임매니저 싱글톤 생성.
        if (instance != this)
        {
            Destroy(gameObject);
        }       
        DontDestroyOnLoad(gameObject);        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;        
    }   // 씬이 로드될 떄마다 호출되는 이벤트.
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    public void GameStart(int sceneIndex)   // 씬을 인덱스로 불러옴.
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ToTitle()                   // 타이틀로 돌아감.
    {
        SceneManager.LoadScene(0);
    }

    public void SceneReload()               // 현재 씬을 리로드함.
    {
        SceneManager.LoadScene(currentScene);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SceneReload();
        }

        if (Input.GetKeyDown(KeyCode.F8))
        {
            ToTitle();
        }
    }
}
