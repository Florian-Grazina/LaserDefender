using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] private Vector2 scrollSpeed;
    private Vector2 offset;
    Material myMaterial;

    protected void Awake()
    {
        myMaterial = GetComponent<Renderer>().material;
        offset = scrollSpeed * Time.deltaTime;
    }

    protected void Update()
    {
        myMaterial.mainTextureOffset += offset;
    }
}
