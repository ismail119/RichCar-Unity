using System.Collections;
using UnityEngine;

public class HousesPlatform : MonoBehaviour
{
        private MoneyStacking moneyStacking;

        private void Awake()
        {
                moneyStacking = FindObjectOfType<MoneyStacking>();
        }

        public void HousesPlatformStart()
        {
                StartCoroutine(MoneySpend());
        }

        private IEnumerator MoneySpend()
        {
                yield return new WaitForSeconds(.28f);
                moneyStacking.PopMoney();
                StartCoroutine(MoneySpend());
        }
}
