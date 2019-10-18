using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace novelReader.Models
{
    public class Sidebar : ListBoxWrapper
    {
        public Sidebar(ListBox list) : base(list)
        {
            //行不置顶
            this.KeepLineInTop = false;
        }
    }
}
