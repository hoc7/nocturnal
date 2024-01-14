using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BoneGame.Nocturnal.Planetarium
{
    public class StarLineDrawer : MonoBehaviour
    {
        [SerializeField] private MeshRenderer MeshRenderer;
        [SerializeField] private MeshFilter MeshFilter;
        /// 直線の描画
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="isVertical"></param>
        public void SetVertices(Vector3 start, Vector3 end, float MeshWidth, int sortingOrder)
        {
            Mesh mesh = new Mesh();
            float widthDiff = 0f;
            float heightDiff = 0f;

            widthDiff = MeshWidth / 2f;


            Vector3 StartLeft = new Vector3(start.x - widthDiff, start.y - heightDiff, start.z);
            Vector3 StartRight = new Vector3(start.x + widthDiff, start.y + heightDiff, start.z);
            Vector3 EndLeft = new Vector3(end.x - widthDiff, end.y - heightDiff, end.z);
            Vector3 EndRight = new Vector3(end.x + widthDiff, end.y + heightDiff, end.z);

            List<Vector3> InputVertices = new List<Vector3>();
            InputVertices.Add(StartLeft);
            InputVertices.Add(StartRight);
            InputVertices.Add(EndLeft);
            InputVertices.Add(EndRight);

            // 頂点を左上→右上→左下→右上の順に並び替えたリストを作る
            InputVertices = InputVertices.OrderByDescending(_ => _.y).ThenBy(_ => _.x).ToList();


            Vector3[] vertices = new Vector3[4];
            int[] triangles = new int[6];

            // 頂点座標の設定　並び替えリストを元に絶対に表面がカメラを向くように座標配置
            vertices[0] = InputVertices[0];
            vertices[1] = InputVertices[1];
            vertices[2] = InputVertices[3];
            vertices[3] = InputVertices[2];

            // 描画順の設定
            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;
            triangles[3] = 2;
            triangles[4] = 3;
            triangles[5] = 0;

            mesh.SetVertices(vertices);
            mesh.triangles = triangles;

            MeshFilter.mesh = mesh;

            MeshRenderer.sortingOrder = sortingOrder;
        }
        }
}