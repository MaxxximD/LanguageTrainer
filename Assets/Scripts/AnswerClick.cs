using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using UnityEngine.Events;

public class AnswerClick : MonoBehaviour
{
    public TestingPage tst;
    

    // Start is called before the first frame update
    void Start()
    {
         
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnClick()
    {
        var btn = GetComponent<Button>();
        //Debug.Log(btn.GetComponentInChildren<Text>().text);
        GameObject.Find("Down_Panel").GetComponent<TestingPage>().OnClickAnswerButton(btn); //Функция должна иметь модификатор доступа public


    }

   

    
}
