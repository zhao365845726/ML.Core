using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskScheduler;

namespace ML.Core.Common
{
  //参考地址：https://dahall.github.io/TaskScheduler/html/R_Project_TaskScheduler.htm
  public class TaskSchedulerHelper
  {
    public TaskSchedulerHelper()
    {

    }

    #region Task
    /// <summary>
    /// delete task
    /// </summary>
    /// <param name="taskName"></param>
    public static void DeleteTask(string taskName)
    {
      TaskSchedulerClass ts = new TaskSchedulerClass();
      ts.Connect(null, null, null, null);
      ITaskFolder folder = ts.GetFolder("\\");
      folder.DeleteTask(taskName, 0);
    }

    /// <summary>
    /// get all tasks
    /// </summary>
    public static IRegisteredTaskCollection GetAllTasks()
    {
      TaskSchedulerClass ts = new TaskSchedulerClass();
      ts.Connect(null, null, null, null);
      ITaskFolder folder = ts.GetFolder("\\");
      IRegisteredTaskCollection tasks_exists = folder.GetTasks(1);
      return tasks_exists;
    }
    /// <summary>
    /// check task isexists
    /// </summary>
    /// <param name="taskName"></param>
    /// <returns></returns>
    public static bool IsExists(string taskName)
    {
      var isExists = false;
      IRegisteredTaskCollection tasks_exists = GetAllTasks();
      for (int i = 1; i <= tasks_exists.Count; i++)
      {
        IRegisteredTask t = tasks_exists[i];
        if (t.Name.Equals(taskName))
        {
          isExists = true;
          break;
        }
      }
      return isExists;
    }

    /// <summary>
    /// create scheduler
    /// </summary>
    /// <param name="creator"></param>
    /// <param name="taskName"></param>
    /// <param name="path"></param>
    /// <param name="interval"></param>
    /// <param name="startBoundary"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public static _TASK_STATE CreateTaskScheduler(string creator, string taskName, string path, string interval, string startBoundary, string description,string userId,string password, _TASK_LOGON_TYPE taskLogonType, bool run = false)
    {
      try
      {
        if (IsExists(taskName))
        {
          DeleteTask(taskName);
        }

        //new scheduler
        TaskSchedulerClass scheduler = new TaskSchedulerClass();
        //pc-name/ip,username,domain,password
        scheduler.Connect(null, null, null, null);
        //get scheduler folder
        ITaskFolder folder = scheduler.GetFolder("\\");


        //set base attr 
        ITaskDefinition task = scheduler.NewTask(0);
        task.RegistrationInfo.Author = creator;//creator
        task.RegistrationInfo.Description = description;//description

        //set trigger  (IDailyTrigger ITimeTrigger)
        ITimeTrigger tt = (ITimeTrigger)task.Triggers.Create(_TASK_TRIGGER_TYPE2.TASK_TRIGGER_TIME);
        tt.Repetition.Interval = interval;// format PT1H1M==1小时1分钟 设置的值最终都会转成分钟加入到触发器
        tt.StartBoundary = startBoundary;//start time

        //set action
        IExecAction action = (IExecAction)task.Actions.Create(_TASK_ACTION_TYPE.TASK_ACTION_EXEC);
        action.Path = path;//计划任务调用的程序路径

        task.Settings.AllowDemandStart = true;    //可以独立于任何上下文启动
        task.Settings.Hidden = false;   //UI显示不显示
        task.Settings.ExecutionTimeLimit = "PT30S"; //运行任务时间超时停止任务吗? PTOS 不开启超时
        task.Settings.DisallowStartIfOnBatteries = false;//只有在交流电源下才执行
        task.Settings.RunOnlyIfIdle = false;//仅当计算机空闲下才执行

        if (string.IsNullOrEmpty(userId))
        {
          userId = null;
        }
        if (string.IsNullOrEmpty(password))
        {
          password = null;
        }

        //TASK_LOGON_NONE     登录后运行
        //TASK_LOGON_PASSWORD 无论是否登录都运行（但设置了任务后不执行），推荐使用NONE
        IRegisteredTask regTask = folder.RegisterTaskDefinition(taskName, task,
                                                            (int)_TASK_CREATION.TASK_CREATE,
                                                            userId, //user
                                                            password, // password
                                                            taskLogonType,
                                                            "");
        if (run)
        {
          IRunningTask runTask = regTask.Run(null);
          return runTask.State;
        }
        return regTask.State;
      }
      catch (Exception ex)
      {
        throw ex;
      }

    }

    /// <summary>
    /// create scheduler
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static _TASK_STATE CreateTaskSchedulerByEntity(TaskSchedulerEntity entity,bool run = false)
    {
      try
      {
        if (string.IsNullOrEmpty(entity.TaskName))
        {
          throw new Exception("计划任务名称不能为空");
        }
        _TASK_STATE taskState = CreateTaskScheduler(entity.Creator, entity.TaskName, entity.Path, entity.Interval, entity.StartBoundary, entity.Description, entity.UserId, entity.Password, entity.TaskLogonType, run);
        return taskState;
      }
      catch (Exception ex)
      {
        throw ex;
      }

    }


    #endregion

  }

  /// <summary>
  /// 计划任务实体
  /// </summary>
  public class TaskSchedulerEntity
  {
    /// <summary>
    /// 创建者
    /// </summary>
    public string Creator { get; set; }
    /// <summary>
    /// 计划任务名称
    /// </summary>
    public string TaskName { get; set; }
    /// <summary>
    /// 执行的程序路径
    /// </summary>
    public string Path { get; set; }
    /// <summary>
    /// 计划任务执行的频率 PT1M一分钟  PT1H30M 90分钟,如果为空则为执行一次
    /// </summary>
    public string Interval { get; set; }
    /// <summary>
    /// 开始时间 请遵循 yyyy-MM-ddTHH:mm:ss 格式
    /// </summary>
    public string StartBoundary { get; set; }
    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// UserId
    /// </summary>
    public string UserId { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// 任务登录类型
    /// </summary>
    public _TASK_LOGON_TYPE TaskLogonType { get; set; }
  }
}
