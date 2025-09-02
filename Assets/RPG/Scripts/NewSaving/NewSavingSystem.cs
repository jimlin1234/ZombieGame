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
            //using���z��:�Ω�T�O�b�Y�ӵ{���϶�������A��޲z���귽�]�p�ɮ׬y�B��Ʈw�s�u���^����Q���T�a����. ���q�`�Ψ�²�� try-finally �϶��A�� ���]�t catch �����~�B�z����. using ���z������W�O�@�ӽsĶ���y�k�}(syntax sugar)�A���|�Q�sĶ���@�� try-finally �϶�.
            using (FileStream stream = File.Open(path, FileMode.Create)) //FileMode.Create  �гy�@�ӷs���(return FileStream)�A�p�G���F�N�л\
            {
                byte[] bytes = Encoding.UTF8.GetBytes("Hello Word!!!");
                stream.Write(bytes, 0, bytes.Length); 
            }
           
            //stream.Close();
        }

        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Loading from " + path);

            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                byte[] buffer = new byte[stream.Length];//�ŧi�@��byte�}�C,���׬�stream������
                stream.Read(buffer, 0, buffer.Length); //Read(Ū����ƨ�buffer�}�C,�q��0�Ӧ�m�}�l��,��h�֭�byte)

                print(Encoding.UTF8.GetString(buffer));

            }
            //FileStream stream = File.Open(path, FileMode.Open); //FileMode.Open �H�}�Ҥ@�Ӥw�s�b�����
            
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            //Application.persistentDataPath �����P���x�����| (�p Windows, Mac, iOS, Android)
            //return Application.persistentDataPath + "/" + saveFile + ".sav"; 
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}
