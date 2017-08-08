using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;
using UnityEngine;

public class testReader
{
    public void readXML()
    {
        Debug.Log(Directory.GetCurrentDirectory() + "/Assets/XML/charDialogues.xml");
        XmlTextReader reader = new XmlTextReader(Directory.GetCurrentDirectory() + "/Assets/XML/charDialogues.xml");

        List<BaseConvoNode> result;

        while (reader.Read())
        {

            if (reader.Name == "root")
            {
                result = new List<BaseConvoNode>();
                // Start of xml file. 
                // Loop through until end of root.
                while (!(reader.NodeType == XmlNodeType.EndElement && reader.Name == "root"))
                {
                    reader.Read();

                    if (reader.Name == "baseNode")
                    {
                        // Convo variables
                        int tmpReqIntim = 0;
                        string tmpText = "";
                        string[] tmpChoices = null;
                        int[] tmpCorrect = null;
                        int[] tmpNeutral = null;
                        int[] tmpNegative = null;
                        ConvoNode[] tmpFollowupConvos = new ConvoNode[3];
                        tmpFollowupConvos[0] = null;
                        tmpFollowupConvos[1] = null;
                        tmpFollowupConvos[2] = null;


                        reader.Read();

                        // Found new baseNode.
                        // Loop until end of conversation.
                        while (!(reader.NodeType == XmlNodeType.EndElement && reader.Name == "baseNode"))
                        {
                            reader.Read();
                            // Load conversation start.
                            if (reader.Name == "reqIntimacy")
                                tmpReqIntim = readInt(ref reader, "reqIntimacy");
                            else if (reader.Name == "voiceLine")
                                tmpText = readString(ref reader, "voiceLine");
                            // Load all choices
                            else if (reader.Name == "choices")
                                tmpChoices = readStringArr(ref reader, "choices");
                            else if (reader.Name == "correct")
                                tmpCorrect = readIntArr(ref reader, "correct");
                            else if (reader.Name == "neutral")
                                tmpNeutral = readIntArr(ref reader, "neutral");
                            else if (reader.Name == "negative")
                                tmpNegative = readIntArr(ref reader, "negative");
                                // Load responses, if any
                            else if (reader.Name == "posRes")
                                tmpFollowupConvos[0] = getSubNodes(ref reader, "posRes");
                            else if (reader.Name == "netRes")
                                tmpFollowupConvos[1] = getSubNodes(ref reader, "netRes");
                            else if (reader.Name == "negRes")
                                tmpFollowupConvos[2] = getSubNodes(ref reader, "negRes");
                        }

                        result.Add(new BaseConvoNode(tmpReqIntim, tmpText, tmpChoices, tmpCorrect, tmpNeutral, tmpNegative, 
                            tmpFollowupConvos[0], tmpFollowupConvos[1], tmpFollowupConvos[2], false));
                    }
                }
            }
        }
    }

    private ConvoNode getSubNodes( ref XmlTextReader reader, string elementTag)
    {
        // Convo variables
        string tmpText = "";
        string[] tmpChoices = null;
        int[] tmpCorrect = null;
        int[] tmpNeutral = null;
        int[] tmpNegative = null;
        ConvoNode[] tmpFollowupConvos = new ConvoNode[3];
        tmpFollowupConvos[0] = null;
        tmpFollowupConvos[1] = null;
        tmpFollowupConvos[2] = null;

        while (!(reader.NodeType == XmlNodeType.EndElement && reader.Name == elementTag))
        {
            reader.Read();
            Debug.Log("SubNode");

            switch (reader.Name)
            {
                case "voiceLine":
                    tmpText = readString(ref reader, "voiceLine");
                    break;
                case "choices":
                    tmpChoices = readStringArr(ref reader, "choices");
                    break;
                case "correct":
                    tmpCorrect = readIntArr(ref reader, "correct");
                    break;
                case "neutral":
                    tmpNeutral = readIntArr(ref reader, "neutral");
                    break;
                case "negative":
                    tmpNegative = readIntArr(ref reader, "negative");
                    break;
                case "posRes":
                    tmpFollowupConvos[0] = getSubNodes(ref reader, "posRes");
                    break;
                case "netRes":
                    tmpFollowupConvos[1] = getSubNodes(ref reader, "netRes");
                    break;
                case "negRes":
                    tmpFollowupConvos[2] = getSubNodes(ref reader, "negRes");
                    break;

                default:
                    break;
            }

            #region oldElseIf
            /*
            if (reader.Name == "voiceLine")

            else if (reader.Name == "choices")
            else if (reader.Name == "correct")
                tmpCorrect = readIntArr(ref reader, "correct");
            else if (reader.Name == "neutral")
                tmpNeutral = readIntArr(ref reader, "neutral");
            else if (reader.Name == "negative")
                tmpNegative = readIntArr(ref reader, "negative");
            else if (reader.Name == "posRes")
                tmpFollowupConvos[0] = getSubNodes(ref reader, "posRes");
            else if (reader.Name == "netRes")
                tmpFollowupConvos[1] = getSubNodes(ref reader, "netRes");
            else if (reader.Name == "negRes")
                tmpFollowupConvos[2] = getSubNodes(ref reader, "negRes");
        */
            #endregion
        }

        return new ConvoNode(tmpText, tmpChoices, tmpCorrect, tmpNeutral, tmpNegative, tmpFollowupConvos[0], tmpFollowupConvos[1], tmpFollowupConvos[2]);
    }

    private string[] readStringArr(ref XmlTextReader reader, string elementTag)
    {
        List<string> strList = new List<string>();

        while (!(reader.NodeType == XmlNodeType.EndElement && reader.Name == elementTag))
        {
            while (reader.NodeType != XmlNodeType.Text)
                reader.Read();
            strList.Add(reader.Value);

            while (reader.NodeType != XmlNodeType.Text || reader.NodeType != XmlNodeType.EndElement)
                reader.Read();
        }

        return strList.ToArray();
    }

    private int[] readIntArr(ref XmlTextReader reader, string elementTag)
    {
        //Debug.Log(reader.Name);

        while (reader.NodeType != XmlNodeType.Text)
        {
            reader.Read();
        }

        //Debug.Log(reader.Value);
        
        string[] value = reader.Value.Split(',');
        int[] intArr = new int[value.Length];

        for (int i = 0; i < value.Length; i++)
        {
            Debug.Log(value[i]);
            intArr[i] = int.Parse(value[i]);
        }

        /*
        while (reader.NodeType != XmlNodeType.EndElement)
        {
            Debug.Log("Stuck?");
            reader.Read();
            if (reader.Name == "root")
                break;
        }*/
        
        return intArr;
    }

    private string readString(ref XmlTextReader reader, string elementTag)
    {
        while (reader.NodeType != XmlNodeType.Text)
            reader.Read();
        string result = reader.Value;
        while (reader.NodeType != XmlNodeType.EndElement && reader.Name == elementTag)
            reader.Read();
        return result;
    }

    private int readInt(ref XmlTextReader reader, string elementTag)
    {
        while (reader.NodeType != XmlNodeType.Text)
            reader.Read();
        int result = int.Parse(reader.Value);
        while (reader.NodeType != XmlNodeType.EndElement && reader.Name == elementTag)
            reader.Read();
        return result;
    }
}
