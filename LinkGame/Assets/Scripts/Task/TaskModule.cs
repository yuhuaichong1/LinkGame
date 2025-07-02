using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XrCode
{
    public class TaskModule : BaseModule
    {
        Dictionary<int, Task> tasks;
        Dictionary<int, int> taskStatus;

        protected override void OnLoad()
        {
            base.OnLoad();

            tasks = new Dictionary<int, Task>();
            taskStatus = new Dictionary<int, int>();
            GetTaskData();
        }

        private void GetTaskData()
        {
            taskStatus = SPlayerPref.GetDictionary<int, int>(PlayerPrefDefines.taskStatus);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
        }

        /// <summary>
        /// »ñÈ¡½±Àø
        /// </summary>
        /// <param name="taskId"></param>
        public void Receive(int taskId)
        {
            
        }
    }
}

