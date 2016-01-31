using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{

    private static MySceneManager _instance = null;
    public static MySceneManager Instance { get { return _instance; } }

    int m_trialAmount = 3;
    int m_trialsLoaded = 0;

	void Awake ()
    {
        _instance = this;

        m_trialAmount = (SceneManager.sceneCountInBuildSettings - 4);
	}


    public static void NextTrial()
    {
        if(_instance.m_trialsLoaded >= _instance.m_trialAmount)
        {
            Debug.Log("Completed Game");
            WonGameScene();
            return;
        }
               
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        _instance.m_trialsLoaded++;
    }

    public static void LostGameScene()
    {
        SceneManager.LoadScene("end_lose");
    }

    public static void WonGameScene()
    {
        SceneManager.LoadScene("end_win");
    }
}
