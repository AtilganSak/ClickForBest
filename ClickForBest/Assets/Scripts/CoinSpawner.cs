using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] GameObject coin_prefab;

    [SerializeField] float randomize;

    public void SpawnCoin()
    {
        GameObject coin_instance = Instantiate(coin_prefab);
        coin_instance.transform.position = Vector3.zero;
    }
    public void SetCoinPrefab(GameObject _go)
    {
        if (_go != null)
        {
            coin_prefab = _go;
        }
    }
    public void SetCoinSprite(Sprite _sprite)
    {
        if(_sprite != null)
            coin_prefab.GetComponent<SpriteRenderer>().sprite = _sprite;
    }
}
