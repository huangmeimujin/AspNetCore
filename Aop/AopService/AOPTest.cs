using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AopService
{
    public class AOPTest : IInterceptor
    {
        public ILogger<AOPTest> _logger { get; set; }

        public void Intercept(IInvocation invocation)
        {
            _logger.LogWarning("你正在调用方法 "{ 0}
            "  参数是 {1}... ",
               invocation.Method.Name,
               string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray()));
            //在被拦截的方法执行完毕后 继续执行           
            invocation.Proceed();

            _logger.LogWarning("方法执行完毕，返回结果：{0}", invocation.ReturnValue);
        }
    }
}
