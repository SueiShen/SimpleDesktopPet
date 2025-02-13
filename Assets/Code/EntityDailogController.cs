using System.Data.Common;
using System.IO;
using TMPro;
using UnityEngine;

public class EntityDailogController : MonoBehaviour
{
    private Mediator Mediator;
    private Transform DailogBox;
    private SpriteRenderer SpriteRenderer;
    private float EntityWidth;
    private TextMeshPro DailogText;
    private Renderer Renderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Mediator = GetComponentInParent<Mediator>();
        DailogBox = transform.Find("DailogBox");
        SpriteRenderer = DailogBox.GetComponent<SpriteRenderer>();
        SpriteRenderer.color = Color.white;
        EntityWidth = Screen.height / Mediator.ScaleRatio;
        DailogText = transform.Find("DailogText").GetComponent<TextMeshPro>();
        Renderer = gameObject.GetComponent<Renderer>();
        string filePath = Path.Combine(Application.streamingAssetsPath, "Characters", Mediator.Character, "Dialog/DailogFrame.png");
        if (!File.Exists(filePath))
        {
            filePath = Path.Combine(Application.streamingAssetsPath, "SpriteError.png");
        }
        byte[] imageBytes = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageBytes); // 將 PNG 轉換為 Texture2D
        Sprite spritetexture = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.width * 0.3f), new Vector2(0.5f, 0.5f));
        SpriteRenderer.sprite = spritetexture; // 套用到 3D 物件的材質上
        transform.localScale = new Vector3(EntityWidth / texture.width, EntityWidth / texture.width, 1);
    }

    // Update is called once per frame
    void Update()
    {
        SetText();
    }

    void SetPosition()
    {
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(new Vector3(1 - ((EntityWidth / Screen.height) / 2), 0.05f + ((EntityWidth * 0.3f) / 2 / Screen.height), Camera.main.nearClipPlane));
        transform.localPosition = new Vector3(worldPos.x, worldPos.y, 0);
    }
    void SetText()
    {
        if (Mediator.Dailog == "")
        {
            SetRenderers(false); // 隱藏

        }
        else
        {
            SetPosition();
            SetRenderers(true);  // 顯示
            DailogText.text = Mediator.Dailog;
        }
    }
    void SetRenderers(bool Set)
    {
        foreach (Transform child in transform)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = Set;
            }
        }
    }
}
