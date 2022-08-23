using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace Projek1
{
    public class Movie
    {
        public int id { get; set; }
        public string title { get; set; }
        public string genre { get; set; }
        public string sinopsis { get; set; }

        public Movie(int _id, string _title, string _genre, string _sinopsis)
        {
            this.title = _title;
            this.genre = _genre;
            this.sinopsis = _sinopsis;
            this.id = _id;
        }

    }
}
