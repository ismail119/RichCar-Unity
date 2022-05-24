using DG.Tweening;
using UnityEngine;

public class Collision : MonoBehaviour
{
    //Connections
    private MoneyStacking moneyStacking;
    private GameObject moneyParticle;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        moneyStacking = FindObjectOfType<MoneyStacking>();
        moneyParticle = Resources.Load<GameObject>("Particle/MoneyParticle");
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Money":
                Destroy(Instantiate(moneyParticle,
                    FindObjectOfType<PlayerMovement>().transform.position,Quaternion.identity),2);
                Destroy(other.gameObject);
                if (!other.GetComponent<Collected>().isCollected)
                {
                    moneyStacking.PushMoney();
                }
                break;
            
            case "BadWay":
                if (!other.GetComponent<Collected>().isCollected)
                {
                    Destroy(other.gameObject);
                    other.GetComponent<Collected>().isCollected = true;
                    for (int i = 0; i < 4; i++)
                    {
                        moneyStacking.PopMoney();
                    }
                }
                break;
            
            case "GoodWay":
                if (!other.GetComponent<Collected>().isCollected)
                {
                    Destroy(other.gameObject);
                    other.GetComponent<Collected>().isCollected = true;
                    for (int i = 0; i < 4; i++)
                    {
                        moneyStacking.PushMoney();
                    }
                }
                break;
            case "down":
                Destroy(other.gameObject);
                moneyStacking.PopMoney();
                break;
            case "Finish":
                if (!other.GetComponent<Collected>().isCollected)
                {
                    FindObjectOfType<HousesPlatform>().HousesPlatformStart();
                    Camera.main.transform.DORotate(new Vector3(7.75f, 41f, 0), 1.25f);
                    playerMovement._cameraOffset = new Vector3(-8.45f, 5, -8.5f);
                    other.GetComponent<Collected>().isCollected = true;
                    CanvasController.isFinish = true;
                }
                break;
        }
    }
}
