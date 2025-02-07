using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SECRETCOMMAND : MonoBehaviour
{   
    private string[] SecretCommand = {"I", "M", "P", "U", "T", "A", "T", "I", "O", "N"};
    private int currentIndex = 0;
    [SerializeField] private UnityEvent happen;

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(!other.CompareTag("Human")){
            return;
        }
        
        //Debug.Log(currentIndex);
        if (Input.GetKeyDown(KeyCode.I) && CheckCommand("I"))
        {
            //正しいコマンドなら次へ 
            currentIndex++;
        }
        else if (Input.GetKeyDown(KeyCode.M) && CheckCommand("M"))
        {
            //正しいコマンドなら次へ 
            currentIndex++;
        }
        else if (Input.GetKeyDown(KeyCode.P) && CheckCommand("P"))
        {
            //正しいコマンドなら次へ 
            currentIndex++;
        }
        else if (Input.GetKeyDown(KeyCode.U) && CheckCommand("U"))
        {
            //正しいコマンドなら次へ 
            currentIndex++;
        }
        else if (Input.GetKeyDown(KeyCode.T) && CheckCommand("T"))
        {
            //正しいコマンドなら次へ 
            currentIndex++;
        }
        else if (Input.GetKeyDown(KeyCode.A) && CheckCommand("A"))
        {
            //正しいコマンドなら次へ 
            currentIndex++;
        }
        else if (Input.GetKeyDown(KeyCode.O) && CheckCommand("O"))
        {
            //正しいコマンドなら次へ 
            currentIndex++;
        }
        else if (Input.GetKeyDown(KeyCode.N) && CheckCommand("N"))
        {
            //正しいコマンドなら次へ 
            currentIndex++;
        }
        else if(Input.anyKeyDown)
        {
            Debug.Log("でたよ～～");
            // 他のキーが押された場合、コマンドをリセット
            currentIndex = 0;
        }

        // コマンドが完了した場合の処理
        if (currentIndex == SecretCommand.Length && !GloValues.GoSecret)
        {
            Debug.Log("Konami Code Entered!");
            happen.Invoke();
            GloValues.GoSecret = true;
            currentIndex = 0; // コマンドをリセット
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        Debug.Log("トリガーでたよ～～");
        if(other.CompareTag("Human")){
            currentIndex = 0;
        }
    }

    // 入力されたキーがコマンドと一致するかチェック
    private bool CheckCommand(string keyName)
    {
        return keyName == SecretCommand[currentIndex];
    }
}
