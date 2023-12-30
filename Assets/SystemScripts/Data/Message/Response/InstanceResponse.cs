using System;
using Apoptosis.Asmodeus.Message.Request;

namespace Apoptosis.Asmodeus.Message.Response
{
    /// <summary>
    /// 他のクラスからのインスタンス生成要求結果を処理して返却するためのメッセージオブジェクトクラス
    /// </summary>
    public class InstanceResponse
    {
        private object Requester { get; set; } = null;

        private object RequestObject { get; set; } = null;


        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="request">リクエストメッセージ</param>
        /// <param name="result">リクエストの結果生成されたオブジェクト</param>
        public InstanceResponse(InstanceRequest request, object result)
        {
            this.Requester = request.Requester;
            this.RequestObject = result;
        }

        /// <summary>
        /// 自分がリクエストしたオブジェクトかどうか
        /// </summary>
        /// <param name="obj">呼び出し元インスタンス</param>
        /// <returns>自分のリクエストに対しての返信かどうか</returns>
        public bool IsRequest(object me)
        {
            return me == Requester;
        }

        /// <summary>
        /// リクエストオブジェクトを取得する。自分がリクエストしたものでなければnullが返却される
        /// </summary>
        /// <param name="me">呼び出し元インスタンス</param>
        /// <returns>リクエストしたオブジェクト</returns>
        public Object GetRequestObject(object me)
        {
            if(IsRequest(me))
            {
                return RequestObject;
            }

            return null;
        }
    }
}
