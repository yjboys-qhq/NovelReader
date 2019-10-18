using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace novelReader.Models
{
    /// <summary>
    /// С˵�ļ�
    /// </summary>
    public class NovelFile
    {
        /// <summary>
        /// ���ļ�
        /// </summary>
        /// <param name="path">·��</param>
        /// <param name="novel"></param>
        public static void Open(String path, ObservableCollection<Content> novel)
        {

            //�粻Ϊ��
            if (String.IsNullOrEmpty(path))
            {
                return;
            }
            //�����ļ���
            FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read);
            //�����ȡ��
            StreamReader sr = new StreamReader(fs);
            //�����ǰ������UTF8
            if (sr.CurrentEncoding == Encoding.UTF8)
            {
                //����1024��char����
                var chArr = new char[1024];
                //��ȡ1024��char
                sr.Read(chArr, 0, chArr.Length);
                //��ȡbyte����
                var buffer1 = Encoding.UTF8.GetBytes(chArr);
                //����byte����2����С��1��ͬ
                var buffer2 = new byte[buffer1.Length];
                //�ļ���λ��Ϊ0
                fs.Position = 0;
                //��ȡ�ļ�����buffer2
                fs.Read(buffer2, 0, buffer2.Length);
                //����bool���ͣ��Ƿ���ͬ
                var same = true;
                //����ж��Ƿ���ͬ
                for (int i = 0; i < buffer1.Length; i++)
                {
                    if (buffer1[i] != buffer2[i])
                    {
                        same = false;
                        break;
                    }
                }
                //�����ͬ�������뷽ʽ���´򿪶�ȡ��
                if (!same)
                {
                    fs.Position = 0;
                    sr = new StreamReader(fs, Encoding.GetEncoding("GBK"));
                }
            }
            //����line
            String line;
            //����ȡ�����в�Ϊ��
            while ((line = sr.ReadLine()) != null)
            {
                //�粻Ϊ�ջ�ȫ�ǿո񣬽���һ����ӵ�С˵��
                if (!String.IsNullOrWhiteSpace(line))
                {
                    novel.Add(new Content(line));
                }
            }
            sr.Dispose();
        }
    }
}
