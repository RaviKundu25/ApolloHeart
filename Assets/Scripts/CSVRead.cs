using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class CSVRead : MonoBehaviour
{
    public List<Text> valuesToShow;

    // Start is called before the first frame update
    void Start()
    {
        NativeGallery.RequestPermission(NativeGallery.PermissionType.Read);
        NativeGallery.RequestPermission(NativeGallery.PermissionType.Write);

        string path = "/storage/emulated/0/Download/";
        try
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            FileInfo[] files = directoryInfo.GetFiles();
            double largestNum = 0;
            for (int i = 0; i < directoryInfo.GetFiles().Length; i++)
            {
                if(files[i].Name.Split("."[0])[1] == "csv" && files[i].Name.Split("_"[0])[0] == "Vitals")
                {
                    if(largestNum < double.Parse(files[i].Name.Split("."[0])[0].Split("_"[0])[1]))
                    {
                        largestNum = double.Parse(files[i].Name.Split("."[0])[0].Split("_"[0])[1]);
                    }
                }
            }
            path += "Vitals_" + largestNum.ToString() + ".csv";

            //Debug.Log(path);

            StreamReader streamReader = new StreamReader(path);
            bool endOfLine = false;
            List<string> lines = new List<string>();
            while (!endOfLine)
            {
                string data_string = streamReader.ReadLine();
                if(data_string == null)
                {
                    endOfLine = true;
                    break;
                }
                if(data_string.Length > 1)
                {
                    lines.Add(data_string);
                }
            }

            if(lines[lines.Count-1].Split(","[0])[6] != "")
            {
                valuesToShow[0].text = lines[lines.Count - 1].Split(","[0])[6];
            }

            if (lines[lines.Count - 1].Split(","[0])[7] != "")
            {
                valuesToShow[1].text = lines[lines.Count - 1].Split(","[0])[7];
            }

            if (lines[lines.Count - 1].Split(","[0])[8] != "")
            {
                valuesToShow[2].text = lines[lines.Count - 1].Split(","[0])[8];
            }

            if (lines[lines.Count - 1].Split(","[0])[22] != "")
            {
                valuesToShow[3].text = lines[lines.Count - 1].Split(","[0])[22];
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }
}
