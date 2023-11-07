using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Class_Task_hw
{
    public delegate int TextDelegate();
    struct TaskInfo
    {
        public int _number;
        public Task<int> _task;
        public TextBlock _textBlocks;
    }
    internal class AppVM
    {
        MainWindow _view;
        WorkToText _workToText;
        List<TextDelegate> _methods;
        List<TaskInfo> _tasks;
        List<TextBlock> _textBlocks;
        Commands _getMethod;
        Commands _getAction;

        public AppVM(MainWindow view)
        {
            _view = view;

            _workToText = new WorkToText();
            _methods = new List<TextDelegate>();
            _tasks = new List<TaskInfo>();
            _textBlocks = new List<TextBlock>();

            _textBlocks.Add(_view.Text1);
            _textBlocks.Add(_view.Text2);
            _textBlocks.Add(_view.Text3);
            _textBlocks.Add(_view.Text4);
            _textBlocks.Add(_view.Text5);

            _getMethod = new Commands(ChoiceMethod);
            _getAction = new Commands(ActionMethod);

            _methods.Add(_workToText.NumberOfSentences);
            _methods.Add(_workToText.NumberOfSentences);
            _methods.Add(_workToText.NumberOfSentences);
            _methods.Add(_workToText.NumberOfSentences);
            _methods.Add(_workToText.NumberOfSentences);
        }
        public Commands GetMethod { get { return _getMethod; } }
        public Commands GetAction { get { return _getAction; } }
        private void ChoiceMethod(object param)
        {
            int index = _tasks.FindIndex(o => o._number == Convert.ToInt32(param));

            if (index == -1)
            {
                TaskInfo taskInfo = new TaskInfo();
                taskInfo._task = new Task<int>(() => _methods[Convert.ToInt32(param)]());
                taskInfo._number = Convert.ToInt32(param);
                taskInfo._textBlocks = _textBlocks[Convert.ToInt32(param)];

                _tasks.Add(taskInfo);
            }
            else
            {
                _tasks.RemoveAt(index);
            }
        }
        private void ActionMethod(object param)
        {
            foreach (var task in _tasks)
            {
                task._task.Start();
                task._textBlocks.Text = task._task.Result.ToString();
            }
        }
    }
}
