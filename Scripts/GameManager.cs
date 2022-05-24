using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private readonly ArrayList _panelList = new ArrayList();

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this) 
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
        IntroducePanels();
    }

    private void Start() => SceneManager.LoadScene(FindObjectOfType<PlayerPrefs>().GetRealLevel());

    public void IntroducePanels()
    {
        _panelList.Clear();
        foreach (Transform child in GameObject.Find("Canvas").transform)
            _panelList.Add(child.transform);
    }
    
    private void SetPanelUp(string panelName)
    {
        foreach (Transform panel in _panelList)
        {
            if (panel.name.Equals(panelName))
            {
                panel.gameObject.SetActive(true);
            }
            else
            {
                panel.gameObject.SetActive(false);
            }
        }
    }
    public void GamePanelSection()
    {
        SetPanelUp("GamePanel");
    }

    public void WinPanelSection()
    {
        SetPanelUp("WinPanel");
    }
    
    public void LosePanelSection()
    {
        SetPanelUp("RestartPanel");
    }
    
}