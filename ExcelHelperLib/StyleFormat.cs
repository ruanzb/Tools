#region 说明
//---------------------------------------名称:Excel全局样式
//---------------------------------------依赖:Aspose.Cells.dll
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
    /// 区域样式模板 指定区域内所有单元格的样式
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
    }
}
