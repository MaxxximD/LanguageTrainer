using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ButtonHandler : MonoBehaviour, IPointerClickHandler 
{
    public static string name_topic;
    public static int topicId;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(name_topic);
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetText(string text)
    {
       Text txt = transform.Find("Text").GetComponent<Text>();
       txt.text = text;
    }

    //Обработчик события при нажатии на кнопку топика
    public void OnPointerClick(PointerEventData eventData)
    {
        var b = eventData.selectedObject;
        name_topic = b.GetComponentInChildren<Text>().text;
        topicId = int.Parse(b.GetComponentInChildren<Button>().name);
    }
}
