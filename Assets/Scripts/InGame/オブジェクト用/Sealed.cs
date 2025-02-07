using System.IO;
using UnityEngine;

public class Sealed : MonoBehaviour
{
    public string Seal = "<< 5Lq66ZaT44Gu55yf5a6fOmltcHV0YXRpb24NCuelnuanmOOBq+aVmeOBiOOBquOBhOOBp+OBrT8= >>";

    public void GenerateHint()
    {
        // ファイルを保存するパス
        string filePath = Path.Combine(Application.persistentDataPath, "封書.txt");

        // テキストファイルにメッセージを書き込み
        File.WriteAllText(filePath, Seal);
        

        Debug.Log("Seal generated and saved: " + filePath);
    }
}
