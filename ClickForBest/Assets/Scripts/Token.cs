using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    public GameObject destroy_particle;

    private void OnEnable()
    {
        DOMove dOMove = GetComponent<DOMove>();
        if (dOMove)
        {
            dOMove.doComplete.AddListener(ReachedTarget);
        }
    }
    private void ReachedTarget()
    {
        GameObject go = Instantiate(destroy_particle, ReferenceKeeper.Instance.Canvas3.transform);
        go.transform.position = transform.position;
        Destroy(gameObject);
    }
}
