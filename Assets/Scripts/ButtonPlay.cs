using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;
public class ButtonPlay : MonoBehaviour
{
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Обрабатывает нажатие на слово для воспроизводения соответствующего аудиофайла
    public void OnClick()
    { 
        var name_word = GetComponent<Text>().text;
        var bd = GetComponent<BD_config>();
        var lst = bd.GetWords($"SELECT * FROM Words WHERE Eng = '{name_word}'");
        var mp3_path = lst[0].Mp3Path;
        var path = Path.Combine("Sounds", mp3_path);
        //Debug.Log(path);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>(path);
        audioSource.Play();
    }
}
