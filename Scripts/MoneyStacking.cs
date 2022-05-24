using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MoneyStacking : MonoBehaviour
{
    // On Game Side
    [SerializeField] private GameObject moneyPrefab;
    // On Memory Side
    private Stack<GameObject> moneyStack = new Stack<GameObject>();
    
    //Connections
    private CarStacking carStacking;
    [SerializeField] private Image barImage;
    private GameObject SpendEffect;
    [SerializeField] private GameObject[] houses;
    

    private void Awake()
    {
        carStacking = FindObjectOfType<CarStacking>();
        SpendEffect = Resources.Load<GameObject>("Particle/Spend");
    }

    public void PushMoney()
    {
        Transform activeStackPos=GameObject.FindGameObjectWithTag("Car").transform.GetChild(0);

        GameObject money = Instantiate(moneyPrefab,
            activeStackPos.position+
            new Vector3(0,0.15f,0)*(moneyStack.Count-1), Quaternion.identity);
        money.transform.parent = activeStackPos; 
        moneyStack.Push(money);
        barImage.fillAmount = (float) moneyStack.Count / 10;
        if (moneyStack.Count==10)
        {
            for (int i = 0; i < 10; i++)
            {
                Destroy(moneyStack.Pop());
            }
            carStacking.BetterCar();
            barImage.fillAmount = 0;
        }
        
    }

    public void PopMoney()
    {
        
        if (moneyStack.Count==0)
        {
            if (carStacking.GetActiveCarIndex() == 0)
            {
                if (CanvasController.isFinish)
                {
                    CanvasController.IsGameStarted = false;
                    FindObjectOfType<PlayerAnimation>().PlayerDance();
                    StartCoroutine(SendRay());
                    
                }
                else
                {
                    CanvasController.IsGameStarted = false;
                    FindObjectOfType<PlayerAnimation>().PlayerIdle();
                    GameManager.Instance.LosePanelSection();
                }

                return;
            }
            
            carStacking.WorseCar();
            for (int i = 0; i < 8; i++)
            {
                PushMoney();
            }
        }
        else
        {
            Destroy(Instantiate(SpendEffect, moneyStack.Peek().transform.position, Quaternion.identity),3);
            Destroy(moneyStack.Pop());
        }
        barImage.fillAmount = (float) moneyStack.Count / 10;
    }

    private IEnumerator SendRay()
    {
        yield return new WaitForSeconds(2.5f);
        Ray ray = new Ray(FindObjectOfType<PlayerMovement>().transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            switch (hit.collider.tag)
            {
                case "House1":
                    Camera.main.transform.DOLookAt(houses[0].transform.position, 2);
                    StartCoroutine(FinishWin());
                    break;
                case "House2":
                    Camera.main.transform.DOLookAt(houses[1].transform.position, 2);
                    StartCoroutine(FinishWin());
                    break;
                case "House3":
                    Camera.main.transform.DOLookAt(houses[2].transform.position, 2);
                    StartCoroutine(FinishWin());
                    break;
                case "House4":
                    Camera.main.transform.DOLookAt(houses[3].transform.position, 2);
                    StartCoroutine(FinishWin());
                    break;
            }
        }
    }

    private IEnumerator FinishWin()
    {
        yield return new WaitForSeconds(4);
        GameManager.Instance.WinPanelSection();
    }
}