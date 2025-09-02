using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace RPG.NewSaving
{
    public class NewSavingSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Saving to " + path);

            //TODO:test write some data
            //stream.WriteByte(102);
            //using陳述式:用於確保在某個程式區塊結束後，其管理的資源（如檔案流、資料庫連線等）能夠被正確地釋放. 它通常用來簡化 try-finally 區塊，但 不包含 catch 的錯誤處理部分. using 陳述式本質上是一個編譯器語法糖(syntax sugar)，它會被編譯成一個 try-finally 區塊.
            using (FileStream stream = File.Open(path, FileMode.Create)) //FileMode.Create  創造一個新文件(return FileStream)，如果有了就覆蓋
            {
                byte[] bytes = Encoding.UTF8.GetBytes("Hello Word!!!");
                stream.Write(bytes, 0, bytes.Length);
            }
           
            //stream.Close();
        }

        public void Load(string saveFile)
        {
            print("Loading from " + GetPathFromSaveFile(saveFile));

        }

        private string GetPathFromSaveFile(string saveFile)
        {
            //Application.persistentDataPath 為不同平台的路徑 (如 Windows, Mac, iOS, Android)
            //return Application.persistentDataPath + "/" + saveFile + ".sav"; 
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}
