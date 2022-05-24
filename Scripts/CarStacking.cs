using System;
using UnityEngine;

public class CarStacking : MonoBehaviour
{
    
    [SerializeField] private GameObject[] cars = new GameObject[5];
    private PlayerAnimation playerAnimation;
    private GameObject explosionEffect;

    private void Awake()
    {
        playerAnimation = FindObjectOfType<PlayerAnimation>();
        explosionEffect = Resources.Load<GameObject>("Particle/Explosion");
    }

    public void BetterCar()
    {
        int index = Array.IndexOf(cars, GameObject.FindGameObjectWithTag("Car"));
        if (index!=4)
        {
            playerAnimation.PlayerDriving();
            SetActivity(Array.IndexOf(cars,GameObject.FindGameObjectWithTag("Car"))+1);
        }
    }

    public void WorseCar()
    {
        int index = Array.IndexOf(cars, GameObject.FindGameObjectWithTag("Car"));
        if (index==1)
        {
            playerAnimation.PlayerWalking();
        }
        if (index!=0)
        {
            SetActivity(Array.IndexOf(cars,GameObject.FindGameObjectWithTag("Car"))-1);
            
        }
    }
    
    public int GetActiveCarIndex()
    {
        return Array.IndexOf(cars, GameObject.FindGameObjectWithTag("Car"));
    }

    private void SetActivity(int index)
    {
        Destroy(Instantiate(explosionEffect, GameObject.FindGameObjectWithTag("Car").transform.position,
            Quaternion.identity),3f);
        for (int i = 0; i < cars.Length; i++)
        {
            if (i!=index)
                cars[i].SetActive(false);
            else
                cars[i].SetActive(true);
        }
    }
}