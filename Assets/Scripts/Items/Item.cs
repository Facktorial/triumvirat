using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] Material sellectedMaterial;
    [SerializeField] Material baseMaterial;
    MeshRenderer mesh;
    ItemHolder holder;

    Vector3 originPosition;

    bool hidden = false;

    private void Start()
    {
        originPosition = transform.position;

        mesh = GetComponent<MeshRenderer>();
        holder = GetComponent<ItemHolder>();

        mesh.material = baseMaterial;
    }

    public void Sellect()
    {
        mesh.material = sellectedMaterial;
    }

    public void Desellect()
    {
        mesh.material = baseMaterial;
    }

    public ItemScriptable GetItem()
    {
        return holder.item;
    }

    public void HideItem()
    {
        hidden = true;
        GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(RespawnItemRoutine());
    }

    public void ShowItem()
    {
        hidden = false;
        GetComponent<MeshRenderer>().enabled = true;
        StopCoroutine(RespawnItemRoutine());
    }

    IEnumerator RespawnItemRoutine()
    {
        yield return new WaitForSeconds(Random.Range(5, 10));

        if (hidden)
            ShowItem();
    }

    private void OnEnable()
    {
        GameEvents.OnGameRestart += ShowItem;
    }

    private void OnDisable()
    {
        GameEvents.OnGameRestart -= ShowItem;
    }
}
