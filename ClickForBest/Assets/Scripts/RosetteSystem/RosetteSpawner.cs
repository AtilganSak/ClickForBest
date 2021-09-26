using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static RosetteController;

public class RosetteSpawner : MonoBehaviour
{
    [SerializeField] GameObject rosette_prefab;
    [SerializeField] Transform content;
    [SerializeField] Transform spawn_point;
    [SerializeField] Transform placement_point;
    [SerializeField] Transform canvas_transform;
    [SerializeField] GameObject rotate_object;
    [SerializeField] GameObject start_particle_fx;

    public void GenerateRossette(Item _item)
    {
        placement_point.gameObject.SetActive(true);
        GameObject go = Instantiate(rosette_prefab, canvas_transform);
        go.GetComponent<Image>().sprite = _item.icon;
        go.transform.position = spawn_point.position;
        go.GetComponent<DOMove>().endTarget = placement_point;
        go.GetComponent<DOMove>().doComplete.AddListener(() => SetParent(go.transform));
        go.GetComponent<DOScale>().doComplete.AddListener(CompletedScale);
        go.GetComponent<DOScale>().DO();
    }
    public void LoadRosette(Item _item)
    {
        GameObject go = Instantiate(rosette_prefab, content);
        go.transform.localScale = Vector3.one;
        go.GetComponent<Image>().sprite = _item.icon;
    }
    private void CompletedScale()
    {
        start_particle_fx.GetComponent<DOScale>().DO();
        rotate_object.GetComponent<DOScale>().DO();
        Invoke("StopRotateObject", 2.5F);
    }
    private void StopRotateObject()
    {
        start_particle_fx.GetComponent<DOScale>().DORevert();
        rotate_object.GetComponent<DOScale>().DORevert();
    }
    private void SetParent(Transform _ros)
    {
        _ros.SetParent(content);
        _ros.SetAsFirstSibling();
        placement_point.SetAsFirstSibling();
        placement_point.gameObject.SetActive(false);
    }
}
