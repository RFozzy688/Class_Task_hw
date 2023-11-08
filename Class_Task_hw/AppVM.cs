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
        public Task<int> _task;
        public string _text;
    }
    internal class AppVM
    {
        MainWindow _view;
        WorkToText _workToText;
        List<TextDelegate> _methods;
        List<TaskInfo> _tasks;
        List<TextBlock> _textBlocks;
        List<CheckBox> _checkBoxes;
        Commands _getMethod;
        Commands _getAction;int c = 0;

        public AppVM(MainWindow view)
        {
            _view = view;

            _workToText = new WorkToText();
            _methods = new List<TextDelegate>();
            _tasks = new List<TaskInfo>();
            _textBlocks = new List<TextBlock>();
            _checkBoxes = new List<CheckBox>();

            _textBlocks.Add(_view.Text1);
            _textBlocks.Add(_view.Text2);
            _textBlocks.Add(_view.Text3);
            _textBlocks.Add(_view.Text4);
            _textBlocks.Add(_view.Text5);

            _checkBoxes.Add(_view.Check1);
            _checkBoxes.Add(_view.Check2);
            _checkBoxes.Add(_view.Check3);
            _checkBoxes.Add(_view.Check4);
            _checkBoxes.Add(_view.Check5);

            _getMethod = new Commands(ChoiceMethod);
            _getAction = new Commands(ActionMethod);

            _methods.Add(_workToText.NumberOfSentences);
            _methods.Add(_workToText.NumbreOfSimbols);
            _methods.Add(_workToText.NumbreOfWords);
            _methods.Add(_workToText.NumberOfInterrogativeSentences);
            _methods.Add(_workToText.NumberOfExclamationSentences);
        }
        public Commands GetMethod { get { return _getMethod; } }
        public Commands GetAction { get { return _getAction; } }
        private void ChoiceMethod(object param)
        {
            int index = Convert.ToInt32(param);

            if (_view.SaveToFile.IsChecked == true )
            {
                TaskInfo taskInfo = new TaskInfo();
                taskInfo._task = new Task<int>(() => _methods[index]());
                taskInfo._text = _checkBoxes[index].Content.ToString();

                _tasks.Add(taskInfo);
            }
            else if(_view.OutputOnScreen.IsChecked == true)
            {
                if (_textBlocks[index].Text == "")
                {
                    Task<int> task = new Task<int>(() => _methods[index]());
                    task.Start();

                    _textBlocks[index].Text = task.Result.ToString();
                }
                else
                {
                    _textBlocks[index].Text = "";
                }
            }
        }
        private void ActionMethod(object param)
        {
            foreach (var task in _tasks)
            {
                task._task.Start();
                //task._textBlocks.Text = task._task.Result.ToString();
            }
        }
    }
}
