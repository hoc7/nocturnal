
using System;
using UniRx;
using UnityEngine;
using ObservableExtensions = UniRx.ObservableExtensions;

namespace BoneGame.Message
{
    public static class Messenger
    {
        /// <summary>
        /// 指定したオブジェクトを送信
        /// </summary>
        /// <typeparam name="T">送信するメッセージオブジェクトの型</typeparam>
        /// <param name="messageObject">メッセージオブジェクト</param>
        public static void Publish<T>(T messageObject)
            where T : class
        {
            MessageBroker.Default.Publish<T>(messageObject);
        }
        

        /// <summary>
        /// メッセージの受け取り処理のIDisposableインターフェースを受け取る
        /// </summary>
        /// <typeparam name="T">受信するメッセージオブジェクトの型</typeparam>
        /// <param name="onNext">メッセージ受信時の実行処理</param>
        /// <returns>IDisposableインターフェース</returns>
        public static  IDisposable Subscribe<T>(Action<T> onNext, object caller = null)
            where T : class
        {
            IDisposable disposable = ObservableExtensions.Subscribe(Receive<T>(), onNext);

            // ゲームオブジェクト(View)から呼ばれたときは自動破棄設定を行う
            if (caller != null && caller.GetType() == typeof(GameObject))
            {
                disposable.AddTo((GameObject)caller);

            }

            return disposable;
        }

        /// <summary>
        /// メッセージ受け取り処理のIObservableインターフェースを受け取る
        /// </summary>
        /// <typeparam name="T">受信するメッセージオブジェクトの型</typeparam>
        /// <returns>IObservableインターフェース</returns>
        public static IObservable<T> Receive<T>()
            where T : class
        {
            return MessageBroker.Default.Receive<T>();
        }


    }
}