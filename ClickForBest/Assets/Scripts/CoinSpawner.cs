using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] GameObject coin_prefab;

    [SerializeField] float randomize;

    public void SpawnCoin()
    {
        GameObject coin_instance = Instantiate(coin_prefab);
        coin_instance.transform.position = Vector3.zero;

        ReferenceKeeper.Instance.GameManager.AddScore();
        ReferenceKeeper.Instance.GameManager.AddCoin();
    }
}
