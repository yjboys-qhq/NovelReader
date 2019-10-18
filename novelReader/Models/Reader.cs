using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace novelReader.Models
{
    public class Reader : ListBoxWrapper
    {
        /// <summary>
        /// 阅读功能
        /// </summary>
        /// <param name="list"></param>
        public Reader(ListBox list) : base(list)
        {
            //让行置顶
            this.KeepLineInTop = true;
        }
    }
}
