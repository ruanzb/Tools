#region 说明
//---------------------------------------名称:Excel全局样式
//---------------------------------------版本:1.0.0.0
//---------------------------------------更新时间:2017/11/13
//---------------------------------------作者:献丑
//---------------------------------------CSDN:http://blog.csdn.net/qq_26712977
//---------------------------------------GitHub:https://github.com/a462247201/Tools 
#endregion

#region 名空间
using Aspose.Cells; 
#endregion

namespace ExcelHelperLib
{
    /// <summary>
    /// 全局样式模板
    /// </summary>
   public  class StyleFormat
    {
        /// <summary>
        /// 列宽
        /// </summary>
        int _ColumnsSize = 20;

        public int ColumnsSize
        {
            get { return _ColumnsSize; }
            set { _ColumnsSize = value; }
        }
        /// <summary>
        /// 行高
        /// </summary>
        int _RowsSize = 10;

        public int RowsSize
        {
            get { return _RowsSize; }
            set { _RowsSize = value; }
        }

        /// <summary>
        /// 文字对齐方式 默认居中
        /// </summary>
        TextAlignmentType _Alignment = TextAlignmentType.Center;

        public TextAlignmentType Alignment
        {
            get { return _Alignment; }
            set { _Alignment = value; }
        }

        /// <summary>
        /// 是否自动换行
        /// </summary>
        bool _IsTextWrapped = false;

        public bool IsTextWrapped
        {
            get { return _IsTextWrapped; }
            set { _IsTextWrapped = value; }
        }

    }
}
