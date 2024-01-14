using Newtonsoft.Json;

namespace BoneGame.System
{
    /// <summary>
    /// SaveDataのベースクラス
    /// 各ゲームでこれを実装してそれぞれのセーブデータクラスを作る
    /// セーブ・ロード機構はこのクラスを参照する
    /// </summary>
    [JsonObject]
    public abstract class SaveDataBase
    {
        public int SaveId;
    }
}