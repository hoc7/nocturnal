namespace Apoptosis.Asmodeus.Message.Request
{
    /// <summary>
    /// 他のクラスにインスタンス生成を依頼するためのメッセージオブジェクトクラス
    /// </summary>
    public class InstanceRequest
    {
        /// <summary>
        /// インスタンスを要求するクラスの完全パス
        /// </summary>
        public string RequestClassName { get; private set; } = null;

        public object[] ConstractorArgs { get; private set; } = null;

        /// <summary>
        /// リクエストを投げたクラスのインスタンス
        /// </summary>
        public object Requester { get; private set; } = null;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="requestClaeeName"></param>
        /// <param name="requester"></param>
        public InstanceRequest(string requestClaeeName, object requester,  params object[] args)
        {
            this.RequestClassName = requestClaeeName;
            this.Requester = requester;
            this.ConstractorArgs = args;
        }
    }
}
