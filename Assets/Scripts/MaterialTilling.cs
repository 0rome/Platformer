using UnityEngine;

public class MaterialTilling : MonoBehaviour
{
    [SerializeField] private Vector2 tiling = new Vector2(1, 1);
    private MaterialPropertyBlock propertyBlock;
    private Renderer objRenderer;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        propertyBlock = new MaterialPropertyBlock();
    }

    void Update()
    {
        // Обновляем свойства материала
        objRenderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetVector("_Tiling", tiling);
        objRenderer.SetPropertyBlock(propertyBlock);
    }
}
