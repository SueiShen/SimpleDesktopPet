using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SpriteController : MonoBehaviour
{
    private Mediator Mediator;
    private SpriteRenderer SpriteRenderer;
    private BoxCollider2D BoxCollider2D;
    public int ScaleRatio = 5;
    private float EntityWidth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Mediator = GetComponentInParent<Mediator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
        EntityWidth = Screen.height / ScaleRatio;
        SpriteChange();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpriteChange()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Characters", Mediator.Character, "Sprites", Mediator.Sprite + ".png");
        if (!File.Exists(filePath))
        {
            filePath = Path.Combine(Application.streamingAssetsPath, "SpriteError.png");
        }
        byte[] imageBytes = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageBytes); // 將 PNG 轉換為 Texture2D
        Sprite spritetexture = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        SpriteRenderer.sprite = spritetexture; // 套用到 3D 物件的材質上
        BoxCollider2D.size = SpriteRenderer.sprite.bounds.size;
        BoxCollider2D.offset = SpriteRenderer.sprite.bounds.center;
        float Scale = EntityWidth / texture.width;
        transform.localScale = new Vector3(Scale, Scale, 1);
        //transform.localPosition = new Vector3(texture.width*Scale/2)


    }
}
