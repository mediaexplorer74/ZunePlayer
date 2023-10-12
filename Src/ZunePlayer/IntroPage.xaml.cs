using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ZunePlayer
{
    
    public sealed partial class IntroPage : Page
    {
        public IntroPage()
        {
            this.InitializeComponent();
        }

        ViewModel.SongVM songList = ViewModel.SongVM.Single();
        int SongId;

        private void LuckySong(object sender, RoutedEventArgs e)
        {
            if (songList.AllItems.Count > 0)
            {
                Random rd = new Random();
                SongId = rd.Next() % songList.AllItems.Count;
                Uri SongUri = new Uri(songList.GetSongByID(SongId).Path);
                mediaPlayer.Source = SongUri;
                mediaPlayer.Play();
                PoleStoryboard.Begin();
                BigStoryboard.Begin();
            }
        }

        private void NextSongPlay(object sender, RoutedEventArgs e)
        {
            if (songList.AllItems.Count > 0)
            {
                Random rd = new Random();
                SongId = rd.Next() % songList.AllItems.Count;
                Uri SongUri = new Uri(songList.GetSongByID(SongId).Path);
                mediaPlayer.Source = SongUri;
                mediaPlayer.Play();
                PoleStoryboard.Begin();
                BigStoryboard.Begin();
            }
        }

        private void GoList(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void GoAdd(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddSong));
        }
    }
}
