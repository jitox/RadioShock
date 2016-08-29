using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

public static class CMapParser
{


    public static int[][] readMap(string aFile)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(aFile);
        List<string> cosoNodes = new List<string>();
        
        foreach (XmlNode node in doc.DocumentElement.ChildNodes)
        {
            string text = node.InnerText; //or loop through its children as well
            cosoNodes.Add(text);
        }
        string map = cosoNodes[1];
        map = map.Trim();
        string[] tokens = map.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        
        
        List<List<int>> intTileMap = new List<List<int>>();
        for (int i = 0; i < tokens.Length; i++)
        {
            List<int> lineinInt = new List<int>();
            string[] line = tokens[i].Split(',');
            for (int j = 0; j < line.Length; j++)
            {
                int n;
                bool isNumeric = int.TryParse(line[j], out n);
                if(isNumeric)
                    lineinInt.Add(int.Parse(line[j]));
                
            }
            intTileMap.Add(lineinInt);
           
        }
      return intTileMap.Select(a => a.ToArray()).ToArray();
    }
}

