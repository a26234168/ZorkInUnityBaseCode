using UnityEngine;
using Zork.Common;
using TMPro;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class UnityOutputService : MonoBehaviour, IOutputService
{
    [SerializeField]
    private int MaxEnteries = 60;

    [SerializeField]
    private Transform OutputTextContainer;


    [SerializeField]
    private TextMeshProUGUI TextLinePrefab;

    [SerializeField]
    private Image NewLinePrefab;


    public UnityOutputService() => mEntries = new List<GameObject>();

    public void Clear() => mEntries.ForEach(entry => Destroy(entry));

    public void Write(string value) => ParseAndWriteLine(value);

    public void WriteLine(string value) => ParseAndWriteLine(value);

    private void ParseAndWriteLine(string value)
    {
        string[] delimiters = { "\n" };

        var lines = value.Split(delimiters, StringSplitOptions.None);
        foreach (var line in lines)
        {
            if(mEntries.Count >= MaxEnteries)
            {
                var entry = mEntries.First();
                Destroy(entry);
                mEntries.Remove(entry);
            }
            if (string.IsNullOrWhiteSpace(line))
            {
                WriteNewLine();
            }
            else
            {
                WriteTextLine(line);
            }
        }
    }
    private void WriteNewLine()
    {
        var newline = Instantiate(NewLinePrefab);
        newline.transform.SetParent(OutputTextContainer, false);
        mEntries.Add(newline.gameObject);
    }


    private void WriteTextLine(string value)
    {
        var textLine = Instantiate(TextLinePrefab);
        textLine.transform.SetParent(OutputTextContainer, false);
        textLine.text = value;
        mEntries.Add(textLine.gameObject);
    }

    

    private readonly List<GameObject> mEntries;
}
