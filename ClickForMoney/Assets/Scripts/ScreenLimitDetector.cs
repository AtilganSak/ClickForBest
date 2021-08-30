using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLimitDetector : MonoBehaviour
{
    public bool destroyer;

    public GameObject handle;
    public GameObject bottom_collider;

    public bool unloading { get; set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);

            ReferenceKeeper.Instance.GameManager.RemoveCoin();

            if (!destroyer)
            {
                if(!handle.activeInHierarchy)
                    handle.SetActive(true);
            }
        }
    }
    public void Pulled_Handle()
    {
        unloading = true;
        ReferenceKeeper.Instance.ClickButton.SetActive(false);
        bottom_collider.SetActive(false);
        handle.SetActive(false);
    }
}
