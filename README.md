# Tools

基于FrameWork 4.0 编译环境为VS2013

HttpToolsLib封装了对于Http请求的一些操作并集成了苏飞的HttpHelper类

ExtractLib封装了包括字符串截取，编码转换，正则表达式，Xpath等操作在内的帮助类，其中操作XPath依赖于HtmlAgilityPack.dll实现


-----------------------------------------------
2017/11/13
ExcelHelpLib:
新增了封装了Excel内容和样式设置的ExcelHelperLib 可对导出的Excel进行边框,字体，颜色等样式设置


-----------------------------------------------
2017/11/15
ExcelHelpLib:
不再对所谓的标题行标题列作区分(在ExcelFormat对象中只保留SCells属性，即可配置样式的单元格集合。除此之外，新增了数据行，数据列，数据区块的概念，方便一组规则且具有相同样式的数据区块插入。为确保配置样式和插入的灵活性，所有的单元格最终汇总到SCells中等待写入)
