using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace novelReader.Models
{
    /// <summary>
    /// 内容
    /// </summary>
    public class Content
    {
        /// <summary>
        /// 文本
        /// </summary>
        public String Text { get; set; }
        /// <summary>
        /// 页眉
        /// </summary>
        public String Header { get; set; }
        public Boolean IsHeader { get; set; }
        /// <summary>
        /// 阅读时间
        /// </summary>
        private double readingTime;
        /// <summary>
        /// 阅读速度
        /// </summary>
        private double readingSpeed;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text"></param>
        public Content(String text)
        {
            //移除空白
            this.Text = text.Trim();
            //得到章节匹配的结果
            Match matchResult = ChapterMatcher.Exec(this.Text);
            //页眉设置为章节结果
            this.Header = matchResult.Value;
            //章节结果正负值
            this.IsHeader = matchResult.Success;
        }
        /// <summary>
        /// 预计阅读时间
        /// </summary>
        /// <param name="speed">阅读速度</param>
        /// <returns>阅读时间</returns>
        public double EstimateReadingTime(int speed)
        {
            //比较速度看其是否有改变
            if (speed == this.readingSpeed) {
                return this.readingTime;
            }
            //更改速度
            this.readingSpeed = speed;
            //计算阅读时间
            this.readingTime = calReadingTime(this.Text.Length, speed) + calBufferTime(this.Text.Length, speed);

            return this.readingTime;
        }
        /// <summary>
        /// 阅读时间
        /// </summary>
        /// <param name="length"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        private double calReadingTime(int length, int speed)
        {
            return ((double)length / speed) * 1000.0;
        }
        /// <summary>
        /// 缓冲时间
        /// </summary>
        /// <param name="length"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        private double calBufferTime(int length, int speed)
        {
            if (length > speed * 2)
            {
                return calReadingTime(length - speed, speed * 3);
            }
            else if (length < speed)
            {
                return calReadingTime(length, speed * 2);
            }
            else
            {
                return 0.0;
            }
        }
        
    }
}
