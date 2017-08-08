using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("root")]
public class DialogueContainer {

    public string characterName;
    [XmlArray("Dialogues")]
    [XmlArrayItem("Dialogue")]
    public List<Dialogue> m_dialogues = new List<Dialogue>();

    public static DialogueContainer Load(Characters characterType)
    {
        TextAsset xmlFile = Resources.Load<TextAsset>(characterType.ToString());
        XmlSerializer serializer = new XmlSerializer(typeof(DialogueContainer));
        StringReader reader = new StringReader(xmlFile.text);
        DialogueContainer dialogues = serializer.Deserialize(reader) as DialogueContainer;

        return dialogues;
    }
}


