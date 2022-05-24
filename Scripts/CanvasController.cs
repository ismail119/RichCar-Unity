using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public static bool IsGameStarted { get; set; }
    public static bool isFinish;
    [SerializeField]private Text canvaslevel;

    
    private void Awake()
    {
        isFinish = false;
        GameManager.Instance.IntroducePanels();
        canvaslevel.text = (FindObjectOfType<PlayerPrefs>().GetLevel()+1).ToString();
    } 
    public void StartTheGame()
    {
        IsGameStarted = true;
        GameManager.Instance.GamePanelSection();
        GameObject.Find("Player").GetComponent<Animator>().Play("Walk");
    }
    
    public void ContinueTheGame()
    {
        Time.timeScale = 1;
        GameManager.Instance.GamePanelSection();
    }
    
    public void RestartTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextTheGame()
    {
        if (SceneManager.GetActiveScene().buildIndex==4)
        {
            SceneManager.LoadScene(0);
            FindObjectOfType<PlayerPrefs>().SetLevel(FindObjectOfType<PlayerPrefs>().GetLevel()+1);
            FindObjectOfType<PlayerPrefs>().SetRealLevel(0);
            return;
        }
        FindObjectOfType<PlayerPrefs>().SetLevel(FindObjectOfType<PlayerPrefs>().GetLevel()+1);
        FindObjectOfType<PlayerPrefs>().SetRealLevel(FindObjectOfType<PlayerPrefs>().GetRealLevel()+1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
        
}