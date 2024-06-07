# CancellationToken

> 本资料的书写起于希望写一个 *即时停止* 的功能在我的某个控制类中,
> 当前这个解决方案的实现逻辑是在全计算流程中构造 *停止点* 来实现
> 属于在三个基本代码段中的 *loop* 中增加跳出条件

## 先验知识

* csharp 异步编程
* 批处理的概念

## 概述

CancellationToken 提供了一种 **方便** 的[方式](CancellationToken 是一个结构，它允许你通过一个 CancellationToken 对象来通知操作应该取消) 来 *取消异步操作* ，以提高程序的健壮性和用户体验。

## NOTE

每个 "CancellationToken" 实例都是一次性的。一旦它被取消，它就会一直保持取消状态，无法恢复。

## 使用场景

* 操作取消: 通过调用某个函数, 实现 *停止* 指令的传输;
* 超时处理: 对某操作设置一个时间限制, *CancellationToken* 可以与 *Task.Delay* 结合使用来实现超时处理;
* 资源管理: 通过外部资源监视器通知, 例如，当执行某个文件 I/O 操作时，如果磁盘空间不足，可能需要取消该操作;
* **批处理任务: 取消整个批处理过程**(我现在也不懂)
* 网络请求: 核心逻辑是, 任何停止的操作, 是要释放当前和即将使用的计算机资源;

## .NET 框架下的三个重要组件

1. CancellationTokenSource(取消令牌源): *用于发出取消请求*
    * "CancellationTokenSource" 是一个可用于发出取消请求的对象
    * 它包含了一个 "CancellationToken" ，并且可以通过调用 "CancellationTokenSource.Token" 属性来获取该取消令牌。
    * 通过调用 "CancellationTokenSource.Cancel()" 方法，可以发出取消请求，通知与之关联的 "CancellationToken"
    * "CancellationTokenSource" 可以被用于创建多个 "CancellationToken" ，从而实现多个地方对相同取消请求的监视和处理

2. CancellationToken(取消令牌): *用于检查取消请求是否发生，并与异步操作关联起来*
    * "CancellationToken" 是一个用于检查是否发生了取消请求的对象。
    * 它通常通过 "CancellationTokenSource.Token" 属性获取，与特定的 "CancellationTokenSource" 相关联
    * 可以通过调用 "CancellationToken.IsCancellationRequested" 属性来检查是否发生了取消请求。
    * "CancellationToken" 可以用于监视异步操作的状态，并在取消请求时终止这些操作

3. CancellationTokenRegistration(取消令牌注册): *用于注册取消操作的回调函数，以便在取消请求时执行相应的操作*
    * "CancellationTokenRegistration" 用于注册一个委托，当取消请求被发出时执行该委托
    * 它通常是由 "CancellationToken.Register()" 方法返回的
    * 可以使用 "CancellationTokenRegistration.Dispose()" 方法取消注册，避免在取消请求后继续执行相关的操作。
    * 通过取消注册，可以确保在不需要时避免资源的浪费或不必要的处理。

## 相关设计模式

1. 轮询模式（Polling Pattern）：
    * 这种模式适用于需要周期性地检查取消请求是否被发出的情况。
    * 在异步操作中，可以在循环中调用CancellationToken.IsCancellationRequested属性来检查取消请求，并在需要时终止操作。

2. 等待模式（Wait Pattern）：
    * 这种模式适用于需要在取消请求发出前等待某个条件的情况。
    * 可以使用CancellationToken.WaitHandle.WaitOne()方法来等待取消请求的信号。

3. 监听模式（Observer Pattern）：
    * 这种模式适用于需要在取消请求发出时通知多个观察者的情况。
    * 可以使用CancellationToken.Register()方法注册取消操作的回调函数。

4. 工作池模式（Worker Pool Pattern）：

    * 这种模式适用于需要在多个工作任务中共享相同的取消请求的情况。
    * 可以将同一个CancellationToken对象传递给多个工作任务，以便它们能够共享取消请求。

5. 超时模式（Timeout Pattern）：
    * 这种模式适用于需要在操作执行时间过长时自动取消操作的情况。
    * 可以使用CancellationToken结合Task.Delay()或Task.WhenAny()来实现操作的超时控制。
