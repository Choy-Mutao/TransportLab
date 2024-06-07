// See https://aka.ms/new-console-template for more information
using CancellationTokenDemo;

Console.WriteLine("Hello, World!");

var cancel_demo1 = new Demo1();
cancel_demo1.Start();

// 等待用户输入以取消任务
Console.ReadKey();

cancel_demo1.Cancel();