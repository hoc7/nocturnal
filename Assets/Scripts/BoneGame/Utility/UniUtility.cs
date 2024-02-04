using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BoneGame.System
{
    public static class UniUtility
    {
        /// <summary>
        /// RectTransform（Canvas-Screen Space Camera）をWorldSpaceに変換する
        /// </summary>
        /// <param name="mainCamera"></param>
        /// <param name="rect"></param>
        /// <param name="canvas"></param>
        /// <returns></returns>
        public static Vector3 GetWorldPositionFromRectPosition(Camera mainCamera, RectTransform rect, Canvas canvas)
        {
            Vector3 vector = Vector3.zero;

            RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, rect.position, canvas.worldCamera,
                out vector);

            return vector;
        }

        /// <summary>
        /// WorldSpaceをRectTransform（Canvas-Screen Space Camera）に変換する
        /// </summary>
        /// <param name="mainCamera">WorldSpaceのカメラ</param>
        /// <param name="pos">変換したい座標</param>
        /// <param name="canvas">変換先のCanvas</param>
        /// <param name="uiCamera">変換先のCanvasを移すCamera</param>
        /// <returns></returns>
        public static Vector2 GetRectPositionFromWorldPosition(Camera mainCamera, Vector3 pos, Canvas canvas)
        {
            // WorldSpaceをRectTransform（Overlay）に変換
            Vector2 vector = RectTransformUtility.WorldToScreenPoint(mainCamera, pos);

            Vector2 rectPos = Vector2.zero;

            // RectTransform（Overlay）⇒ RectTransform（Canvas-Screen Space Camera）に変換
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), vector,
                canvas.worldCamera, out rectPos);

            return rectPos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainCamera"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static Vector3 GetScreenPositionFromWorldPosition(Camera mainCamera, Vector3 pos)
        {
            Vector3 vector3 = mainCamera.ScreenToWorldPoint(pos);
            return vector3;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainCamera"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static Vector3 GetWorldPositionFromScreenPosition(Camera mainCamera, Vector3 pos)
        {
            Vector3 vector3 = RectTransformUtility.WorldToScreenPoint(mainCamera, pos);
            return vector3;
        }


        //--------------------------------------------------------------------------------
        // リスト内のすべての要素を削除する
        //--------------------------------------------------------------------------------
        public static void AllDestroy<T>(this List<T> list)
            where T : UnityEngine.Object
        {
            for (int i = list.Count - 1; i > -1; i--)
            {
                if (list[i] is GameObject obj)
                {
                    UnityEngine.Object.Destroy(obj);
                }
            }
        }

        //--------------------------------------------------------------------------------
        // Listから要素をランダムで1つ取得する
        //--------------------------------------------------------------------------------
        public static T GetRandom<T>(this List<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }


        //--------------------------------------------------------------------------------
        // コンポーネントからシーン名を取得する
        //--------------------------------------------------------------------------------
        public static string GetSceneName(this Component component)
        {
            return component.gameObject.scene.name;
        }


        //--------------------------------------------------------------------------------
        // 指定された列挙型の数を返却
        //--------------------------------------------------------------------------------
        /// <summary>
        /// 指定された列挙型の値の数返します
        /// </summary>
        public static int GetEnumLength<T>()
        {
            return Enum.GetValues(typeof(T)).Length;
        }

        /// <summary>
        /// ランダムな値を引いてprobより低い値が出たらTrueを返す
        /// </summary>
        /// <param name="prob">確率</param>
        /// <returns></returns>
        public static bool DiceRoll(int prob)
        {
            int dice = Random.Range(0, 100);
            return dice < prob;
        }
        
    }

    public static class CharacterTurner
    {
        private static readonly List<string> Ordering =
            new List<string> {
                "あ", "い", "う", "え", "お",
                "か", "き", "く", "け", "こ",
                "さ", "し", "す", "せ", "そ",
                "た", "ち", "つ", "て", "と",
                "な", "に", "ぬ", "ね", "の",
                "は", "ひ", "ふ", "へ", "ほ",
                "ま", "み", "む", "め", "も",
                "や",       "ゆ",       "よ",
                "ら", "り", "る", "れ", "ろ",
                "わ",                   "を",
                "ん", 
            };

        public static int GetIndex(List<string> pages, string initial)
        {
            pages = pages.Select(_ => KatakanaToHiragana(_)).ToList();
            var searchOrder = Ordering.SkipWhile(x => x != initial).ToList();
            var result = pages
                .FirstOrDefault(page => searchOrder.Any(page.StartsWith));
            return pages.IndexOf(result);
        }
        
        static string KatakanaToHiragana(string input)
        {
            char[] result = new char[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                char originalChar = input[i];
                // カタカナのUnicode範囲チェック
                if (originalChar >= '\u30A0' && originalChar <= '\u30FF')
                {
                    // カタカナからひらがなに変換（差分は96）
                    char convertedChar = (char)(originalChar - 96);
                    result[i] = convertedChar;
                }
                else
                {
                    // カタカナ以外の文字はそのまま
                    result[i] = originalChar;
                }
            }
            return new string(result);
        }
    }
    

    
    public class JapaneseStringComparer : IComparer<string>
    {
        private CompareInfo compareInfo = CultureInfo.GetCultureInfo("ja-JP").CompareInfo;

        // 日本語のカルチャを使用してCompareInfoを取得

        public int Compare(string x, string y)
        {
            // 日本語の五十音順に基づいて比較
            return compareInfo.Compare(x, y, CompareOptions.StringSort);
        }
    }

    public class RemovalStack<T>
    {
        private List<T> items = new List<T>();

        public void Push(T item)
        {
            items.Add(item);
        }

        public T Pop()
        {
            if (items.Count > 0)
            {
                T temp = items[items.Count - 1];
                items.RemoveAt(items.Count - 1);
                return temp;
            }
            else
                return default(T);
        }

        public List<T> ToList()
        {
            return items;
        }

        public int Count => items.Count;

        public bool Any()
        {
            return items.Count != 0;
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        public void Remove(int itemAtPosition)
        {
            items.RemoveAt(itemAtPosition);
        }
    }
}