using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Results
{
    //public int Id { get; set; }
    public int LevelId { get; set; }
    public int TopicId { get; set; }
    public string TopicName { get; set; }
    public int WayOfControlId { get; set; }
    public string WayOfControlName { get; set; }
    public int Mistakes { get; set; }
    public int CorrectAnswersOnFirstAttempt { get; set; }
    public int CorrectAnswers { get; set; }
    public int TotalWordsCount { get; set; }
    public double Rating { get; set; }

    public Results(int levelId, int topicId, string topic_name, int method_controlId, string name_method, int mistakes, int correct_answer_onFirst, int correct_answer, int totalWordsCount, double rating)
    {
        LevelId = levelId;
        TopicId = topicId;
        TopicName = topic_name;
        WayOfControlId = method_controlId;
        WayOfControlName = name_method;
        Mistakes = mistakes;
        CorrectAnswersOnFirstAttempt = correct_answer_onFirst;
        CorrectAnswers = correct_answer;
        TotalWordsCount = totalWordsCount;
        Rating = rating;

    }
}


