using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FNAFGameManager : MonoBehaviour
{
    public TextMeshProUGUI textWord, counter;
    public static FNAFGameManager FNAFManager;
    public string strrough,str2make;
    [HideInInspector] public string str;
    public string[] words;
    public List<PhoneButton> buttons;
    public int words2form;
    private float time =45f;
    public bool screamerOn, phoneUp = true;

    void Start()
    {
        if (FNAFManager != null)
        {
            Destroy(gameObject);
        }
        FNAFManager = this;

        StartCoroutine(wordsCoroutine());
    }

    void Update()
    {
        time-=Time.deltaTime;
        counter.text = Mathf.CeilToInt(time).ToString();
        if (time <= 0f)
        {
            Debug.Log("PERDU");
            //DEFEAT
        }
        if(screamerOn && phoneUp)
        {
            Debug.Log("PERDU");
            //DEFEAT
        }
    }

    private IEnumerator wordsCoroutine()
    {
        while (words2form > 0)
        {
            Reset();
            Set();
            str = ""; 
            while (str != str2make)
            {
                for(int i = 0; i<str.Length;i++)
                {
                    if(str[i]!= str2make[i])
                    {
                        Reset();
                    }
                }
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
            words2form--;
        }
        //VICTORY
    }

    private void Set()
    {
        strrough = words[Random.Range(0,words.Length)];
        str2make = strrough.Replace("-","");
        textWord.text = str2make;
        string[] parts = strrough.Split(new string[] {"-"},System.StringSplitOptions.None);
        
        List<PhoneButton> buttons2 = new List<PhoneButton>();
        buttons2.AddRange(buttons);
        foreach(PhoneButton p in buttons)
        {
            p.gameObject.SetActive(false);
        }
        foreach(string s in parts)
        {
            PhoneButton p = buttons2[Random.Range(0,buttons2.Count)];
            p.TMP.text = s;
            p.clicked = false;
            p.gameObject.SetActive(true);
            buttons2.Remove(p);
        }

    }
    private void Reset()
    {
        str = "";
        foreach(PhoneButton b in buttons)
        {
            b.UnClick();
        }
    }
}
