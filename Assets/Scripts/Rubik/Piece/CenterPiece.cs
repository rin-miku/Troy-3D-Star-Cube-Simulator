using System.Collections.Generic;
using UnityEngine;

public class CenterPiece : PieceBase
{
    
    public List<PieceBase> surfacePieces;

    public override void InitPiece()
    {
        // 中心块给予 MeshCollider 组件
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform parent = transform.GetChild(i);
            for (int j = 0; j < parent.childCount; j++)
            {
                parent.GetChild(j).gameObject.AddComponent<MeshCollider>().convex = true;
            }
        }
    }
}
