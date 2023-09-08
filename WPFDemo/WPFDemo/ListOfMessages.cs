using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Xml.Linq;

namespace WPFDemo
{
    public class ListOfMessages : ObservableCollection<Message>
    {
        public void Add(string messageText,double timer)
        {
            Message newMsg = new Message(messageText, timer);
            newMsg.MessageTimeExpiredEvent += () =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.Remove(newMsg);
                });
            };
            this.Add(newMsg);
        }
    }
}
