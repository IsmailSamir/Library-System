using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Book
    {
        //Fixed Length.
        public char[] BookID, BookName;
        public int BookeID_Len, BookName_Len;
       
        //Length Indicator.
        public string BookAuthor, FieldID;
        public int BookAuthor_Len,FieldID_Len;

        public Book()
        {
            BookeID_Len = 5;
            BookName_Len = 20;
            BookID = new char[BookeID_Len];
            BookName = new char[BookName_Len];
        }
      
        public bool set_BookID(string _BookID)
        {
            if(_BookID.Length > BookeID_Len)
            {
                return false;
            }
            _BookID.CopyTo(0, BookID, 0, _BookID.Length);
            for (int i = _BookID.Length; i < BookeID_Len; i++)
                BookID[i] = '\0';
            return true;
        }
        public bool set_BookName(string _BookName)
        {
            if (_BookName.Length > BookName_Len)
                return false;

            _BookName.CopyTo(0, BookName, 0, _BookName.Length);
            for (int i = _BookName.Length; i < BookName_Len; i++)
                BookName[i] = '\0';

            return true;
        }
       
        public void set_BookAuthor(string _BookAuthor)
        {
            BookAuthor = _BookAuthor;
            BookAuthor += "@";
            BookAuthor_Len = BookAuthor.Length;
        }
        public void set_FieldID(string _FieldID)
        {
            FieldID = _FieldID;
            FieldID_Len = FieldID.Length;
        }


        public string get_BookID()
        {
            string _BookId = new string(BookID);
            return _BookId;
        }
        public string get_BookName()
        {
            string _BookName = new string(BookName);
            return _BookName;
        }
        public string get_BookAuthor()
        {
            return BookAuthor;
        }
        public string get_FieldID()
        {
            return FieldID;
        }

    }
}
