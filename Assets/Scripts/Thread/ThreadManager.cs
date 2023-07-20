/*
 * FileName:      ThreadManager.cs
 * Author:        魏宇辰
 * Date:          2023/07/20 11:23:37
 * Describe:      线程管理器
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System.Threading;
using UnityEngine;

namespace DemoGame
{
    public class ThreadManager
    {
        private Thread computationThread;
        public void Init()
        {
            computationThread = new Thread(ComputationThread);
            computationThread.Start();
        }

        public void Stop()
        {
            computationThread.Interrupt();
            computationThread.Abort();
            computationThread = null;
        }

        public void ComputationThread()
        {
            while (true)
            {

            }
        }

    }


}
