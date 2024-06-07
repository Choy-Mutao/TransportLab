using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancellationTokenDemo
{
    /// <summary>
    /// 本 demo 旨在实现 [操作取消, 即调用取消函数, 实现异步的停止]
    /// </summary>
    public class Demo1
    {
        private CancellationTokenSource m_cancellationTokenSource = new CancellationTokenSource();

        public void Start()
        {

        }

        public void Cancel()
        {

        }
    }
}
