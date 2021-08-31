using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLimitDetector : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            ReferenceKeeper.Instance.GameManager.RemoveCoin();

            Destroy(collision.gameObject);
        }
    }
}
