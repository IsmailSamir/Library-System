using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace Library
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Delete(object sender, EventArgs e)
        {
            FileStream FS = new FileStream("Book.txt", FileMode.Open);

            string BeDeleted = DeleteBook.Text;

            byte[] p; bool Found = false; string Temp = "";

            while (FS.Position != FS.Length && !Found)
            {
                Book book = new Book();
                string BookName = "";

                book.FieldID_Len=FS.ReadByte();
                p = new byte[book.FieldID_Len];
                FS.Read(p, 0, book.FieldID_Len);
                for (int i = 0; i < book.FieldID_Len; i++)
                    Temp += (char)p[i];
                book.set_FieldID(Temp);

                FS.Seek(5,SeekOrigin.Current);
                p=new byte[20];
                FS.Read(p,0,20);
                for (int i = 0; i < 20; i++)
                    BookName += (char)p[i];
                book.set_BookName(BookName);

                if(BeDeleted.CompareTo(BookName)==0)
                {
                    book.BookAuthor_Len = FS.ReadByte();
                    for (int i = 0; i < book.BookAuthor_Len; i++)
                        FS.WriteByte((byte)'\0');

                    long releas =(book.BookAuthor_Len + 25 + book.FieldID_Len);
                    
                    FS.Seek(-releas,SeekOrigin.Current);
                    for (int i = 0; i < releas; i++)
                        FS.WriteByte((byte)'\0');
                       
                    MessageBox.Show("Record Deleted !! ");
                    Found = true;
                }
                
                else
                {
                    book.BookAuthor_Len = FS.ReadByte();
                    FS.Seek(book.BookAuthor_Len, SeekOrigin.Current);
                }
			 
			
            }

            DeleteBook.Clear();
         

            FS.Close();
                
        }
        private void AddFields(object sender, EventArgs e)
        {
            Fields Field = new Fields();
            FileStream _FS = new FileStream("Fields.txt", FileMode.Append);

            byte[] p; string Temp;

            Field.set_FieldID(Field_ID.Text);
            Field.set_FieldName(FieldName.Text);
            Field.set_ShelfNumber(ShelfNumber.Text);


            Field.Record_Len=Field.get_FieldID().Length+Field.get_FieldName().Length+Field.get_ShelfNumber().Length;

            _FS.WriteByte((byte)Field.Record_Len);
            
            //Write Field ID .
            _FS.WriteByte((byte)Field.get_FieldID().Length);           
            Temp=Field.get_FieldID();
            p = new byte[Field.get_FieldID().Length];
            for (int i = 0; i < Field.get_FieldID().Length; i++)
                p[i] = (byte)Temp[i];
            _FS.Write(p, 0, Field.get_FieldID().Length);

            //Write Field Name.
            _FS.WriteByte((byte)Field.get_FieldName().Length);
            Temp = Field.get_FieldName();
            p=new byte[Field.get_FieldName().Length];
            for (int i = 0; i < Field.get_FieldName().Length; i++)
                p[i] = (byte)Temp[i];
            _FS.Write(p, 0, Field.get_FieldName().Length);

            //Write Shelf Number.
            _FS.WriteByte((byte)Field.get_ShelfNumber().Length);
            Temp = Field.get_ShelfNumber();
            p = new byte[Field.get_ShelfNumber().Length];
            for (int i = 0; i < Field.get_ShelfNumber().Length; i++)
                p[i] = (byte)Temp[i];
            _FS.Write(p, 0, Field.get_ShelfNumber().Length);


            Field_ID.Clear();
            FieldName.Clear();
            ShelfNumber.Clear();

            _FS.Close();   
        
        }
        private void DisplayShelf(object sender, EventArgs e)
        {
            FileStream FS = new FileStream("Fields.txt", FileMode.Open);

            string _Category = Category.Text;
            byte[] p; bool Found = false;

            //Search for ShelfNumber.
            while (FS.Position != FS.Length && !Found)
            {
                Fields F = new Fields();
               
                F.Record_Len = FS.ReadByte();
                F.FieldID_Len = FS.ReadByte();
                FS.Seek(F.FieldID_Len, SeekOrigin.Current);
                F.FieldName_Len = FS.ReadByte();
                p = new byte[F.FieldName_Len];
                FS.Read(p, 0, F.FieldName_Len);
                for (int i = 0; i < F.FieldName_Len; i++)
                    F.FieldName+= (char)p[i];

                if (_Category.CompareTo(F.FieldName) == 0)
                {
                    F.ShelfNumber_Len = FS.ReadByte();
                    p = new byte[F.ShelfNumber_Len];
                    FS.Read(p, 0, F.ShelfNumber_Len);
                    for (int i = 0; i < F.ShelfNumber_Len; i++)
                        F.ShelfNumber += (char)p[i];
                    MessageBox.Show("Shlef ID : " + F.ShelfNumber);
                    Found = true;
                }
               
                else
                {

                    F.FieldID_Len = FS.ReadByte();
                    FS.Seek(F.FieldID_Len, SeekOrigin.Current);
                }

            }
            if (!Found)
            {
                MessageBox.Show("Category Not Found !! ");
            }

            Category.Clear();
            FS.Close();
        }
        private void AddBook(object sender, EventArgs e)
        {
            Book book=new Book();
            FileStream FS = new FileStream("Book.txt", FileMode.Append, FileAccess.Write);
        

            byte[] p; string Temp;

            //Write Field ID.
            book.set_FieldID(FieldID.Text);
            Temp = book.get_FieldID();
            FS.WriteByte((byte)book.get_FieldID().Length);
            p = new byte[book.get_FieldID().Length];
            for (int i = 0; i < book.get_FieldID().Length; i++)
                p[i] = (byte)Temp[i];
            FS.Write(p, 0, book.get_FieldID().Length);

            //Write Book ID.
            if (book.set_BookID(BookID.Text))
            {
                 p = new byte[book.get_BookID().Length];
                 Temp = book.get_BookID();
                for (int i = 0; i < book.get_BookID().Length; i++)
                    p[i] = (byte)Temp[i];
                FS.Write(p, 0, book.get_BookID().Length);
            }
            else
                MessageBox.Show("Error !! Book_ID  Length Very Large .");

            
            //Write Book Name.
            if (book.set_BookName(NameBook.Text))
            {
                 p = new byte[book.get_BookName().Length];
                 Temp = book.get_BookName();
                for (int i = 0; i < book.get_BookName().Length; i++)
                    p[i] = (byte)Temp[i];
                FS.Write(p, 0, book.get_BookName().Length);
            }
            else
                MessageBox.Show("Error !! Book_Name Length Very Large .");


            //Write Book Author.
            book.set_BookAuthor(BookAuthor.Text);         
            Temp = book.get_BookAuthor();
            FS.WriteByte((byte)book.get_BookAuthor().Length);
            p = new byte[book.get_BookAuthor().Length];
            for (int i = 0; i < book.get_BookAuthor().Length; i++)
                p[i] = (byte)Temp[i];
            FS.Write(p, 0, book.get_BookAuthor().Length);

            BookID.Clear();
            NameBook.Clear();
            FieldID.Clear();
            BookAuthor.Clear();
                       
            FS.Close();
        }
        private void DisplayBooks(object sender, EventArgs e)
        {
            FileStream FSB = new FileStream("Book.txt", FileMode.Open);
            FileStream FS = new FileStream("Fields.txt", FileMode.Open);

            Fields F = new Fields();

            string _Category = Field_Name.Text;
            byte[] p; bool Found = false;

            //Search For Field ID In Field File.
            while (FS.Position != FS.Length && !Found)
            {
                F = new Fields();
              
                F.Record_Len = FS.ReadByte();
                F.FieldID_Len = FS.ReadByte();
                p = new byte[F.FieldID_Len];
                FS.Read(p,0,F.FieldID_Len);
                for (int i = 0; i < F.FieldID_Len; i++)
                    F.FieldID += (char)p[i];
               
                F.FieldName_Len = FS.ReadByte();
                p = new byte[F.FieldName_Len];
                FS.Read(p, 0, F.FieldName_Len);
                for (int i = 0; i < F.FieldName_Len; i++)
                    F.FieldName += (char)p[i];

                if (_Category.CompareTo(F.FieldName) == 0)
                {
                    Found = true;
                }
                else
                {

                    F.ShelfNumber_Len = FS.ReadByte();
                    FS.Seek(F.ShelfNumber_Len, SeekOrigin.Current);
                }
            } 
            if (!Found)
            {
                MessageBox.Show("Category Not Found !! ");
                return;
            }
     
            //Search For Authors Name In Book File.
            while(FSB.Position != FSB.Length)
            {
                Book book = new Book();
                book.FieldID_Len = FSB.ReadByte();
                p = new byte[book.FieldID_Len];
                FSB.Read(p, 0, book.FieldID_Len);
                for (int i = 0; i < book.FieldID_Len; i++)
                    book.FieldID += (char)p[i];             
                FSB.Seek(25, SeekOrigin.Current);
                if(book.FieldID.CompareTo(F.get_FieldID())==0)
                {   
                    book.BookAuthor_Len = FSB.ReadByte();
                    p = new byte[book.BookAuthor_Len];
                    FSB.Read(p, 0, book.BookAuthor_Len);
                    for (int i = 0; i < book.BookAuthor_Len - 1; i++)
                        book.BookAuthor += (char)p[i];

                    listView.Items.Add("Author Of "+Field_Name.Text+ " Book : ['"+book.get_BookAuthor()+"'].");
                                     
                }
                else
                {
                    book.BookAuthor_Len = FSB.ReadByte();
                    FSB.Seek(book.BookAuthor_Len, SeekOrigin.Current);
                }
            }

            FSB.Close();
            FS.Close();
        }
      
        
           
        private void BookID_TextChanged(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void BookAuthor_TextChanged(object sender, EventArgs e)
        {

        }
        private void label8_Click(object sender, EventArgs e)
        {

        }
        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
        private void label9_Click(object sender, EventArgs e)
        {

        }
        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
