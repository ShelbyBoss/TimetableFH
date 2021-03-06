﻿using System.ComponentModel;
using Windows.UI;
using TimetableFH.Controls.Compare;

namespace TimetableFH.Coloring
{
    public class EventColor : INotifyPropertyChanged
    {
        private string group, name;
        private CompareType groupCompareType, nameCompareType;
        private Color color;

        public string Group
        {
            get { return group; }
            set
            {
                if (value == group) return;

                group = value;
                OnPropertyChanged(nameof(Group));
            }
        }

        public CompareType GroupCompareType
        {
            get { return groupCompareType; }
            set
            {
                if (value == groupCompareType) return;

                groupCompareType = value;
                OnPropertyChanged(nameof(GroupCompareType));
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (value == name) return;

                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public CompareType NameCompareType
        {
            get { return nameCompareType; }
            set
            {
                if (value == nameCompareType) return;

                nameCompareType = value;
                OnPropertyChanged(nameof(NameCompareType));
            }
        }

        public Color Color
        {
            get { return color; }
            set
            {
                if (value == color) return;

                color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        public EventColor()
        {
            GroupCompareType = CompareType.Ignore;
            NameCompareType = CompareType.Ignore;
            Color = Colors.Gray;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
