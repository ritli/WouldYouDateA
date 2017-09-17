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

    [XmlArray("Responses")]
    [XmlArrayItem("Response")]
    public List<string> response;

    [XmlAttribute("continue")]
    public int continueDialogue;

    [XmlAttribute("leave")]
    public int leaveDialogue;

    public bool LeaveDialogue
    {
        get
        {
            if (leaveDialogue == 1)
            {
                return true;
            }
            return false;
        }

    }

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
