using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BoneGame.Event
{
    /// <summary>
    /// プレイしたイベントを保存するクラス
    /// </summary>
    [JsonObject]
    public class EventLog
    {
        private List<int> _playedEvent;
        
        public EventLog()
        {
            _playedEvent = new List<int>();
        }
        
        /// <summary>
        /// イベントの取得
        /// </summary>
        /// <param name="eventId"></param>
        public void SetEvent(int eventId)
        {
            _playedEvent.Add(eventId);
        }

        /// <summary>
        /// イベントをクリアしているかどうかの取得
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public bool IsClearEvent(int eventId)
        {
            return _playedEvent.Contains(eventId);
        }
    }
}