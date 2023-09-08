using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;
using System;

namespace WPFDemo
{
    /// <summary>
    /// Represents a single message with timer functionality.
    /// </summary>
    public class Message : INotifyPropertyChanged
    {
        public string MessageText { get; set; }

        // Holds remaining time for message notification
        private double _timeRemaining;

        public double TimeRemaining
        {
            get { return _timeRemaining; }
            set
            {
                _timeRemaining = value;
                OnPropertyChanged();
            }
        }

        // Timer to countdown for message notification
        private Timer _timer;

        // Event invoked when message timer expires
        public event Action MessageTimeExpiredEvent;

        // Event for property change notification
        public event PropertyChangedEventHandler PropertyChanged;

        // Constructor to initialize message and start timer
        public Message(string messageText, double timeInSeconds)
        {
            MessageText = messageText;
            TimeRemaining = timeInSeconds;

            _timer = new Timer(1000);
            _timer.Elapsed += OnTimerTick; // Attach event to eventhandler
            _timer.AutoReset = true;
            _timer.Start();
        }

        // Handler for timer tick event
        private void OnTimerTick(object sender, ElapsedEventArgs e)
        {
            TimeRemaining -= 1;
            if (TimeRemaining <= 0)
            {
                _timer.Stop();
                MessageBox.Show(MessageText);
                MessageTimeExpiredEvent?.Invoke();
            }
        }

        // Notify property change
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"A message will display in {TimeRemaining} seconds";
        }
    }
}
