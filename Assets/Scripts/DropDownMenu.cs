using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DropDownMenu : MonoBehaviour
{
    Dropdown dropdown;
    private List<Level> levels;
    BD_config bd;
    public static int level;

    // Start is called before the first frame update
    void Start()
    {
        // ссылка на класс, чтобы вызывать его методы в другом скрипте
        bd = GetComponent<BD_config>();
        dropdown = GetComponent<Dropdown>();

        dropdown.options.Clear();
        levels = GetLevels();
        //заполняем dropdown списком элементов
        foreach (var level in levels)
            dropdown.options.Add(new Dropdown.OptionData(level.Name));

        DropDownItemSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate { DropDownItemSelected(dropdown); });
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    //обработчик события при выборе уровня сложности
    public void DropDownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        // название выбранного уровня
        var name = dropdown.options[index].text;
        // находим нужный элемент в списке исходя из выбранного уровня сложности
        var lev = GetLevels().Single(l => l.Name == name);
        level = lev.Id;

        //bd.ClearContent();
        bd.FillScrollView(bd.GetTopics(lev.Id));
    }

    

    class Level
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    List<Level> GetLevels()
    {
        return new List<Level>
        {
            new Level {Id = 1, Name = "Beginner"},
            new Level {Id = 2, Name = "Elementary" },
            new Level {Id = 3, Name = "Intermediate" },
            new Level {Id = 4, Name = "Advanced" },
        };
    }

     
}
