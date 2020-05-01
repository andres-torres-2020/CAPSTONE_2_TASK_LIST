using System;
using System.Collections.Generic;
using System.Text;

namespace CAPSTONE_2_TASK_LIST
{
    class Task
    {
        #region fields
        private string name;
        private string description;
        private DateTime dueDate;
        private bool completedStatus;
        #endregion
        #region properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public DateTime DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }
        public bool CompletedStatus
        {
            get { return completedStatus; }
            set { completedStatus = value; }
        }
        #endregion
        #region constructors
        public Task()
        {
            completedStatus = false;
        }
        public Task(string _name, string _description, DateTime _dueDate, bool _completedStatus)
        {
            name = _name;
            description = _description;
            dueDate = _dueDate;
            completedStatus = _completedStatus;
        }
        #endregion
    }
}
