#region 说明
//---------------------------------------名称:Apose Excel单元格自定义基类
//---------------------------------------版本:1.0.0.0
//---------------------------------------更新时间:2017/11/10
//---------------------------------------作者:献丑
//---------------------------------------CSDN:http://blog.csdn.net/qq_26712977
//---------------------------------------GitHub:https://github.com/a462247201/Tools 
#endregion

using System.Drawing;
using System.IO;

namespace ExcelHelperLib
{
    /// <summary>
    /// 单元格基类 支持坐标和内容
    /// </summary>
    public  class CellBase
    {
        #region 构造函数
        public CellBase()
        {

        }

        /// <summary>
        /// 普通文本
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="data"></param>
        public CellBase(int x, int y, object data)
        {
            this.X = x;
            this.Y = y;
            this.Txt_Obj = data;
            this.IsPic = false;
        }
        /// <summary>
        /// 图片流
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="data"></param>
        public CellBase(int x, int y, MemoryStream data)
        {
            this.X = x;
            this.Y = y;
            this.Image_Ms = data;
            this.IsPic = true;
        }

        /// <summary>
        /// 图片流
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="data"></param>
        public CellBase(int x, int y, Image data,System.Drawing.Imaging.ImageFormat format_type)
        {
            this.X = x;
            this.Y = y;
            data.Save(Image_Ms, format_type);
            this.IsPic = true;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 纵坐标
        /// </summary>
        int _Y = 0;

        public int Y
        {
            get { return _Y; }
            set { _Y = value; }
        }

        /// <summary>
        /// 横坐标
        /// </summary>
        int _X = 0;

        public int X
        {
            get { return _X; }
            set { _X = value; }
        }

        /// <summary>
        /// 是否是图片数据
        /// </summary>
        bool _IsPic = false;

        public bool IsPic
        {
            get { return _IsPic; }
            set { _IsPic = value; }
        }
        /// <summary>
        /// 普通文本
        /// </summary>
        object _Txt_Obj = new object();

        public object Txt_Obj
        {
            get { return _Txt_Obj; }
            set { _Txt_Obj = value; }
        }
        /// <summary>
        /// 图片流
        /// </summary>
        MemoryStream _Image_Ms = new MemoryStream();

        public MemoryStream Image_Ms
        {
            get { return _Image_Ms; }
            set { _Image_Ms = value; }
        }
        #endregion
    }
}
