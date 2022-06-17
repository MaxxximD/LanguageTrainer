using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class SelectionMethod : MonoBehaviour
{
    Dropdown dropdown;
    public static Dictionary<int, string> MethodControl;
    
    public static int IndMethodControl;

    // Start is called before the first frame update
    void Start()
    {
        MethodControl = GetMethod();
        dropdown = GetComponent<Dropdown>();

        dropdown.options.Clear();
        foreach (var item in MethodControl)
        {
            dropdown.options.Add(new Dropdown.OptionData(item.Value));
        }

        Method_DropDownSelected(dropdown);
        dropdown.onValueChanged.AddListener(delegate { Method_DropDownSelected(dropdown); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    Dictionary<int, string> GetMethod()
    {
        return new Dictionary<int, string>
        {
            {1, "Рус -> Eng" },
            {2, "Eng -> Рус" },
            {3, "Audio -> Рус" }
        };
    }

   
    public void Method_DropDownSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        string name = dropdown.options[index].text;
        IndMethodControl = GetMethod().FirstOrDefault(x => x.Value == name).Key;
        //Debug.Log(IndMethodControl);
        
    }
}
