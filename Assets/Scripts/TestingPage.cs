using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;




public class TestingPage : MonoBehaviour
{
    [SerializeField] private Transform transform_content;
    [SerializeField] private GameObject btn_answer;
    [SerializeField] private Text Task_word;
    [SerializeField] private Text Progress_label;
    [SerializeField] private Text Topic_Name;
    
    BD_config bd;

    private List<BD_config.Word> _words;// Список заданий/слов
    int taskCounter; // Количество показанных заданий.
    int localMistakesCounter; // Количество допущенных ошибок в пределах одного задания.
    int mistakesCounter; // Количество допущенных ошибок.
    int wayOfControl; // Число от 1 до 4 – номер способа контроля.
    int topicId;
    string topicName;
    int correctAnswersOnFirstAttempt; // Количество заданий, на которые был дан верный ответ с первой попытки.
    int SZ; // Общее количество слов, по которым ведётся тестирование.
    double rating; // Рейтинг -- результат прохождения тестирования.
    List<BD_config.Word> used; // Список выполненных заданий
    BD_config.Word currTask; // Текущее задание/ слово
    List<BD_config.Word> answers; // ответы на текущее задание
    
    int levelId;
    bool flag;

    bool first = false;

    //Setting setting1;
    //Setting setting2;

    //Храним результаты
    public static Results res;
      
    // Start is called before the first frame update
    void Start()
    {
        bd = GetComponent<BD_config>();

        //btns = GetComponentsInChildren<Button>();
         
        levelId = DropDownMenu.level;
        topicName = ToggleHandler.name_topic;
        wayOfControl = SelectionMethod.IndMethodControl;
        //Debug.Log(wayOfControl);
        Topic_Name.text = topicName;

        topicId = ToggleHandler.topicId;
        taskCounter = 0;
        correctAnswersOnFirstAttempt = 0;
        mistakesCounter = 0;
        rating = 0.0;
        _words = bd.GetWords(ToggleHandler.topicId);
        SZ = _words.Count;
        Progress_label.text = string.Format("Выполнено {0} из {1} ({2}%)", taskCounter, SZ, 0);
        used = new List<BD_config.Word>();

        //setting1 = App.database.GetSetting1();
        //setting2 = App.database.GetSetting2();
       

        NextTask();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextTask()
    {
        flag = true;
        System.Random rnd = new System.Random();
        localMistakesCounter = 0;

        var words = _words.OrderBy(s => rnd.NextDouble()).ToList();
        var item = words.First();

        while (used.Contains(item))
        {
            words = _words.OrderBy(s => rnd.NextDouble()).ToList();
            item = words.First();
        }

        used.Add(item);
        currTask = item;
        words.Remove(currTask);
        answers = words.Take(4).ToList();
        answers.Insert(rnd.Next(5), currTask);
        FillPage();
    }


    private void ClearButtons()
    {
        for (var i = 1; i < 6; i++)
            GameObject.Find($"{i}").GetComponent<Image>().color = Color.white;
    }

    public void FillPage()
    {
        switch (wayOfControl)
        {
            case 1:
                Task_word.text = currTask.Rus;
                for (var i = 1; i < 6; i++)
                {
                    if (first == false)
                    {
                        var g = Instantiate(btn_answer, transform_content);
                        g.GetComponentInChildren<Text>().text = answers[i - 1].Eng;
                        g.GetComponent<Button>().name = i.ToString();
                    }
                    else
                        GameObject.Find($"{i}").GetComponentInChildren<Text>().text = answers[i - 1].Eng;
                }
                first = true;
                break;
            case 2:
                Task_word.text = currTask.Eng;
                for (var i = 1; i < 6; i++)
                {
                    if (first == false)
                    {
                        var g = Instantiate(btn_answer, transform_content);
                        g.GetComponentInChildren<Text>().text = answers[i - 1].Rus;
                        g.GetComponent<Button>().name = i.ToString();
                    }
                    else
                        GameObject.Find($"{i}").GetComponentInChildren<Text>().text = answers[i - 1].Rus;
                }
                first = true;
                break;
            case 3:
                Task_word.text = "Воспроизвести повторно";
                for (var i = 1; i < 6; i++)
                {
                    if (first == false)
                    {
                        var g = Instantiate(btn_answer, transform_content);
                        g.GetComponentInChildren<Text>().text = answers[i - 1].Rus;
                        g.GetComponent<Button>().name = i.ToString();
                    }
                    else
                        GameObject.Find($"{i}").GetComponentInChildren<Text>().text = answers[i - 1].Rus;
                }
                first = true;
                //PlayAudio();
                break;
        }
    }

    //Обработка выбора ответа при нажатии 
    public void OnClickAnswerButton(Button btn)
    {
        var selectedAnswer = answers.FirstOrDefault(w => wayOfControl == 1 ? w.Eng == btn.GetComponentInChildren<Text>().text : w.Rus == btn.GetComponentInChildren<Text>().text);
        //Debug.Log(selectedAnswer.Rus);
        if (currTask == selectedAnswer)
        {
            taskCounter++;
            //progressBar.Progress = taskCounter * 1.0 / SZ;

            double percent = Math.Round(taskCounter * 100.0 / SZ, 2);
            Progress_label.text = string.Format("Выполнено {0} из {1} ({2}%)", taskCounter, SZ, percent);

            if (localMistakesCounter == 0)
                correctAnswersOnFirstAttempt++;
            switch (localMistakesCounter)
            {
                case 0: rating += (double)100 / SZ; break;
                case 1: rating += (double)50 / SZ; break;
                case 2: rating += (double)25 / SZ; break;
                case 3: rating += 12.5 / SZ; break;
                case 4: break;
            }
            // Если тестирование не завершено, выводим следующее задание.
            if (taskCounter < SZ)
            {
                ClearButtons();
                NextTask();
                //Debug.Log(Task_word.text);
            }
            // Если тестирование завершено, передаём результаты
            else
            {

                for (var i = 1; i < 6; i++)
                   GameObject.Find($"{i}").GetComponent<Button>().enabled = false;

                res = new Results(levelId, topicId, topicName, wayOfControl, SelectionMethod.MethodControl[wayOfControl], mistakesCounter, correctAnswersOnFirstAttempt, SZ - mistakesCounter, SZ, rating);
                
                Debug.Log("THE END!!!!");
                SceneManager.LoadScene(6);
            }
        }
        else
        {
            if (flag)
            {
                mistakesCounter++;
                flag = false;
            }
            if (btn.GetComponent<Image>().color != Color.red)
            {
                btn.GetComponent<Image>().color = Color.red;
                localMistakesCounter++;
            }
        }
    }
}
