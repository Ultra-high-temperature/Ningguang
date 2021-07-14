技术栈为dotnet5.0 + efc + mysql5.7 + quarz

待办清单

数据库重新设计，表结构不依赖数据项，采用json字段存储信息，用

没找到dotnet平台对自定义类加载器的支持 可能需要研究一下OSGi.NET了

ningguang_task模块通过远程调用其他模块API实现定时任务
task是调度模块，只存放调用api的必要数据和任务的执行状态，任务分发到模块产生了什么数据，task不予理会
task -> item 
task指的是一个定时任务，包含多个同时执行的item。
item指的是定时任务的具体操作。同一个task下的item可能会发给不同的模块。
注意：同一个task内的item顺序无法保证，能保证顺序的只有配置有preTask的两个任务

task定时任务通过底层Quartz实现 一个task会按照itemList的类别划分出多个job，当job全部执行完成，task才算完成一次执行
task 主要 作用是结合 pretaskId 保证任务的先后顺序准确
关于preTask，要求所有前置Task至少执行过一次

systemTrigger 0/5 * * * * ? 系统任务，从task和item表中加载任务到内存中，并自动的创建job


jobDetail绑定上job
trigger定时器
Scheduler 和 jobDetail 是一对一的关系吗？
不是。
schedulerFactory.getScheduler();
返回的是是同一个调度器对象
Scheduler 和 jobDetail 是一对多的关系
绑定了同一个Job的不同JobDetail也是相互隔离的
trigger不能复用
jobdetail和trigger是一对一的关系，对象不能复用


quatrz的key是由group + name 组成 如：TriggerKey: group1.triggerName1
目前group分为system 和 user 两类
name细分为triggerName和jobName


name = {TaskId}_{ItemType}_USER_JOB 

name = {TaskId}_{ItemType}_USER_TRIGGER

实际的jobKey = userJob.{name}

设计中，systemJob负责加载卸载用户任务

ReaderWriterLockSlim ReaderWriterLock   

------
废弃设计：
job对象需要使用dbcontext，故希望quartz加入di框架，交由容器注入dbcontext
原因：net内嵌框架无法在运行时动态注册job对象，quartz又不能使用有参构造函数实例化job，以注入dbcontext

Job instance construction
By default Quartz will try to resolve job's type from container and if there's no explicit registration Quartz will use ActivatorUtilities to construct job and inject it's dependencies via constructor. Job should have only one public constructor.
默认情况下，Quartz 将尝试从容器解析作业的类型，如果没有显式注册，Quartz 将用于ActivatorUtilities构造作业并通过构造函数注入它的依赖项。Job 应该只有一个公共构造函数。