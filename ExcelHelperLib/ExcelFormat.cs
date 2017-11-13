#region 说明
//---------------------------------------名称:可自定义的Excel格式模板
//---------------------------------------版本:1.0.0.0
//---------------------------------------依赖:Aspose.Cells.dll
//---------------------------------------更新时间:2017/11/10
//---------------------------------------作者:献丑
//---------------------------------------CSDN:http://blog.csdn.net/qq_26712977
//---------------------------------------GitHub:https://github.com/a462247201/Tools 
#endregion

#region 名空间
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Aspose.Cells;
#endregion

namespace ExcelHelperLib
{
    /// <summary>
    /// 可自定义的Excel格式模板 定义全局样式和标题样式内容
    /// </summary>
    public class ExcelFormat:StyleFormat
    {
        #region Excel内容相关
        /// <summary>
        /// Sheet索引
        /// </summary>
        int _SheetIndex = 0;

        public int SheetIndex
        {
            get { return _SheetIndex; }
            set { _SheetIndex = value; }
        }
     
        /// <summary>
        /// 列标题
        /// </summary>
        List<SCell> _Columns = new List<SCell>();

        public List<SCell> Columns
        {
            get { return _Columns; }
            set { _Columns = value; }
        }
        /// <summary>
        /// 行标题
        /// </summary>
        List<SCell> _Rows = new List<SCell>();

        public List<SCell> Rows
        {
            get { return _Rows; }
            set { _Rows = value; }
        }

        /// <summary>
        /// 单元格List
        /// </summary>
        List<SCell> _SCells = new List<SCell>();

        public List<SCell> SCells
        {
            get { return _SCells; }
            set { _SCells = value; }
        }

        #endregion

        #region 文件路径和保存相关

        /// <summary>
        /// 相同路径文件存在时是否覆盖文件  默认覆盖
        /// </summary>
        bool _IsCover = true;

        public bool IsCover
        {
            get { return _IsCover; }
            set { _IsCover = value; }
        }

        /// <summary>
        /// 保存路径
        /// </summary>
        String _SavePath = "Temp.xlsx";

        public String SavePath
        {
            get { return _SavePath; }
            set { _SavePath = value; }
        }
        
        #endregion

        /// <summary>
        /// 添加标题
        /// </summary>
        /// <param name="Inner_objlist">标题填充内容List</param>
        /// <param name="Style">标题单元格样式</param>
        /// <param name="TitleType">标题类型 1列标题 2行标题</param>
        /// <param name="FillModel">(0,0)单元格填充模式</param>
        public void CreateTitle(IList<object> Inner_objlist,CellStyle Style,int TitleType = 1,ExcelMethod.FillModel FillModel = ExcelMethod.FillModel.空出第一个单元格)
        {
            for(int i = 0;i<Inner_objlist.Count;i++)
            {
                SCell scell = new SCell();
                scell.CStyle = Style;
                scell.Txt_Obj = Inner_objlist[i];
                if (TitleType == 1)
                {
                    if (FillModel == ExcelMethod.FillModel.列标题填充)
                    {
                        scell.Y = i;
                    }
                    else
                    {
                        scell.Y = i+1;
                    }

                    this.Columns.Add(scell);
                }
                else if(TitleType == 2)
                {
                    if (FillModel == ExcelMethod.FillModel.行标题填充)
                    {
                        scell.X = i;
                    }
                    else
                    {
                        scell.X = i + 1;
                    }

                    this.Rows.Add(scell);
                }
            }
        }

    }
}
