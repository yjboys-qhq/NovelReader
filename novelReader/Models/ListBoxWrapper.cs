using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace novelReader.Models
{
    /// <summary>
    /// 列表箱包装
    /// </summary>
    public abstract class ListBoxWrapper
    {
        /// <summary>
        /// 可选项列表
        /// </summary>
        protected ListBox list;
        /// <summary>
        /// 行置顶
        /// </summary>
        public bool KeepLineInTop { get; set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="list"></param>
        public ListBoxWrapper(ListBox list)
        {
            this.list = list;
            this.KeepLineInTop = true;
        }
        /// <summary>
        /// 跳转到置顶行，行跳转
        /// </summary>
        /// <param name="num">指定行</param>
        private void gotoLine(int num)
        {
            //给选择索引赋值
            this.list.SelectedIndex = num;
            //将选择到的行滚动到视野中
            this.list.ScrollIntoView(this.list.SelectedItem);
        }
        /// <summary>
        /// 跳转到置顶章，章跳转
        /// </summary>
        /// <param name="item">条目</param>
        private void gotoLineItem(object item)
        {
            this.list.SelectedItem = item;
            this.list.ScrollIntoView(this.list.SelectedItem);
        }
        /// <summary>
        /// 跳转到首行
        /// </summary>
        public void FocusOnFirstLine()
        {
            this.gotoLine(0);
        }
        /// <summary>
        /// 跳转到尾行
        /// </summary>
        public void FocusOnLastLine()
        {
            this.gotoLine(this.LineCount() - 1);
        }
        /// <summary>
        /// 跳转到上一行
        /// </summary>
        public void FocusOnPrevLine()
        {
            if (this.IsAtFirstLine()) return;

            this.gotoLine(this.CurrentLine() - 1);
        }
        /// <summary>
        /// 跳转到下一行
        /// </summary>
        public void FocusOnNextLine()
        {
            if (this.IsAtLastLine()) return;

            int nextLine = this.CurrentLine() + 1;

            if (this.KeepLineInTop)
            {
                this.FocusOnLastLine();
                this.Refresh();
            }

            this.gotoLine(nextLine);
        }
        /// <summary>
        /// 设置焦点到该列
        /// </summary>
        /// <param name="num"></param>
        public void FocusOnLine(int num) {
            if (this.KeepLineInTop)
            {
                this.FocusOnLastLine();
                this.Refresh();
            }

            this.gotoLine(num);
        }
        /// <summary>
        /// 跳转到该项目
        /// </summary>
        /// <param name="item"></param>
        public void FocusOnLineItem(object item)
        {
            if (this.KeepLineInTop)
            {
                this.FocusOnLastLine();
                this.Refresh();
            }

            this.gotoLineItem(item);
        }
        /// <summary>
        /// 设置焦点
        /// </summary>
        public void Focus() {
            this.list.Focus();
        }
        /// <summary>
        /// 当前项
        /// </summary>
        /// <returns></returns>
        public Content CurrentLineItem()
        {
            return (Content)this.list.SelectedItem;
        }
        /// <summary>
        /// 当前行
        /// </summary>
        /// <returns></returns>
        public int CurrentLine()
        {
            return this.list.SelectedIndex;
        }
        /// <summary>
        /// 行数
        /// </summary>
        /// <returns></returns>
        public int LineCount()
        {
            return this.list.Items.Count;
        }
        /// <summary>
        /// 是否第一行
        /// </summary>
        /// <returns></returns>
        public bool IsAtFirstLine()
        {
            return this.CurrentLine() == 0;
        }
        /// <summary>
        /// 是否最后一行
        /// </summary>
        /// <returns></returns>
        public bool IsAtLastLine()
        {
            return this.CurrentLine() + 1 == this.LineCount();
        }
        /// <summary>
        /// 布局更新
        /// </summary>
        public void Refresh()
        {
            this.list.UpdateLayout();
        }
    }
}
