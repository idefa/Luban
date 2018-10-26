using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LubanSample.Controllers
{
    public static class HttpRequestExtension
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="request">原始请求</param>
        /// <param name="value">结果值</param>
        /// <returns></returns>
        public static JsonResult CreateResponse<T>(this HttpRequestBase request, T value)
        {
            return new JsonResult() { Data = value, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// 返回错误信息
        /// </summary>
        /// <param name="request">原始请求</param>
        /// <param name="statusCode">状态码</param>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public static HttpStatusCodeResult CreateResponse(this HttpRequestBase request, HttpStatusCode statusCode, string errorMsg)
        {
            return new HttpStatusCodeResult(statusCode, errorMsg);
        }
    }
}