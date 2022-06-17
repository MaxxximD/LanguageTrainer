using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class BD_config : MonoBehaviour
{
    [SerializeField] private Transform transform_content;
    [SerializeField] private GameObject Prefab;
    
    public SqliteConnection dbConnect;
    private string path;
    

    //Заполняет поле контент ScrollView элементами Prefab (см. Листинг 7)
    public void FillScrollView(List<Topic> l)
    {
        if(Prefab.GetComponent<Button>() != null )
            for (int i = 0; i < l.Count; i++)
            {
                var g = Instantiate(Prefab);
                g.GetComponentInChildren<Text>().text = l[i].Name;
                g.GetComponentInChildren<Button>().name = l[i].TopicId.ToString();
                g.transform.SetParent(transform_content);
                g.transform.localScale = Vector2.one;
            }
        else
            for (int i = 0; i < l.Count; i++)
            {
                var g = Instantiate(Prefab);
                g.GetComponentInChildren<Text>().text = l[i].Name;
                g.GetComponentInChildren<Toggle>().name = l[i].TopicId.ToString();
                g.transform.SetParent(transform_content);
                g.transform.localScale = Vector2.one;
            }
    }
    

    public void ClearContent()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     

    public List<Topic> GetTopics(int levelId)
    {
        return GetTopics($"SELECT * FROM Topics WHERE Level == {levelId}");
    }
    

    public List<Word> GetWords(int topicId)
    {
        return GetWords($"SELECT * FROM Words WHERE TopicId == {topicId}");
    }
  

    public List<Topic> GetTopics(string query)
    {
        List<Topic> l_topics = new List<Topic>();

        path = Application.dataPath + "/db/My_db.bytes";
        dbConnect = new SqliteConnection("Data Source =" + path);
        dbConnect.Open();
        if (dbConnect.State == ConnectionState.Open)
        {
            SqliteCommand cmd = new SqliteCommand();
            cmd.Connection = dbConnect;
            cmd.CommandText = query;
            SqliteDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                 l_topics.Add(new Topic(r.GetInt32(0), r.GetInt32(1), r.GetInt32(2), r.GetString(3)));
            }

        }
        else
        {
            Debug.Log("Error connection");
        }
        return l_topics;
    }

    //Получение значений из таблицы БД с помощью запроса
    public List<Word> GetWords(string query)
    {
        List<Word> l_words = new List<Word>();

        path = Application.dataPath + "/db/My_db.bytes";
        dbConnect = new SqliteConnection("Data Source =" + path);
        dbConnect.Open();
        if (dbConnect.State == ConnectionState.Open)
        {
            SqliteCommand cmd = new SqliteCommand();
            cmd.Connection = dbConnect;
            cmd.CommandText = query;
            SqliteDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                l_words.Add(new Word(r.GetInt32(0), r.GetInt32(1), r.GetString(2), r.GetString(3), r.GetString(4)));
            }

        }
        else
        {
            Debug.Log("Error connection");
        }
        return l_words;
    }

    // Класс для таблицы Topics
    public class Topic
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }

        public Topic(int _id, int _topicId, int _level, string _name)
        {
            Id = _id;
            TopicId = _topicId;
            Level = _level;
            Name = _name;
        }

        public int GetTopicId()
        {
            return TopicId;
        }
    }

    // Класс для таблицы Words
    public class Word
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Mp3Path { get; set; }
        public string Rus { get; set; }
        public string Eng { get; set; }

        public Word(int _id, int _topicId, string _mp3Path, string _rus, string _eng)
        {
            Id = _id;
            TopicId = _topicId;
            Mp3Path = _mp3Path;
            Rus = _rus;
            Eng = _eng;
        }
    }
    
}
