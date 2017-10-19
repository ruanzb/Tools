
#region 说明
//---------------------------------------名称:ListView重写 
//---------------------------------------版本:1.0.0.0
//---------------------------------------说明:listview双缓冲防闪烁
//---------------------------------------更新时间:2017/10/19
//---------------------------------------作者:献丑
//---------------------------------------CSDN:http://blog.csdn.net/qq_26712977
//---------------------------------------GitHub:https://github.com/a462247201/Tools
#endregion


#region 名空间
using System.Windows.Forms; 
#endregion

namespace PControlsLib
{
    /// <summary>
    /// ListView重写 
    /// </summary>
    public class ListViewNF : System.Windows.Forms.ListView
    {
        #region 方法
        public ListViewNF()
        {
            // 开启双缓冲
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            // Enable the OnNotifyMessage event so we get a chance to filter out 
            // Windows messages before they get to the form's WndProc
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
            this.FullRowSelect = true;
            this.View = System.Windows.Forms.View.Details;
            this.GridLines = true;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        } 
        #endregion

        #region 函数重写
        protected override void OnNotifyMessage(Message m)
        {
            //Filter out the WM_ERASEBKGND message
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }
        }
        #endregion
    }
}
