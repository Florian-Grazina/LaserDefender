using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1f;
    private Vector2 offset;
    Material myMaterial;

    protected void Awake()
    {
        myMaterial = GetComponent<Renderer>().material;
    }

    protected void Update()
    {
        myMaterial.mainTextureOffset += new Vector2(0, scrollSpeed * Time.deltaTime);
    }
}
