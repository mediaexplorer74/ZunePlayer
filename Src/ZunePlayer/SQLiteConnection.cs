using System;

namespace ZunePlayer
{
    public class SQLiteConnection
    {
        private string v;

        public SQLiteConnection(string v)
        {
            this.v = v;
        }

        internal IDisposable Prepare(string sql)
        {
            //throw new NotImplementedException();
            return default;
        }
    }
}