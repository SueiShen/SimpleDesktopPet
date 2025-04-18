using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Mediator : MonoBehaviour
{
    public string Character = "DONQ";
    public string Sprite = "N01";
    public string Voice = "";
    public string Dailog = "";
    public float ScaleRatio = 5;
    public float Xpos = 0;
    public float Ypos = 0;
    private GameObject Core;
    private GlobalDailogController GlobalDailogController;
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
        Core = GameObject.Find("Core");
        GlobalDailogController = Core.GetComponent<GlobalDailogController>();
        GlobalDailogController.OnMessageReceived += GetDailog;
        Entity = transform.Find("Entity");
        SpriteController = Entity.GetComponent<SpriteController>();
        BoxCollider2D = Entity.GetComponent<BoxCollider2D>();
        EntityAudio = transform.Find("EntityAudio");
        VoiceController = EntityAudio.GetComponent<VoiceController>();
        EntityDailog = transform.Find("EntityDailog");
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
    void GetDailog(string message)
    {
        //Debug.Log("Got : " + message);
        string[] GotMessage = message.Split(new string[] { "|" }, System.StringSplitOptions.None);
        if (GotMessage[0] == "GLOB" || GotMessage[0] ==Character)
        {
            Debug.Log(Character + "Got : " + message);
            Sprite = GotMessage[1];
            SpriteController.SpriteChange();
            Voice = GotMessage[2];
            Dailog = GotMessage[3];
        }
    }
}
