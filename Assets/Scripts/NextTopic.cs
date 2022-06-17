using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;


public class NextTopic : MonoBehaviour
{
    public BD_config bd;
    public ButtonHandler bh;

    [SerializeField] private Transform transform_content1;
    [SerializeField] private Transform transform_content2;
    [SerializeField] private GameObject Prefab_left;
    [SerializeField] private GameObject Prefab_right;
    [SerializeField] private Text NameTopic;

    public List<BD_config.Word> l_words;
    public List<BD_config.Topic> l_topics;

    // Start is called before the first frame update
    void Start()
    {
        bd = transform.GetComponent<BD_config>();
        string name = ButtonHandler.name_topic;
        NameTopic.text = name;
        int topicId = ButtonHandler.topicId;

        l_words = bd.GetWords(topicId);
        FillPanel(l_words);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    


    public void FillPanel(List<BD_config.Word> l)
    {
        for (int i = 0; i < l.Count; i++)
        {
            var g1 = Instantiate(Prefab_left);
            g1.GetComponentInChildren<Text>().text = l[i].Eng;
            g1.transform.SetParent(transform_content1);
            g1.transform.localScale = Vector2.one;

            var g2 = Instantiate(Prefab_right);
            g2.GetComponentInChildren<Text>().text = l[i].Rus;
            g2.transform.SetParent(transform_content2);
            g2.transform.localScale = Vector2.one;
        }
    }

    
}
