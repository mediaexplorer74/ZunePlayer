using SQLitePCL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ZunePlayer
{
    /// <summary>
    /// Provide application-specific behavior to supplement the default application class。
    /// </summary>
    sealed partial class App : Application
    {
        public static SQLiteConnection conn { get; internal set; }

        //load db
        private void LoadDatabase()
        {     // Get a reference to the SQLite database    
            conn = new SQLiteConnection("MusicSong.db");
            string sql = @"CREATE TABLE IF NOT EXISTS
            MusicLib (
            Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
            Title VARCHAR( 140 ),
            Artist VARCHAR( 140 ),
            Album VARCHAR( 140 ),
            Path VARCHAR( 140 ),
            Playcount INTEGER
            );";
            using (var statement = conn.Prepare(sql))
            {
                //statement.Step();
            }
        }

     
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            LoadDatabase();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            //rootFrame.Style = Resources["RootFrameStyle"] as Style;
                       
            if (rootFrame == null)
            {
                
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    
                }

               
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
               
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }

           
            Window.Current.Activate();

            // register a global listener for the BackRequested event
            // You can register for this event in each page if you want to exclude specific pages from back navigation, 
            // or you want to execute page-level code before displaying the page.
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;

            rootFrame.Navigated += (s, a) =>
            {
                if (rootFrame.CanGoBack)
                {
                    // Setting this visible is ignored on Mobile and when in tablet mode!     
                    Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.Collapsed;
                }
                else
                {
                    Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.Collapsed;
                }
            };
        }

       
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
           
            deferral.Complete();
        }

        private void OnBackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }
    }
}
