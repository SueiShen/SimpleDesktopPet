using UnityEngine;

public class Mediator : MonoBehaviour
{
    public string Character = "DONQ";
    public string Sprite = "N01";
    public string Voice = "";
    public string Dailog = "";
    private Transform Entity;
    private SpriteController SpriteController;
    private Transform EntityAudio;
    private VoiceController VoiceController;
    private Transform EntityDailogCanvas;
    private EntityDailogController EntityDailogController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform Entity = transform.Find("Entity");
        SpriteController = Entity.GetComponent<SpriteController>();
        Transform EntityAudio = transform.Find("EntityAudio");
        VoiceController = EntityAudio.GetComponent<VoiceController>();
        Transform EntityDailogCanvas = transform.Find("EntityDailogCanvas");
        EntityDailogController = EntityDailogCanvas.GetComponent<EntityDailogController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
