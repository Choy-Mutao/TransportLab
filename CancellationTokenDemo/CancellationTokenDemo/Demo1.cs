namespace CancellationTokenDemo
{
    /// <summary>
    /// 本 demo 旨在实现 [操作取消, 即调用取消函数, 实现异步的停止]
    /// </summary>
    public class Demo1
    {
        // CS0236 A field initializer cannot reference the non-static field, method
        private static CancellationTokenSource m_cancellationTokenSource = new CancellationTokenSource();
        private CancellationToken m_cur_cancellationtoken = m_cancellationTokenSource.Token;

        public void Start()
        {
            Task.Run(() =>
            {
                while (!m_cur_cancellationtoken.IsCancellationRequested)
                {
                    Console.WriteLine("\n I m running");
                    Thread.Sleep(1000);
                }

                Console.WriteLine("\n I m cancelled");
            });
        }

        public void Cancel() =>  m_cancellationTokenSource.Cancel();
    }
}
