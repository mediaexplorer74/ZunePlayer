using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace ZunePlayer
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class NowPlayingPage : Page
    {
        public NowPlayingPage()
        {
            this.InitializeComponent();
        }

        ViewModel.SongVM songList = ViewModel.SongVM.Single();
        int SongId;


        //
        //share song part
        private void ShareSong(object sender, RoutedEventArgs e)
        {
            if (songList.SelectedItem != null)
            {
                DataTransferManager.ShowShareUI();
            }
            else
            {
                var j = new MessageDialog("Please select item!").ShowAsync();
            }
        }

        async void ShareRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {

            DataRequest request = args.Request;
            DataRequestDeferral getFile = args.Request.GetDeferral();
            if (songList.SelectedItem != null)
            {
                request.Data.Properties.Title = "Share a good song with you：" 
                    + songList.SelectedItem.Title;
                request.Data.SetText("The singer is" + songList.SelectedItem.Artist +
                    "\n" + "Copy the link to the browser to listen" + songList.SelectedItem.Path);
            }
            else
            {
                request.Data.Properties.Title = Title1.Text;
                request.Data.SetText(Artist1.Text);
            }

            try
            {
                //Picture sharing is not completed
                //StorageFile image_File = await StorageFile.GetFileFromApplicationUriAsync(new Uri("Assets/音符.png"));
                //request.Data.Properties.Thumbnail = RandomAccessStreamReference.CreateFromFile(image_File);
                //request.Data.SetBitmap(RandomAccessStreamReference.CreateFromFile(image_File));
            }
            finally
            {
                getFile.Complete();
            }
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataTransferManager.GetForCurrentView().DataRequested += ShareRequested;

            //Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.Collapsed;
        }

        private void UpdateSong(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        //RnD
        private void DeleteSong(object sender, RoutedEventArgs e)
        {
            using (var statement = App.conn.Prepare("DELETE FROM MusicLib WHERE Id = ?"))
            {
                //statement.Bind(1, /*GetId()*/default);
                //statement.Step();
            }
            songList.RemoveSong();
            var j = new MessageDialog("Delete successful!").ShowAsync();
            //update(sender, e);
            //Title.Text = "";
            //Artist.Text = "";
            //Path.Text = "";
        }
    }
}
