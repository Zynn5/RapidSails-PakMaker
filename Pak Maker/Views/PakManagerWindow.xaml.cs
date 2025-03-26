using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Pak_Maker.Content.Pak_Maker.Content;
using Pak_Maker.VM;
using Pak_Maker.Commands;

namespace Pak_Maker.Views
{
    /// <summary>
    /// Interaction logic for PakManagerWindow.xaml
    /// </summary>
    public partial class PakManagerWindow : MetroWindow
    {
        private readonly PakManagerVM _viewModel;
        public PakManagerWindow(PakManagerVM viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }
        private async void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (sender is Border clickedBorder)
            {
                var clickedImageWrapper = (ImageWrapper)clickedBorder.DataContext;

                var viewModel = (PakManagerVM)this.DataContext;

                viewModel.HighlightImage(clickedImageWrapper);

                string packUrl = clickedImageWrapper.Pack;
                string category = viewModel.ActiveButton;

                PakDownloader pakDownloader = new PakDownloader();
                await pakDownloader.DownloadPakFileAsync(packUrl, category);
            }
        }
    }
}
