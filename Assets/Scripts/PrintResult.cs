using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 

public class PrintResult : MonoBehaviour
{
    Results res;

    [SerializeField] private Text raiting;
    [SerializeField] private Text mistakes;
    [SerializeField] private Text totalWordCount;
    [SerializeField] private Text correct_answers;
    [SerializeField] private Text method_contr;
    [SerializeField] private Text topic_name;

    // Start is called before the first frame update
    void Start()
    {
        res = TestingPage.res;
        raiting.text = $"Рейтинг:           {res.Rating}";
        mistakes.text = $"Допущено ошибок:          {res.Mistakes}";
        totalWordCount.text = $"Общее количество слов:          {res.TotalWordsCount}";
        correct_answers.text = $"Количество верных ответов:         {res.CorrectAnswers}";
        method_contr.text = $"Выбранный способ контроля:            {res.WayOfControlName}";
        topic_name.text = $"Название выбранной темы словаря:            {res.TopicName}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
