using System;
using TMPro;
using UnityEngine;
using Utage;

namespace BoneGame.Nocturnal.Planetarium
{
    /// <summary>
    /// 時間の描画クラス
    /// </summary>
    public class TimeDrawer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI TimeText;

        public void DrawTime(DateTime dateTime)
        {   
            // Convert dateTime to string
            string timeString = dateTime.ToString("hh時mm分");

            // Set the text of the TextMeshProUGUI component
            TimeText.text = timeString;
        }
        
    }
}