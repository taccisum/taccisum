using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Common.CustomerException;
using Common.Tool.Extend;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Common.Tool.Units
{
    public class ExcelHepler
    {

        /// <summary>
        /// 导出Excel表时用于格式化指定名称字段的委托函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public delegate string FieldFormatter(string name, object field);

        /// <summary>
        /// 将list的数据导出到指定路径的Excel文件中，同时将执行过程中产生的警告信息保存到字段warningMsg
        /// 每个字段为一列按顺序导出
        /// 可指定不需要导出的字段名称，可通过编写委托函数设置指定名称字段的导出格式
        /// 失败：抛出异常
        /// </summary>
        /// auto: tac
        /// created date: 2016/01/25 PM
        /// <exception cref="CommonException"></exception>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">要导出的数据</param>
        /// <param name="templatePath">模版文件路径</param>
        /// <param name="path">文件导出路径</param>
        /// <param name="unexportFiledList">不需要导出的字段列表（区分大小写）</param>
        /// <param name="formatter">调用者用于格式化指定名称字段的委托函数</param>
        /// <returns></returns>
        public void Export<T>(IEnumerable<T> list, string templatePath, string path, List<string> unexportFiledList = null, FieldFormatter formatter = null)
        {
            if (!list.Any())
            {
                throw new CommonException("无任何要导出的数据");
            }

            try
            {
                File.Copy(templatePath, path, true);

                using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    IWorkbook workbook = null;
                    ISheet sheet = null;

                    Regex xls = new Regex("\\.xls$", RegexOptions.IgnoreCase);
                    Regex xlsx = new Regex("\\.xlsx$", RegexOptions.IgnoreCase);
                    if (xls.Match(path).Length > 0)
                    {
                        // 2003版本
                        workbook = new HSSFWorkbook(fs);
                    }
                    else if (xlsx.Match(path).Length > 0)
                    {
                        // 2007版本
                        workbook = new XSSFWorkbook(fs);
                    }
                    else
                    {
                        throw new CommonException("文件格式错误");
                    }

                    //创建属性的集合    
                    List<PropertyInfo> pList = new List<PropertyInfo>();
                    //获得反射的入口    
                    Type type = typeof (T);
                    //把所有的public属性加入到集合
                    Array.ForEach<PropertyInfo>(type.GetProperties(), p => pList.Add(p));


                    sheet = (ISheet) workbook.GetSheetAt(0);
                    int rowIndex = 1;
                    foreach (var item in list)
                    {
                        IRow dataRow = (IRow) sheet.CreateRow(rowIndex);
                        var col = 0;
                        for (int i = 0; i < pList.Count; i++)
                        {
                            //如果是不需要导出的字段，则continue
                            if (unexportFiledList != null && unexportFiledList.Contains(pList[i].Name))
                                continue;

                            var fieldType = pList[i].PropertyType.Name;
                            var fieldValue = pList[i].GetValue(item);
                            string txt = "";
                            //需要格式化特定字段
                            if (formatter != null)
                                txt = formatter(pList[i].Name, fieldValue);
                            else
                            {
                                txt = fieldValue == null ? "" : fieldValue.ToString();
                            }

                            dataRow.CreateCell(col).SetCellValue(txt);
                            col++;
                        }

                        rowIndex++;
                    }
                    //写入文件
                    using (FileStream stm = File.OpenWrite(path))
                    {
                        workbook.Write(stm);
                    }
                    fs.Close();
                }
            }
            catch (CommonException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
