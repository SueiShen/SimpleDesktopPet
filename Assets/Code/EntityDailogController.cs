using TMPro;
using UnityEngine;

public class EntityDailogController : MonoBehaviour
{
    private Mediator Mediator;
    private Transform DailogBox;
    private TextMeshPro DailogText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Mediator = GetComponentInParent<Mediator>();
        DailogBox = transform.Find("DailogBox");
        DailogText = transform.Find("DailogText").GetComponent<TextMeshPro>();

    }

    // Update is called once per frame
    void Update()
    {
        DailogBox.transform.localPosition = new Vector3(Mediator.Xpos,Mediator.Ypos,0);
    }
    void SetText(){

    }
    /*
    實體 = 高/倍率
    posx = 寬/2-實體/2
    825
    posy 負的 = 高/2 - (高*0.05 + 框高/2=50)
    -436
    270 100
*/
}
