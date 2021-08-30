using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] GameObject coin_prefab;

    [SerializeField] float randomize;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnCoin();
        }
    }
    public void SpawnCoin()
    {
        GameObject coin_instance = Instantiate(coin_prefab);
        coin_instance.transform.position = new Vector3(Random.Range(-randomize, randomize), -1.5F, 0);

        ReferenceKeeper.Instance.GameManager.AddCoin();
    }
}
