using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Patterns;
using UnityEngine;

namespace Assets.Scripts.Common
{
    /// <summary>
    /// Вспомогательный класс для работы лога
    /// </summary>
    public class LogVO
    {
        public string t;  //type
        public string tm; //time
        public string l;  //log
        public string s;  //stack

        public LogVO(string logString, string stackTrace, LogType type)
        {
            /*
          if (type == LogType.Assert)
          {
              output.t = "Assert";
              output.l = logString;
          }
          else if (type == LogType.Exception)
          {
              output.t = "Exception";
              output.l = logString;
          }
          else
          {
              int end = logString.IndexOf("]");
              if (end >= 0)
              {
                  output.t = logString.Substring(1, end - 1);
                  output.l = logString.Substring(end + 2);
              }
              else
              {
                  output.l
              }
          }
          */
            t = logTypeToString(type);
            l = logString;
            s = stackTrace;
            tm = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private string logTypeToString(LogType type)
        {
            string res;
            switch (type)
            {
                case LogType.Error:
                    res = "Error";
                    break;
                case LogType.Assert:
                    res = "Assert";
                    break;
                case LogType.Warning:
                    res = "Warning";
                    break;
                case LogType.Log:
                    res = "Log";
                    break;
                case LogType.Exception:
                    res = "Exception";
                    break;
                default:
                    res = "Unknown";
                    break;
            }
            return res;
        }
    }

    /// <summary>
    /// Возможные типы файлов для лога
    /// </summary>
    public enum FileType
    {
        CSV,
        JSON,
        TSV,
        TXT
    }

    /// <summary>
    /// Собственный обработчик логов для удобства анализа происходящего
    /// </summary>
    class MdLogHandler : Mediator
    {

        public new const string NAME = "MdLogHandler";

        public bool useAbsolutePath = false;
        public string fileName = "MyGame";
        public FileType fileType = FileType.CSV;

        public string absolutePath = "c:\\";

        public string filePath;
        public string filePathFull;
        public int count = 0;

        System.IO.StreamWriter fileWriter;

        public MdLogHandler(object viewComponent, FileType fileType = FileType.JSON ) : base (NAME, viewComponent)
        {
            this.fileType = fileType;
        }

        string FileExtensionFromType(FileType type)
        {
            switch (type)
            {
                case FileType.JSON: return ".json";
                case FileType.CSV: return ".csv";
                case FileType.TSV: return ".tsv";
                case FileType.TXT:
                default: return ".txt";
            }
        }

        public override void OnRegister()
        {
            base.OnRegister();
            Debug.Log("OnRegister " + NAME);
            UpdateFilePath();
            if (Application.isPlaying)
            {
                count = 0;
                fileWriter = new System.IO.StreamWriter(filePathFull, false);
                fileWriter.AutoFlush = true;
                switch (fileType)
                {
                    case FileType.CSV:
                        fileWriter.WriteLine("type,time,log,stack");
                        break;
                    case FileType.JSON:
                        fileWriter.WriteLine("[");
                        break;
                    case FileType.TSV:
                        fileWriter.WriteLine("type\ttime\tlog\tstack");
                        break;
                }
                Application.logMessageReceived += OnMessageLog;
                Debug.Log("Saving data to " + filePathFull);
            }
            
        }


        public override void OnRemove()
        {
            base.OnRemove();
            Debug.Log("OnRemove " + NAME);
            if (Application.isPlaying)
            {
                Application.logMessageReceived -= OnMessageLog;

                switch (fileType)
                {
                    case FileType.JSON:
                        fileWriter.WriteLine("\n]");
                        break;
                    case FileType.CSV:
                    case FileType.TSV:
                    default:
                        break;
                }
                fileWriter.Close();
            }
        }



        private void OnMessageLog(string logString, string stackTrace, LogType type)
        {
            LogVO vo = new LogVO(logString, stackTrace, type);

            switch (fileType)
            {
                case FileType.CSV:
                    fileWriter.WriteLine(vo.t + "," + vo.tm + "," + 
                        vo.l.Replace(",", " ").Replace("\n", "") + "," + vo.s.Replace(",", " ").Replace("\n", ""));
                    break;
                case FileType.JSON:
                    fileWriter.Write((count == 0 ? "" : ",\n") + JsonUtility.ToJson(vo));
                    break;
                case FileType.TSV:
                    fileWriter.WriteLine(vo.t + "\t" + vo.tm + "\t" + 
                        vo.l.Replace("\t", " ").Replace("\n", "") + "\t" + vo.s.Replace("\t", " ").Replace("\n", ""));
                    break;
                case FileType.TXT:
                    fileWriter.WriteLine("Type: " + vo.t);
                    fileWriter.WriteLine("Time: " + vo.tm);
                    fileWriter.WriteLine("Log: " + vo.l);
                    fileWriter.WriteLine("Stack: " + vo.s);
                    break;
            }

            count++;
        }

        public void UpdateFilePath()
        {
            filePath = useAbsolutePath ? absolutePath : Application.persistentDataPath;
            filePathFull = System.IO.Path.Combine(filePath, fileName + "." + 
                System.DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss") + FileExtensionFromType(fileType));
        }

    }
}
