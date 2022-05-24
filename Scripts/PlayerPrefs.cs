using UnityEngine;

public class PlayerPrefs : MonoBehaviour
{
    private void Awake() { 
        if (!UnityEngine.PlayerPrefs.HasKey("Level"))
            UnityEngine.PlayerPrefs.SetInt("Level",0); 
        if (!UnityEngine.PlayerPrefs.HasKey("RealLevel"))
            UnityEngine.PlayerPrefs.SetInt("RealLevel",0); 
    }

    public int GetLevel() { return UnityEngine.PlayerPrefs.GetInt("Level"); }
    public void SetLevel(int level) { UnityEngine.PlayerPrefs.SetInt("Level",level); }
    public int GetRealLevel() { return UnityEngine.PlayerPrefs.GetInt("RealLevel"); }
    public void SetRealLevel(int level) { UnityEngine.PlayerPrefs.SetInt("RealLevel",level); }
}
