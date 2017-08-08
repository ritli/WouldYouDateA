using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class Dialogue {

    [XmlAttribute("level")]
    public int level;
    [XmlElement("Lines")]
    public string text;

    [XmlArray("Choices")]
    [XmlArrayItem("Choice")]
    public List<string> choices;

    [XmlAttribute("continue")]
    public int continueDialogue;

    public bool ContinueDialogue
    {
        get
        {
            if (continueDialogue == 1)
            {
                return true;
            }
            return false;
        }

    }
}
