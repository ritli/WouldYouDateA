using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;


public class Dialogue {

    [XmlAttribute("mood")]
    public int mood;
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

    [XmlElement("Event")]
    public string events;

    [XmlAttribute("continue")]
    public int continueDialogue;

    [XmlAttribute("leave")]
    public int leaveDialogue;

    public string Event
    {
        get
        {
            if (events == null || events == "")
            {
                return "NoEvent";
            }

            return events;
        }
    }

    public Mood Mood
    {
        get
        {
            return (Mood)mood;
        }
    }

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
