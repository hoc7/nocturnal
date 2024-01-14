using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;


namespace BoneGame.System
{
    /// <summary>
    /// セーブの管理をするクラス
    /// </summary>
    public static class SaveManager
    {
        private static List<int> NowSaveIds = new List<int>();
        public const int AutoSaveId = 0;
        private const string SaveIdKey = "SaveIds";
        private const string MemoryLabelKey = "MemoryLabel";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Init()
        {
            InitSaveList();
        }

        /// <summary>
        /// オートセーブする
        /// </summary>
        public static void AutoSave(SaveDataBase saveDataBase)
        {
            Save(AutoSaveId, saveDataBase);
        }

        /// <summary>
        /// セーブを実行する　同じセーブIDだったら上書き
        /// </summary>
        /// <param name="saveId"></param>
        public static void Save(int saveId, SaveDataBase saveDataBase)
        {
            string saveKey = GetSaveKey(saveId);
            if (!NowSaveIds.Contains(saveId))
            {
                NowSaveIds.Add(saveId);
            }

            string json = JsonConvert.SerializeObject(saveDataBase);
            ES3.Save<string>(saveKey, json);
            ES3.Save<List<int>>(SaveIdKey, NowSaveIds);
        }

        public static void SaveMemoryLabel(string memoryLabel)
        {
            List<string> labels = LoadLabels();
            if (!labels.Contains(memoryLabel))
            {
                labels.Add(memoryLabel);
            }

            ES3.Save(MemoryLabelKey,labels);

        }

        public static List<string> LoadLabels()
        {
            if (ES3.KeyExists(MemoryLabelKey))
            {
                List<string> labels = ES3.Load<List<string>>(MemoryLabelKey);
                return labels;
            }
            else
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// 特定のIDのセーブデータをロードする
        /// </summary>
        /// <param name="loadSaveId"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Load<T>(int loadSaveId) where T : SaveDataBase
        {
            if (ES3.KeyExists(GetSaveKey(loadSaveId)))
            {
                string json = ES3.Load<string>(GetSaveKey(loadSaveId));
                T saveData = JsonConvert.DeserializeObject<T>(json);
                return saveData;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 全てのセーブデータを取得する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetAllSaveData<T>() where T : SaveDataBase
        {
            List<T> saveDataBases = new List<T>();
            foreach (var key in NowSaveIds)
            {
                saveDataBases.Add(Load<T>(key));
            }

            return saveDataBases;
        }

        /// <summary>
        /// 最新未使用のSaveIdを取得する
        /// </summary>
        /// <returns></returns>
        public static int GetLastSaveId()
        {
            if (NowSaveIds.Count == 0) return 1;
            return NowSaveIds.OrderByDescending(_ => _).First() + 1;
        }

        /// <summary>
        /// SaveListを初期化する
        /// </summary>
        private static void InitSaveList()
        {
            if (ES3.KeyExists(SaveIdKey))
            {
                NowSaveIds = ES3.Load<List<int>>(SaveIdKey);
            }

            if (NowSaveIds == null) NowSaveIds = new List<int>();
        }

        public static void DeleteData(int saveId)
        {
            ES3.DeleteKey(GetSaveKey(saveId));
            if (NowSaveIds.Contains(saveId))
            {
                NowSaveIds.Remove(saveId);
            }

            ES3.Save<List<int>>(SaveIdKey, NowSaveIds);
        }
        
        public static bool ExistScenarioLabelSaveData()
        {
            if (ES3.KeyExists(MemoryLabelKey))
            {
                if (LoadLabels().Any())
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ExistSaveData()
        {
            string autoSaveKey = GetSaveKey(AutoSaveId);
            if (ES3.KeyExists(autoSaveKey)) return true;

            if (ES3.KeyExists(SaveIdKey)) return true;
            return false;
        }

        /// <summary>
        /// Saveデータ用のキーを取得する
        /// </summary>
        /// <param name="saveId"></param>
        /// <returns></returns>
        private static string GetSaveKey(int saveId)
        {
            return "save_" + saveId;
        }
    }
}