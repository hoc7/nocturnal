using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BoneGame.Nocturnal.Planetarium
{
    public static class HipDataFactory
    {
        public static List<HipLine> CreateHipLines(TextAsset csv)
        {
            List<HipLine> lines = new List<HipLine>();
            StringReader sr = new StringReader(csv.text);

            while (true)
            {
                string line = sr.ReadLine();
                if (line == null)
                {
                    break;
                }
                else
                {
                    HipLine hipData = stringToHipLine(line);
                    lines.Add(hipData);
                }
            }

            return lines;
        }

        public static HipLine stringToHipLine(string _hipStr)
        {
            HipLine data = new HipLine();
            // カンマ区切りのデータを文字列の配列に変換
            string[] dataArr = _hipStr.Split(',');
            try
            {
                // 文字列をint,floatに変換する
                string name = dataArr[0];
                int beforehipId = int.Parse(dataArr[1]);
                int afterHipId = int.Parse(dataArr[2]);
                data = new HipLine(name, beforehipId, afterHipId);
            }
            catch
            {
                Debug.Log("data err");
            }
            return data;
        }
        
        public static List<StarData> CreateHipDatas(TextAsset csv)
        {
            List<StarData> hipDatas = new List<StarData>();
            StringReader sr = new StringReader(csv.text);

            while (true)
            {
                string line = sr.ReadLine();
                if (line == null)
                {
                    break;
                }
                else
                {
                    StarData starData = stringToHipData(line);
                    hipDatas.Add(starData);
                }
            }

            return hipDatas;
        }
        
        private static StarData stringToHipData(string _hipStr)
        {
            StarData data = new StarData();
            // カンマ区切りのデータを文字列の配列に変換
            string[] dataArr = _hipStr.Split(',');
            try
            {
                // 文字列をint,floatに変換する
                int hipId = int.Parse(dataArr[0]);
                float hlH = float.Parse(dataArr[1]);
                float hlM = float.Parse(dataArr[2]);
                float hlS = float.Parse(dataArr[3]);
                int hsSgn = int.Parse(dataArr[4]);
                float hsH = float.Parse(dataArr[5]);
                float hsM = float.Parse(dataArr[6]);
                float hsS = float.Parse(dataArr[7]);
                float mag = float.Parse(dataArr[8]);
                Color col = Color.gray;
                float hDeg = (360f / 24f) * (hlH + hlM / 60f + hlS / 3600f);
                float sDeg = (hsH + hsM / 60f + hsS / 3600f) * (hsSgn == 0 ? -1f : 1f);
                Quaternion rotL = Quaternion.AngleAxis(hDeg, Vector3.up);
                Quaternion rotS = Quaternion.AngleAxis(sDeg, Vector3.right);
                Vector3 pos = rotL * rotS * Vector3.forward;
                data = new StarData(hipId, pos, Color.white, mag);
            }
            catch
            {
                Debug.Log("data err");
            }
            return data;
        }
    }
}