using System;
using System.Collections.Generic;
using System.Linq;
using BoneGame.Data;
using BoneGame.Message;
using BoneGame.Nocturnal.Data;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace BoneGame.Nocturnal.Planetarium
{
    public class ViewObjectDetector : MonoBehaviour
    {
        [SerializeField] private Button Button;
        [SerializeField] private Camera MainCamera;

        private void Start()
        {
            Button.OnClickAsObservable().Subscribe(_ =>
            {
                CheckObjectsInViewport();
            }).AddTo(this);
        }

        public void CheckObjectsInViewport()
        {
            // ビューポート内にあるすべてのColliderを取得
            Collider[] hitColliders = Physics.OverlapSphere(MainCamera.transform.position, MainCamera.farClipPlane);

            List<int> hitStars = new List<int>();
            foreach (var hitCollider in hitColliders)
            {
                // オブジェクトがカメラの視野内にあるかを確認
                Vector3 viewportPoint = MainCamera.WorldToViewportPoint(hitCollider.transform.position);
                if (viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1)
                {
                    GameObject hitObject = hitCollider.gameObject;
                    StarView view = hitObject.GetComponent<StarView>();
                    StarData data = MasterDataHolder.Instance.GetStar(view.HitId);
                    hitStars.Add(data.HitId);
                    // オブジェクトが視野内にある場合、ここにロジックを書く
                    Debug.Log(data.HitId);
                }
            }

            List<SignData> signs  = MasterDataHolder.Instance.GetContainSigns(hitStars);

            foreach (SignData sign in signs)
            {
                Debug.Log(sign.JapName);
            }

            PurposeCheckMessage message = new PurposeCheckMessage(signs.Select(_ => _.SignId).ToList(), hitStars);
            Messenger.Publish(message);
        }
    }
}