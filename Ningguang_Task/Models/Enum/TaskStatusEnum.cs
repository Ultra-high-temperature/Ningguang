namespace Ningguang.Models.Enum
{
    public enum TaskStateEnum:int
    {   
        //创建中
        INIT = 0,
        
        //就绪
        SETUP = 1,
        
        //队列中
        WAITING = 5,
        
        //等待前置task完成
        WAIT_BEFORE_TASK = 8,
        
        //运行中
        RUNNING = 10,
        
        //完成
        FINISHED = 20, 
        
        //暂停
        PAUSE = 30,
        
        //删除
        DELETE = 40
    }
}