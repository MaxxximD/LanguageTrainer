using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToggleHandler : MonoBehaviour
{
    public static int topicId;
    public static string name_topic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TopicItemSelected()
    {
        var t = GetComponent<Toggle>();
        if (t.isOn == true)
        {
            topicId = int.Parse(t.GetComponentInChildren<Toggle>().name);
            name_topic = t.GetComponentInChildren<Text>().text;
        }
    }

}
