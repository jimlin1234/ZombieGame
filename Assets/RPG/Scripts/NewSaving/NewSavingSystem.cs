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
            //using���z��:�Ω�T�O�b�Y�ӵ{���϶�������A��޲z���귽�]�p�ɮ׬y�B��Ʈw�s�u���^����Q���T�a����. ���q�`�Ψ�²�� try-finally �϶��A�� ���]�t catch �����~�B�z����. using ���z������W�O�@�ӽsĶ���y�k�}(syntax sugar)�A���|�Q�sĶ���@�� try-finally �϶�.
            using (FileStream stream = File.Open(path, FileMode.Create)) //FileMode.Create  �гy�@�ӷs���(return FileStream)�A�p�G���F�N�л\
            {
                Transform playerTransform = GetPlayerTransform();
                //byte[] buffer = SerializeVector(playerTransform.position);

                BinaryFormatter formatter = new BinaryFormatter(); //�إ�BinaryFormatter����
                NewSerializableVector3 position = new NewSerializableVector3(playerTransform.position); //�NVector3�নNewSerializableVector3����
                formatter.Serialize(stream, position); //�Nposition����ǦC�ƫ�g�Jstream



                //byte[] bytes = Encoding.UTF8.GetBytes("Hello Word!!!"); //�N�r���নbyte�}�C
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
                //byte[] buffer = new byte[stream.Length];//�ŧi�@��byte�}�C,���׬�stream������
                //stream.Read(buffer, 0, buffer.Length); //Read(Ū����ƨ�buffer�}�C,�q��0�Ӧ�m�}�l��,��h�֭�byte)
                BinaryFormatter formatter = new BinaryFormatter();
                //print(Encoding.UTF8.GetString(buffer));
                //print(DeserializeVector(buffer));
                Transform playerTransform = GetPlayerTransform();
                NewSerializableVector3 position = (NewSerializableVector3)formatter.Deserialize(stream); //�Nstream�ϧǦC�Ʀ�NewSerializableVector3����
                //playerTransform.position = DeserializeVector(buffer);
                playerTransform.position = position.ToVector(); //�NNewSerializableVector3�����নVector3�ó]�w��player��m
                Mover mover = playerTransform.GetComponent<Mover>();
                mover.Cancel(); //�������ʡA�קK���J��m��A�����~�򲾰ʨ줧�e�]�w���ؼЦ�m
            }
            //FileStream stream = File.Open(path, FileMode.Open); //FileMode.Open �H�}�Ҥ@�Ӥw�s�b�����
            
        }

        

        private Transform GetPlayerTransform()
        {
            return GameObject.FindWithTag("Player").transform;
        }


        private byte[] SerializeVector(Vector3 vector) //�NVector3�নbyte�}�C (�ǦC��Vector3)
        {
            byte[] vectorBytes = new byte[3 * 4]; //3��float,�C��float��4��byte
            BitConverter.GetBytes(vector.x).CopyTo(vectorBytes, 0);
            BitConverter.GetBytes(vector.y).CopyTo(vectorBytes, 4);
            BitConverter.GetBytes(vector.z).CopyTo(vectorBytes, 8);
            return vectorBytes;
        }

        private Vector3 DeserializeVector(byte[] buffer) //�Nbyte�}�C�নVector3 (�ϧǦC��Vector3)
        {
            Vector3 result = new Vector3();
            result.x = BitConverter.ToSingle(buffer, 0);
            result.y = BitConverter.ToSingle(buffer, 4);
            result.z = BitConverter.ToSingle(buffer, 8);

            return result;
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            //Application.persistentDataPath �����P���x�����| (�p Windows, Mac, iOS, Android)
            //Application.persistentDataPath���ѤF�@�ӯS�w��m�A�}�o�̥i�H�w���a�x�s�MŪ���ϥΪ̫��[�ƾڡA�ӳo�Ӧ�m�b���P�����x�W�i�঳�Ҥ��P
            //return Application.persistentDataPath + "/" + saveFile + ".sav"; 
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}
