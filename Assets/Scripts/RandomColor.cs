using UnityEngine;

public class RandomColor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(Random.value, Random.value, Random.value, 1.0f);
    }
}
