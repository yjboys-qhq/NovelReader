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
    /// 小说文件
    /// </summary>
    public class NovelFile
    {
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="novel"></param>
        public static void Open(String path, ObservableCollection<Content> novel)
        {

            //如不为空
            if (String.IsNullOrEmpty(path))
            {
                return;
            }
            //定义文件流
            FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read);
            //定义读取流
            StreamReader sr = new StreamReader(fs);
            //如果当前编码是UTF8
            if (sr.CurrentEncoding == Encoding.UTF8)
            {
                //定义1024的char数组
                var chArr = new char[1024];
                //读取1024个char
                sr.Read(chArr, 0, chArr.Length);
                //获取byte数组
                var buffer1 = Encoding.UTF8.GetBytes(chArr);
                //声明byte数组2，大小与1相同
                var buffer2 = new byte[buffer1.Length];
                //文件流位置为0
                fs.Position = 0;
                //读取文件流到buffer2
                fs.Read(buffer2, 0, buffer2.Length);
                //声明bool类型：是否相同
                var same = true;
                //逐个判断是否相同
                for (int i = 0; i < buffer1.Length; i++)
                {
                    if (buffer1[i] != buffer2[i])
                    {
                        same = false;
                        break;
                    }
                }
                //如果不同，换编码方式重新打开读取流
                if (!same)
                {
                    fs.Position = 0;
                    sr = new StreamReader(fs, Encoding.GetEncoding("GBK"));
                }
            }
            //定义line
            String line;
            //当读取到的行不为空
            while ((line = sr.ReadLine()) != null)
            {
                //如不为空或全是空格，将这一行添加到小说中
                if (!String.IsNullOrWhiteSpace(line))
                {
                    novel.Add(new Content(line));
                }
            }
            sr.Dispose();
        }
    }
}
