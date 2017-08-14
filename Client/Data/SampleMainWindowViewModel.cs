using KChatClient.Models;
using KChatClient.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace KChatClient.Data
{
    public class SampleMainWindowViewModel : ViewModelBase
    {
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Participant> _participants = new ObservableCollection<Participant>();
        public ObservableCollection<Participant> Participants
        {
            get { return _participants; }
            set
            {
                _participants = value;
                OnPropertyChanged();
            }
        }

        private Participant _selectedParticipant;
        public Participant SelectedParticipant
        {
            get { return _selectedParticipant; }
            set
            {
                _selectedParticipant = value;
                if (SelectedParticipant.HasSentNewMessage) SelectedParticipant.HasSentNewMessage = false;
                OnPropertyChanged();
            }
        }

        public SampleMainWindowViewModel()
        {
            ObservableCollection<ChatMessage> someChatter = new ObservableCollection<ChatMessage>();
            someChatter.Add(new ChatMessage
            {
                Author = "George",
                Message = "Hello",
                Time = DateTime.Now
            });
            someChatter.Add(new ChatMessage
            {
                Author = "Boss",
                Message = "Hello, George",
                Time = DateTime.Now,
                IsOriginNative = true
            });
            someChatter.Add(new ChatMessage
            {
                Author = "George",
                Message = "Can I ask you something",
                Time = DateTime.Now
            });
            someChatter.Add(new ChatMessage
            {
                Author = "Boss",
                Message = "Yes, you can.",
                Time = DateTime.Now,
                IsOriginNative = true
            });
            someChatter.Add(new ChatMessage
            {
                Author = "George",
                Message = "I am not sure that I can finish my tasks for today.",
                Time = DateTime.Now
            });
            someChatter.Add(new ChatMessage
            {
                Author = "George",
                Message = "Is that a problem to continue tommorrow?",
                Time = DateTime.Now
            });

            Participants.Add(new Participant { Name = "George", Chatter = someChatter });
            Participants.Add(new Participant { Name = "Samantha", Chatter = someChatter, IsLoggedIn = false });
            Participants.Add(new Participant { Name = "Caleb", Chatter = someChatter, HasSentNewMessage = true });
            Participants.Add(new Participant { Name = "Mike", Chatter = someChatter, HasSentNewMessage = true });
            Participants.Add(new Participant { Name = "John", Chatter = someChatter });

            SelectedParticipant = Participants.First();
        }
    }
}
