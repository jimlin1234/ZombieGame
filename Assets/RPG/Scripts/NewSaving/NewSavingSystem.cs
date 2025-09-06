using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
            //using陳述式:用於確保在某個程式區塊結束後，其管理的資源（如檔案流、資料庫連線等）能夠被正確地釋放. 它通常用來簡化 try-finally 區塊，但 不包含 catch 的錯誤處理部分. using 陳述式本質上是一個編譯器語法糖(syntax sugar)，它會被編譯成一個 try-finally 區塊.
            using (FileStream stream = File.Open(path, FileMode.Create)) //FileMode.Create  創造一個新文件(return FileStream)，如果有了就覆蓋
            {
                Transform playerTransform = GetPlayerTransform();
                //byte[] buffer = SerializeVector(playerTransform.position);

                BinaryFormatter formatter = new BinaryFormatter(); //建立BinaryFormatter物件
                NewSerializableVector3 position = new NewSerializableVector3(playerTransform.position); //將Vector3轉成NewSerializableVector3物件
                formatter.Serialize(stream, position); //將position物件序列化後寫入stream



                //byte[] bytes = Encoding.UTF8.GetBytes("Hello Word!!!"); //將字串轉成byte陣列
                //stream.Write(buffer, 0, buffer.Length); 
            }
           
            //stream.Close();
        }

        

        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Loading from " + path);

            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                //byte[] buffer = new byte[stream.Length];//宣告一個byte陣列,長度為stream的長度
                //stream.Read(buffer, 0, buffer.Length); //Read(讀取資料到buffer陣列,從第0個位置開始放,放多少個byte)
                BinaryFormatter formatter = new BinaryFormatter();
                //print(Encoding.UTF8.GetString(buffer));
                //print(DeserializeVector(buffer));
                Transform playerTransform = GetPlayerTransform();
                NewSerializableVector3 position = (NewSerializableVector3)formatter.Deserialize(stream); //將stream反序列化成NewSerializableVector3物件
                //playerTransform.position = DeserializeVector(buffer);
                playerTransform.position = position.ToVector(); //將NewSerializableVector3物件轉成Vector3並設定給player位置
                Mover mover = playerTransform.GetComponent<Mover>();
                mover.Cancel(); //取消移動，避免載入位置後，角色繼續移動到之前設定的目標位置
            }
            //FileStream stream = File.Open(path, FileMode.Open); //FileMode.Open 以開啟一個已存在的文件
            
        }

        

        private Transform GetPlayerTransform()
        {
            return GameObject.FindWithTag("Player").transform;
        }


        private byte[] SerializeVector(Vector3 vector) //將Vector3轉成byte陣列 (序列化Vector3)
        {
            byte[] vectorBytes = new byte[3 * 4]; //3個float,每個float為4個byte
            BitConverter.GetBytes(vector.x).CopyTo(vectorBytes, 0);
            BitConverter.GetBytes(vector.y).CopyTo(vectorBytes, 4);
            BitConverter.GetBytes(vector.z).CopyTo(vectorBytes, 8);
            return vectorBytes;
        }

        private Vector3 DeserializeVector(byte[] buffer) //將byte陣列轉成Vector3 (反序列化Vector3)
        {
            Vector3 result = new Vector3();
            result.x = BitConverter.ToSingle(buffer, 0);
            result.y = BitConverter.ToSingle(buffer, 4);
            result.z = BitConverter.ToSingle(buffer, 8);

            return result;
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            //Application.persistentDataPath 為不同平台的路徑 (如 Windows, Mac, iOS, Android)
            //Application.persistentDataPath提供了一個特定位置，開發者可以安全地儲存和讀取使用者持久數據，而這個位置在不同的平台上可能有所不同
            //return Application.persistentDataPath + "/" + saveFile + ".sav"; 
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}
