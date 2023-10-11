using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ZunePlayer.Model;
//using SQLitePCL;

namespace ZunePlayer.ViewModel
{
    public class SongVM
    {

        //Use singleton mode, this is a singleton interface
        private static SongVM single = null;
        public static SongVM Single()
        {
            if (single == null)
            {
                single = new SongVM();
            }
            return single;
        }

        //Load all songs
        private ObservableCollection<Model.Song> allItems = new ObservableCollection<Model.Song>();
        public ObservableCollection<Model.Song> AllItems { get { return this.allItems; } }
        //Currently selected song
        private Model.Song selectedItem = default(Model.Song);
        public Model.Song SelectedItem { get { return selectedItem; } set { this.selectedItem = value; } }

        //Constructor
        private SongVM()
        {
            string q = "%%";
            using (var statement = App.conn.Prepare(
                "SELECT Id, Name, Singer, Path FROM Customer WHERE Id LIKE ? OR Name LIKE ? OR Singer LIKE ? OR Path LIKE ?"))
            {
                //statement.Bind(1, q);
                //statement.Bind(2, q);
                //statement.Bind(3, q);
                //statement.Bind(4, q);
                //while (statement.Step() != /*SQLiteResult.DONE*/null)
                //{
                //    string did = statement[0].ToString();
                //    string dname = statement[1].ToString();
                //    string dsinger = statement[2].ToString();
                //    string dpath = statement[3].ToString();

                //    this.AllItems.Add(new Model.Song(dname, dsinger, dpath));
                //}
            }
        }
        
        //当前选中的歌的id
        public int selectedSong = -1;

        //通过ID获取歌曲
        public Song GetSongByID(int id)
        {
            foreach (Song tempSong in AllItems)
            {
                if (tempSong.Id == id)
                {
                    return tempSong;
                }
            }
            return null;
        }

        
        //向歌单里增加歌曲,返回歌曲ID
        public int AddSong(string _name,string _singer, string _path)
        {
            Song tempSong = new Song( _name,_singer, _path);
            AllItems.Add(tempSong);
            return tempSong.Id;
        }

        //删除歌曲
        public void RemoveSong()
        {
            this.allItems.Remove(this.selectedItem);
            this.selectedItem = null;
        }

        //更新歌曲信息
        public void UpdateSong(int id, string _name, string _singer, string _path)
        {
            //this.selectedItem.id = id;
            this.selectedItem.Title = _name;
            this.selectedItem.Artist = _singer;
            this.selectedItem.Path = _path;
            this.selectedItem = null;
        }

    }
}
