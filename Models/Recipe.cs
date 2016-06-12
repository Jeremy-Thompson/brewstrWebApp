using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace brewstrWebApp.Models
{
    public class Recipe
    {
        //Administrative info
        private String _name;
        private String _author;
        private DateTime _dateCreated;
        private int _category;
        private int _id;
        //Brew specific info
        public int _mashTemp;
        public int _mashTime;
        public int _boilTemp;
        public int _boilTime;
        public int _hopsTime1;
        public int _hopsTime2;
        public int _hopsTime3;
        public int _hopsTime4;
        private int _brewCount;

        public Recipe()
        {
            _name = null;
            _author = null;
            _dateCreated = DateTime.Now;
            _category = 0;
            _mashTemp = 0;
            _mashTime = 0;
            _boilTemp = 0;
            _boilTime = 0;
            _hopsTime1 = 0;
            _hopsTime2 = 0;
            _hopsTime3 = 0;
            _hopsTime4 = 0;
            _brewCount = 0;

        }
        public Recipe(String name, String author, DateTime datecreated, int category,
            int mashtemp, int mashtime, int boiltemp, int boiltime, int hopstime1, int hopstime2,
            int hopstime3, int hopstime4, int brewcount)
        {
            _name = name;
            _author = author;
            _dateCreated = datecreated;
            _category = category;
            _mashTemp = mashtemp;
            _mashTime = mashtime;
            _boilTemp = boiltemp;
            _boilTime = boiltime;
            _hopsTime1 = hopstime1;
            _hopsTime2 = hopstime2;
            _hopsTime3 = hopstime3;
            _hopsTime4 = hopstime4;
            _brewCount = brewcount;
        }
        public string name
        {
            get { return _name; }
        }
        public string author
        {
            get { return _author; }
        }
        public DateTime dateCreated
        {
            get { return _dateCreated; }
        }
        public int category
        {
            get { return _category;  }
        }
        public int id
        {
            get { return _id;  }
        }
    }
}