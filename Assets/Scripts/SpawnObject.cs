using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject spawnObject;
    public void Spawn()
    {
        Instantiate(spawnObject,transform.position,Quaternion.identity);
    }
}
