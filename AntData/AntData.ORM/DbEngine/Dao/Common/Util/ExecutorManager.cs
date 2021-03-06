﻿using System;
using AntData.ORM.DbEngine;
using AntData.ORM.DbEngine.Executor;

namespace AntData.ORM.Common.Util
{
    public class ExecutorManager
    {
        private static readonly Object obj = new Object();
        private static IExecutor executor = null;

        public static IExecutor Executor
        {
            get
            {
                if (executor == null)
                {
                    lock (obj)
                    {
                        if (executor == null)
                        {
                            try
                            {
                                Type executorType = DALBootstrap.GetExecutorType();
                                if (executorType == null)
                                {
                                   executor = new DefaultExecutor();
                                }
                                else
                                {
                                    executor = Activator.CreateInstance(executorType) as IExecutor;
                                }
                            }
                            catch
                            {
                                executor = new DefaultExecutor();
                            }
                        }
                    }
                }

                return executor;
            }
        }
    }
}