using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Pak_Maker.Commands
{
    public class ImageWrapper : INotifyPropertyChanged
    {
        public BitmapImage Image { get; }
        private bool _isHighlighted;
        public string Pack { get; set; }
        public bool IsHighlighted
        {
            get => _isHighlighted;
            set
            {
                if (_isHighlighted != value)
                {
                    _isHighlighted = value;
                    OnPropertyChanged(nameof(IsHighlighted));
                }
            }
        }

        public ImageWrapper(BitmapImage image, bool isHighlighted, string pack)
        {
            Image = image;
            IsHighlighted = isHighlighted;
            Pack = pack;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }



}


