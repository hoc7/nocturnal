using UnityEngine;

namespace BoneGame.Nocturnal.Planetarium
{
    public class StarNameRayCaster : MonoBehaviour
    {
        void Update() {
            // カメラの中心点からレイを飛ばす場合
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0));
            RaycastHit hit;
            
            float maxDistance = 100f;

            // デバッグ用の線を描画（エディタのみで表示される）
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * maxDistance, Color.white,10f);
            Debug.DrawRay(ray.origin,ray.origin+ray.direction * maxDistance,Color.yellow,10f);


            if (Physics.Raycast(ray, out hit,maxDistance)) {
                // ヒットしたオブジェクトの名前を取得
                Debug.Log("Hit object name: " + hit.collider.gameObject.name);
            }
        }
    }
}