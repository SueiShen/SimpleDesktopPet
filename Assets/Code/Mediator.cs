using System.Collections;
using UnityEngine;

public class Mediator : MonoBehaviour
{
    public string Character = "DONQ";
    public string Sprite = "N01";
    public string Voice = "";
    public string Dailog = "";
    public float Xpos = 0;
    public float Ypos = 0;
    private Transform Entity;
    private SpriteController SpriteController;
    private BoxCollider2D BoxCollider2D;
    private Transform EntityAudio;
    private VoiceController VoiceController;
    private Transform EntityDailog;
    private EntityDailogController EntityDailogController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform Entity = transform.Find("Entity");
        SpriteController = Entity.GetComponent<SpriteController>();
        BoxCollider2D = Entity.GetComponent<BoxCollider2D>();
        Transform EntityAudio = transform.Find("EntityAudio");
        VoiceController = EntityAudio.GetComponent<VoiceController>();
        Transform EntityDailog = transform.Find("EntityDailog");
        EntityDailogController = EntityDailog.GetComponent<EntityDailogController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Entity == null)
        {
            Entity = transform.Find("Entity");
        }
        Xpos = Entity.localPosition.x;
        Ypos = Entity.localPosition.y;
    }
}
