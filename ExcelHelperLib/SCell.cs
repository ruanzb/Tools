#region 说明
//---------------------------------------名称:可自定义样式的单元格
//---------------------------------------依赖:Aspose.Cells.dll
//---------------------------------------版本:1.0.0.0
//---------------------------------------更新时间:2017/11/13
//---------------------------------------作者:献丑
//---------------------------------------CSDN:http://blog.csdn.net/qq_26712977
//---------------------------------------GitHub:https://github.com/a462247201/Tools 
#endregion

#region 名空间
using Aspose.Cells;
using System.Drawing; 
#endregion

namespace ExcelHelperLib
{
    /// <summary>
    /// 可配置样式的单元格 继承与CellBase
    /// </summary>
    public class SCell:CellBase
    {
        #region 方法
        /// <summary>
        /// 通过当前设置 生成样式
        /// </summary>
        /// <returns></returns>
        public Style CreateStyle()
        {
            Style style = new Style();
            //边框颜色
            if (CStyle.BorderColor != Color.White)
            {
                style.Borders[BorderType.TopBorder].Color = CStyle.BorderColor;
                style.Borders[BorderType.BottomBorder].Color = CStyle.BorderColor;
                style.Borders[BorderType.LeftBorder].Color = CStyle.BorderColor;
                style.Borders[BorderType.RightBorder].Color = CStyle.BorderColor;
            }
            //边框线
            style.Borders[BorderType.TopBorder].LineStyle = CStyle.TopBorder;
            style.Borders[BorderType.BottomBorder].LineStyle = CStyle.BottomBorder;
            style.Borders[BorderType.LeftBorder].LineStyle = CStyle.LeftBorder;
            style.Borders[BorderType.RightBorder].LineStyle = CStyle.RightBorder;

            //单元格
            if (CStyle.ForegroundColor != Color.White)
            {
                style.ForegroundColor = CStyle.ForegroundColor;
                style.Pattern = BackgroundType.Solid;
            }

            //对齐
            style.HorizontalAlignment = CStyle.HorizontalAlignment;
            style.VerticalAlignment = CStyle.VerticalAlignment;

            //字体
            style.Font.IsBold = CStyle.IsBold;
            style.Font.IsItalic = CStyle.IsItalic;
            style.Font.Size = CStyle.FontSize;
            style.Font.Color = CStyle.FontColor;

            //换行
            style.IsTextWrapped = CStyle.IsTextWrapped;
            return style;
        }
        #endregion

        /// <summary>
        /// 单元格样式
        /// </summary>
        CellStyle _CStyle = new CellStyle();

        public CellStyle CStyle
        {
            get { return _CStyle; }
            set { _CStyle = value; }
        }

    }
}
