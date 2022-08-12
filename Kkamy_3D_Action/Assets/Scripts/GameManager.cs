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
        // ���ӸŴ��� �̱��� ����.
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
    }   // ���� �ε�� ������ ȣ��Ǵ� �̺�Ʈ.
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    public void GameStart(int sceneIndex)   // ���� �ε����� �ҷ���.
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ToTitle()                   // Ÿ��Ʋ�� ���ư�.
    {
        SceneManager.LoadScene(0);
    }

    public void SceneReload()               // ���� ���� ���ε���.
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
