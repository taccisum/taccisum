using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;

namespace Common.Tool.Units
{
    /// <summary>
    /// HTTP GET/POST相关操作类。
    /// </summary>
    public class HttpHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HttpHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region POST方法
        #region Data
        /// <summary>
        /// POST提交方法重载
        /// </summary>
        /// <remarks>
        /// ContentType为默认："application/x-www-form-urlencoded"；
        /// UserAgent为默认："Mozilla-Firefox-Spider(Arvato)"。
        /// </remarks>
        /// <param name="url">string类型。提交地址。</param>
        /// <param name="postdata">string类型。提交数据。</param>
        /// <returns>string类型。接收返回。</returns>
        public static string Post(string url, string postdata)
        {
            return Post(url, postdata, "application/x-www-form-urlencoded", "Mozilla-Firefox-Spider(Arvato)");
        }
        /// <summary>
        /// POST提交方法重载
        /// </summary>
        /// <remarks>
        /// ContentType为默认："application/x-www-form-urlencoded"
        /// </remarks>
        /// <param name="url">string类型。提交地址。</param>
        /// <param name="postdata">string类型。提交数据。</param>
        /// <param name="useragent">string类型。User-Agent。</param>
        /// <returns>string类型。接收返回。</returns>
        public static string Post(string url, string postdata, string useragent)
        {
            return Post(url, postdata, "application/x-www-form-urlencoded", useragent);
        }
        /// <summary>
        /// POST提交方法重载
        /// </summary>
        /// <remarks>
        /// ContentType为默认："text/xml"
        /// UserAgent为默认："Mozilla-Firefox-Spider(Arvato)"。
        /// </remarks>
        /// <param name="url">string类型。提交地址。</param>
        /// <param name="postdata">string类型。提交数据。</param>
        /// <returns>string类型。接收返回。</returns>
        public static string PostXML(string url, string postdata)
        {
            return Post(url, postdata, "text/xml", "Mozilla-Firefox-Spider(Arvato)");
        }
        /// <summary>
        /// POST提交方法重载
        /// </summary>
        /// <remarks>
        /// ContentType为默认："text/xml"
        /// </remarks>
        /// <param name="url">string类型。提交地址。</param>
        /// <param name="postdata">string类型。提交数据。</param>
        /// <param name="useragent">string类型。User-Agent。</param>
        /// <returns>string类型。接收返回。</returns>
        public static string PostXML(string url, string postdata, string useragent)
        {
            return Post(url, postdata, "text/xml", useragent);
        }
        /// <summary>
        /// POST提交方法重载
        /// </summary>
        /// <param name="url">string类型。提交地址。</param>
        /// <param name="postdata">string类型。提交数据。</param>
        /// <param name="contenttype">string类型。ContentType，如"application/x-www-form-urlencoded"。</param>
        /// <param name="useragent">string类型。User-Agent。</param>
        /// <returns>string类型。接收返回。</returns>
        public static string Post(string url, string postdata, string contenttype, string useragent)
        {
            return Post(url, postdata, contenttype, useragent, null);
        }
        /// <summary>
        /// POST提交方法重载
        /// </summary>
        /// <remarks>
        /// 详细编码的代码页名称参考：http://msdn.microsoft.com/zh-cn/library/system.text.encoding.aspx
        /// </remarks>
        /// <param name="url">string类型。提交地址。</param>
        /// <param name="postdata">string类型。提交数据。</param>
        /// <param name="contenttype">string类型。ContentType，如"application/x-www-form-urlencoded"。</param>
        /// <param name="useragent">string类型。User-Agent。</param>
        /// <param name="encodingstring">string类型。编码的代码页名称，如"utf-8"。</param>
        /// <returns>string类型。接收返回。</returns>
        public static string Post(string url, string postdata, string contenttype, string useragent, string encodingstring)
        {
            return Post(url, postdata, contenttype, useragent, encodingstring, null, null);
        }
        /// <summary>
        /// POST提交方法重载
        /// </summary>
        /// <remarks>
        /// 详细编码的代码页名称参考：http://msdn.microsoft.com/zh-cn/library/system.text.encoding.aspx
        /// </remarks>
        /// <param name="url">string类型。提交地址。</param>
        /// <param name="postdata">string类型。提交数据。</param>
        /// <param name="contenttype">string类型。ContentType，如"application/x-www-form-urlencoded"。</param>
        /// <param name="useragent">string类型。User-Agent。</param>
        /// <param name="encodingstring">string类型。编码的代码页名称，如"utf-8"。</param>
        /// <param name="proxy">WebProxy类型。Webproxy。</param>
        /// <param name="cookie">CookieContainer类型。Cookies。</param>
        /// <returns>string类型。接收返回。</returns>
        public static string Post(string url, string postdata, string contenttype, string useragent, string encodingstring, WebProxy proxy, CookieContainer cookie)
        {
            string ret = "";
            try
            {
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(url);
                httpReq.Method = "POST";
                httpReq.ContentType = contenttype;
                httpReq.Accept = "*/*";
                httpReq.UserAgent = useragent;
                if (proxy != null)
                {
                    httpReq.Proxy = proxy;
                }
                if (cookie != null)
                {
                    httpReq.CookieContainer = cookie;
                }
                Stream reqStream = httpReq.GetRequestStream();
                StreamWriter sw = new StreamWriter(reqStream);

                sw.Write(postdata);
                sw.Close();
                Stream rspStream = httpReq.GetResponse().GetResponseStream();
                Encoding encoding = Encoding.Default;
                if (string.IsNullOrEmpty(encodingstring))
                {
                    encoding = Encoding.UTF8;
                }
                else
                {
                    encoding = Encoding.GetEncoding(encodingstring);
                }
                StreamReader sr = new StreamReader(rspStream, encoding);
                ret = sr.ReadToEnd();
                sr.Close();
                rspStream.Close();
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            return ret;
        }

        /// <summary>
        /// POST提交方法重载
        /// </summary>
        /// <param name="PostUrl">string类型。提交地址。</param>
        /// <param name="PostValue">NameValueCollection类型。需要提交的数据。</param>
        /// <param name="HeaderValue">NameValueCollection类型。设置提交的请求头。</param>
        /// <returns>string类型。接收返回。</returns>
        public static string Post(string PostUrl, NameValueCollection PostValue, NameValueCollection HeaderValue)
        {
            return Post(PostUrl, PostValue, HeaderValue, null);
        }

        /// <summary>
        /// POST提交方法重载
        /// </summary>
        /// <remarks>
        /// 详细编码的代码页名称参考：http://msdn.microsoft.com/zh-cn/library/system.text.encoding.aspx
        /// </remarks>
        /// <param name="PostUrl">string类型。提交地址。</param>
        /// <param name="PostValue">NameValueCollection类型。需要提交的数据。</param>
        /// <param name="HeaderValue">NameValueCollection类型。设置提交的请求头。</param>
        /// <param name="encodingstring">string类型。编码的代码页名称，如"utf-8"。</param>
        /// <returns>string类型。接收返回。</returns>
        public static string Post(string PostUrl, NameValueCollection PostValue, NameValueCollection HeaderValue, string encodingstring)
        {
            try
            {
                //定义webClient对象
                WebClient webClient = new WebClient();
                Encoding encoding = Encoding.Default;
                if (string.IsNullOrEmpty(encodingstring))
                {
                    encoding = Encoding.UTF8;
                }
                else
                {
                    encoding = Encoding.GetEncoding(encodingstring);
                }
                webClient.Encoding = encoding;
                if (HeaderValue != null)
                {
                    try
                    {
                        //请求头
                        IEnumerator myEnumerator = HeaderValue.GetEnumerator();
                        foreach (String headername in HeaderValue.AllKeys)
                        {
                            webClient.Headers.Add(headername, HeaderValue[headername]);
                        }
                    }
                    catch { }
                }
                //向服务器发送POST数据
                byte[] responseArray = webClient.UploadValues(PostUrl, PostValue);
                string contentencodeing = webClient.ResponseHeaders[HttpResponseHeader.ContentEncoding];
                string data = GetStringFromArray(responseArray, contentencodeing, encoding);
                return data;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion
        #region File

        /// <summary>
        /// Post文件（可多个）
        /// </summary>
        /// <param name="url">目标API</param>
        /// <param name="paths">文件路径集合</param>
        /// <param name="timeout">请求超时时间</param>
        /// <returns>请求结果</returns>
        public static string PostFiles(string url, IEnumerable<string> paths, int timeout = 3000)
        {
            string result = "";
            var pathList = paths.Select(p => p.Trim()).ToList();
            var nameList = pathList.Select(p => p.Substring(p.LastIndexOf(@"\") + 1));
            string boundaryStr = "--" + DateTime.Now.Ticks.ToString("x");      //边界符
            byte[] boundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundaryStr);
            List<byte[]> contentDispositionList = nameList.Select(n =>
                Encoding.UTF8.GetBytes(String.Format("\r\nContent-Disposition: form-data; name=\"{0}\"filename=\"{1}\"\r\n" +
                                  "Content-Type: application/octet-stream\r\n\r\n", n, n))).ToList();       //content-disposition配置字符串列表

            FileStream fs = null;
            Stream rs = null;
            StreamReader sr = null;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                //设置获得响应的超时时间
                request.Timeout = timeout;
                request.Method = "POST";
                request.ContentType = "multipart/form-data; boundary=" + boundaryStr;

                //提交http格式的报文
                rs = request.GetRequestStream();

                for (int i = 0; i < pathList.Count; i++)
                {
                    rs.Write(boundaryBytes, 0, boundaryBytes.Length); //写边界符
                    rs.Write(contentDispositionList[i], 0, contentDispositionList[i].Length); //写内容描述
                    fs = File.Open(pathList[i], FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    byte[] bytes = new byte[fs.Length];
                    br.Read(bytes, 0, Convert.ToInt32(fs.Length));
                    br.Close();
                    fs.Close();
                    br = null;
                    fs = null;

                    rs.Write(bytes, 0, bytes.Length); //写二进制文件流
                }

                rs.Write(boundaryBytes, 0, boundaryBytes.Length);
                rs.Flush();
                rs.Close();
                rs = null;

                sr = new StreamReader(request.GetResponse().GetResponseStream());
                result = sr.ReadToEnd();
                sr.Close();
                sr = null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                //关闭I/O流
                if (fs != null)
                {
                    fs.Flush();
                    fs.Close();
                }
                if (rs != null)
                {
                    rs.Flush();
                    rs.Close();
                }
                if (sr != null)
                {
                    sr.Close();
                }
            }

            return result;
        }

        public static string PostFile(string url, string path)
        {
            List<string> paths = new List<string>();
            paths.Add(path);
            return PostFiles(url, paths);
        }
        #endregion
        #endregion

        #region GET方法
        /// <summary>
        /// GET方法重载
        /// </summary>
        /// <remarks>
        /// ContentType为默认："text/html"；
        /// UserAgent为默认："Mozilla-Firefox-Spider(Arvato)"。
        /// </remarks>
        /// <param name="url">string类型。提交地址。</param>
        /// <returns>string类型。接收返回。</returns>
        public static string Get(string url)
        {
            return Get(url, "text/html", "Mozilla-Firefox-Spider(Arvato)");
        }
        /// <summary>
        /// GET方法重载
        /// </summary>
        /// <remarks>
        /// ContentType为默认："text/html"
        /// </remarks>
        /// <param name="url">string类型。提交地址。</param>
        /// <param name="useragent">string类型。User-Agent。</param>
        /// <returns>string类型。接收返回。</returns>
        public static string Get(string url, string useragent)
        {
            return Get(url, "text/html", useragent);
        }
        /// <summary>
        /// GET方法重载
        /// </summary>
        /// <param name="url">string类型。提交地址。</param>
        /// <param name="contenttype">string类型。ContentType。</param>
        /// <param name="useragent">string类型。User-Agent。</param>
        /// <returns>string类型。接收返回。</returns>
        public static string Get(string url, string contenttype, string useragent)
        {
            return Get(url, contenttype, useragent, null);
        }
        /// <summary>
        /// GET方法重载
        /// </summary>
        /// <remarks>
        /// 详细编码的代码页名称参考：http://msdn.microsoft.com/zh-cn/library/system.text.encoding.aspx
        /// </remarks>
        /// <param name="url">string类型。提交地址。</param>
        /// <param name="contenttype">string类型。ContentType。</param>
        /// <param name="useragent">string类型。User-Agent。</param>
        /// <param name="encodingstring">string类型。编码的代码页名称，如"utf-8"。</param>
        /// <returns>string类型。接收返回。</returns>
        public static string Get(string url, string contenttype, string useragent, string encodingstring)
        {
            return Get(url, contenttype, useragent, encodingstring, null, null);
        }
        /// <summary>
        /// GET方法重载
        /// </summary>
        /// <remarks>
        /// 详细编码的代码页名称参考：http://msdn.microsoft.com/zh-cn/library/system.text.encoding.aspx
        /// </remarks>
        /// <param name="url">string类型。提交地址。</param>
        /// <param name="contenttype">string类型。ContentType。</param>
        /// <param name="useragent">string类型。User-Agent。</param>
        /// <param name="encodingstring">string类型。编码的代码页名称，如"utf-8"。</param>
        /// <param name="proxy">WebProxy类型。Webproxy。</param>
        /// <param name="cookie">CookieContainer类型。Cookies。</param>
        /// <returns>string类型。接收返回。</returns>
        public static string Get(string url, string contenttype, string useragent, string encodingstring, WebProxy proxy, CookieContainer cookie)
        {
            string ret = "";
            try
            {
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(url);
                httpReq.Method = "GET";
                httpReq.ContentType = contenttype;
                httpReq.Accept = "*/*";
                httpReq.UserAgent = useragent;
                if (proxy != null)
                {
                    httpReq.Proxy = proxy;
                }
                if (cookie != null)
                {
                    httpReq.CookieContainer = cookie;
                }

                Stream rspStream = httpReq.GetResponse().GetResponseStream();
                Encoding encoding = Encoding.Default;
                if (string.IsNullOrEmpty(encodingstring))
                {
                    encoding = Encoding.UTF8;
                }
                else
                {
                    encoding = Encoding.GetEncoding(encodingstring);
                }
                StreamReader sr = new StreamReader(rspStream, encoding);
                ret = sr.ReadToEnd();
                sr.Close();
                rspStream.Close();
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            return ret;
        }
        /// <summary>
        /// GET方法重载
        /// </summary>
        /// <param name="GetUrl">string类型。请求地址。</param>
        /// <param name="HeaderValue">NameValueCollection类型。设置提交的请求头。</param>
        /// <returns>string类型。接收返回。</returns>
        public static string Get(string GetUrl, NameValueCollection HeaderValue)
        {
            return Get(GetUrl, HeaderValue, null);
        }

        /// <summary>
        /// GET方法重载
        /// </summary>
        /// <remarks>
        /// 详细编码的代码页名称参考：http://msdn.microsoft.com/zh-cn/library/system.text.encoding.aspx
        /// </remarks>
        /// <param name="GetUrl">string类型。请求地址。</param>
        /// <param name="HeaderValue">NameValueCollection类型。设置提交的请求头。</param>
        /// <param name="encodingstring">string类型。编码的代码页名称，如"utf-8"。</param>
        /// <returns>string类型。接收返回。</returns>
        public static string Get(string GetUrl, NameValueCollection HeaderValue, string encodingstring)
        {
            try
            {
                //定义webClient对象
                WebClient webClient = new WebClient();
                Encoding encoding = Encoding.Default;
                if (string.IsNullOrEmpty(encodingstring))
                {
                    encoding = Encoding.UTF8;
                }
                else
                {
                    encoding = Encoding.GetEncoding(encodingstring);
                }
                webClient.Encoding = encoding;
                if (HeaderValue != null)
                {
                    try
                    {
                        //请求头
                        IEnumerator myEnumerator = HeaderValue.GetEnumerator();
                        foreach (String headername in HeaderValue.AllKeys)
                        {
                            webClient.Headers.Add(headername, HeaderValue[headername]);
                        }
                    }
                    catch { }
                }
                //向服务器发送并接收数据
                byte[] responseArray = webClient.DownloadData(GetUrl);
                string contentencodeing = webClient.ResponseHeaders[HttpResponseHeader.ContentEncoding];
                string data = GetStringFromArray(responseArray, contentencodeing, encoding);
                return data;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 从数组和Gzip解析出字符串
        /// </summary>
        private static string GetStringFromArray(byte[] responseData, string Gzip, Encoding encoding)
        {
            string str = "";
            if ((Gzip != null) && (Gzip == "gzip"))
            {
                str = gzipDecompress(responseData, encoding);
            }
            else
            {
                str = encoding.GetString(responseData);
            }
            return str;
        }
        /// <summary>
        /// GZIP解压缩
        /// </summary>
        private static string gzipDecompress(byte[] responseData, Encoding encoding)
        {
            StringBuilder builder = new StringBuilder(0x19000);
            GZipStream stream = new GZipStream(new MemoryStream(responseData), CompressionMode.Decompress);
            byte[] buffer = new byte[0x5000];
            for (int i = stream.Read(buffer, 0, 0x5000); i > 0; i = stream.Read(buffer, 0, 0x5000))
            {
                builder.Append(encoding.GetString(buffer, 0, i));
            }
            return builder.ToString();
        }


        /// <summary>
        /// 将传入的键值对集合组装成url的search部分字符串格式
        /// </summary>
        /// <param name="pairs">请求参数键值对</param>
        /// <returns></returns>
        public static string BuildUrlSearch(IEnumerable<KeyValuePair<string, string>> pairs)
        {
            string searchStr = "";
            foreach (var pair in pairs)
            {
                searchStr += pair.Key + "=" + pair.Value + "&";
            }
            return searchStr.Trim('&');
        }
        #endregion
    }
}
