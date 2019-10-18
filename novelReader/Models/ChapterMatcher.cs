using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace novelReader.Models
{
    /// <summary>
    /// 章节匹配
    /// </summary>
    public class ChapterMatcher
    {
        //正则表达式确定章节名称
        static Regex chapterRegex = new Regex(@"(?:^\s*|^\s*第.*?)(第[^\s,.，。]*?[章篇]\s?.*)");

        /// <summary>
        /// 返回正则表达式的结果
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Match Exec(string text)
        {
            return chapterRegex.Match(text);
        }
    }
}
