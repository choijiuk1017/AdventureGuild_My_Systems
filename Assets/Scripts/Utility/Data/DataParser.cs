using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utility.Data
{
    public class DataParser : MonoBehaviour
    {
        private static Dictionary<string, TextAsset> dataDic;
    
        [SerializeField] private TextAsset[] database;

        private void Awake()
        {
            dataDic = new Dictionary<string, TextAsset>();

            database = Resources.LoadAll<TextAsset>("DataTable");

            foreach (var data in database)
            {
                dataDic.Add(data.name, data);
            }
        }

        public static List<Dictionary<string, object>> Parser(string dataName)
        {
            var list = new List<Dictionary<string, object>>();

            var data = dataDic[dataName];
            
            StringReader reader = new StringReader(data.text);
            string text = reader.ReadLine();

            string[] row = text.Split(',');
            text = reader.ReadLine();

            while (text != null)
            {
                var newDic = new Dictionary<string, object>();
                string[] rowData = text.Split(',');
                for (int i = 0; i < rowData.Length; i++)
                {
                    newDic.Add(row[i], rowData[i]);
                    //Debug.Log(row[i]);
                }

                list.Add(newDic);

                text = reader.ReadLine();
            }

            return list;
        }
        
        public static int IntParse(object data)
        {
            return int.Parse(data.ToString());
        }

        public static float FloatParse(object data)
        {
            return float.Parse(data.ToString());
        }
    
        public static Enum EnumParse<T>(object data) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), data.ToString());
        }
    }
}